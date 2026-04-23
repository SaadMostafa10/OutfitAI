using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions.BadRequest.Outfit;
using Domain.Models.Outfit;
using Microsoft.AspNetCore.Http;
using Services.Abstractions;
using Services.Abstractions.Outfit;
using Shared.Dtos.OutfitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Exceptions.NotFound;
using Microsoft.Extensions.Configuration;
using Services.Abstractions.URLService;
using Services.Specifications;
namespace Services.Outfit
{
    public class OutfitService(
        IUnitOfWork unitOfWork,
        IAttachmentService attachmentService,
        IMapper mapper,
        HttpClient httpClient,
        IConfiguration configuration,
        IUrlService urlService) : IOutfitService
    {
        public async Task<OutfitResultDto> AnalyzeAndSaveOutfitAsync(string userId, OutfitRequestDto request)
        {
            // 1. Validation: Ensure all 5 images are provided
            var images = new[] { request.Top, request.Bottom, request.Shoe, request.Accessory, request.Bag };

            if (images.Any(i => i == null || i.Length == 0))
                throw new OutfitImagesRequiredException();

            // 2. Validation: Ensure all files are images
            if (images.Any(i => !i.ContentType.StartsWith("image/")))
                throw new InvalidImageFileException();

            // 3. Prepare the request for Flask AI (Base64Async)
            var aiRequest = new
            {
                top = await ToBase64Async(request.Top),
                bottom = await ToBase64Async(request.Bottom),
                shoe = await ToBase64Async(request.Shoe),
                bag =  await ToBase64Async(request.Bag),
                accessory = await ToBase64Async(request.Accessory)
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(aiRequest), Encoding.UTF8, "application/json");

            // 4.Send data to Flask AI server
            var response = await httpClient.PostAsync("diagnose", jsonContent);
            
            if (!response.IsSuccessStatusCode)
                throw new OutfitAnalysisFailedException();
            
            var resultContent = await response.Content.ReadAsStringAsync();

            try
            {
                // 5. Deserialize AI Response using your OutfitResultDto

                ///// Use this Hardcoded data instead:
                ///var aiResult = new OutfitResultDto
                ///{
                ///    OriginalScore = 0.755f,
                ///    ImprovedScore = 0.900f,
                ///    IsCompatible = true,
                ///    Replacements = new Dictionary<string, string>
                ///    {
                ///        { "Shoe", "White Sneaker" },
                ///        { "Accessory", "Silver Watch" }
                ///    }
                ///};
                // --- MOCK END ---
                var aiResult = JsonSerializer.Deserialize<OutfitResultDto>(resultContent);
                if (aiResult == null)
                    throw new InvalidOutfitAnalysisResultException();

                // 6.Upload images to server storage for history
                // Note: only upload if the AI analysis was successful
                string topPath = await attachmentService.UploadAsync(request.Top, "outfits") ?? "";
                string bottomPath = await attachmentService.UploadAsync(request.Bottom, "outfits") ?? "";
                string shoePath = await attachmentService.UploadAsync(request.Shoe, "outfits") ?? "";
                string accessoryPath = await attachmentService.UploadAsync(request.Accessory, "outfits") ?? "";
                string bagPath = await attachmentService.UploadAsync(request.Bag, "outfits") ?? "";

                // 7.Save to History table
                var history = new OutfitHistory
                {
                    UserId = userId,
                    TopImagePath = topPath,
                    BottomImagePath = bottomPath,
                    ShoeImagePath = shoePath,
                    AccessoryImagePath = accessoryPath,
                    BagImagePath = bagPath,
                    OriginalScore = aiResult.OriginalScore,
                    ImprovedScore = aiResult.ImprovedScore ?? 0f, 
                    IsCompatible = aiResult.IsCompatible,

                    ReplacementsJson = aiResult.Replacements != null
                        ? JsonSerializer.Serialize(aiResult.Replacements)
                        : null,
                    CreatedAt = DateTime.UtcNow
                };

                await unitOfWork.Repository<OutfitHistory>().AddAsync(history);
                await unitOfWork.CompleteAsync();

                // 8. Return result to mobile app
                return aiResult;
            }
            catch (JsonException)
            {
                throw new InvalidAiResponseFormatException();
            }
            
        }

        public async Task<IEnumerable<OutfitHistoryDto>> GetUserHistoryAsync(string userId)
        {
            // 1. Get data using specification
            var spec = new OutfitHistoryWithUserSpecification(userId);
            var historyList = await unitOfWork
                .Repository<OutfitHistory>()
                .GetAllWithSpecAsync(spec);

            // 2. Map to DTO
            var result = mapper.Map<IEnumerable<OutfitHistoryDto>>(historyList);

            // 3. Build full URLs in service layer 
            foreach (var item in result)
            {
                item.TopImagePath = urlService.BuildImageUrl(item.TopImagePath);
                item.BottomImagePath = urlService.BuildImageUrl(item.BottomImagePath);
                item.ShoeImagePath = urlService.BuildImageUrl(item.ShoeImagePath);
                item.AccessoryImagePath = urlService.BuildImageUrl(item.AccessoryImagePath);
                item.BagImagePath = urlService.BuildImageUrl(item.BagImagePath);
            }

            return result;
        }

        public async Task<bool> DeleteHistoryItemAsync(string userId, int historyId)
        {
            // 1. Get item from DB to access image names
            var item = await unitOfWork.Repository<OutfitHistory>().GetByIdAsync(historyId);

            // Safety Check: Item must exist and belong to the same user
            if (item == null || item.UserId != userId)
                throw new Domain.Exceptions.NotFound.OutfitHistoryNotFoundException(historyId);

            // 2. Store image names in a list before deleting the record
            var filesToDelete = new List<string?>
            {
                item.TopImagePath,
                item.BottomImagePath,
                item.ShoeImagePath,
                item.AccessoryImagePath,
                item.BagImagePath
            }.Where(f => !string.IsNullOrEmpty(f)).ToList();

            // 3. Delete record and commit to DB
            unitOfWork.Repository<OutfitHistory>().Delete(item);
            var result = await unitOfWork.CompleteAsync();

            // 4. If DB success, delete physical files from storage
            if (result > 0)
            {
                foreach (var fileName in filesToDelete)
                {
                    // Reconstruct the full path (must match upload folder)
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files/outfits", fileName!);
                    attachmentService.Delete(fullPath);
                }
            }
            return result > 0;
        }

        private async Task<string> ToBase64Async(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return Convert.ToBase64String(ms.ToArray());
        }
        
    }
}
