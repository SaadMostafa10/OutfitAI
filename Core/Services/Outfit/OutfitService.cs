using Domain.Exceptions.BadRequest.Outfit;
using Microsoft.AspNetCore.Http;
using Services.Abstractions.Outfit;
using Shared.Dtos.OutfitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Outfit
{
    public class OutfitService : IOutfitService
    {
        private readonly HttpClient _httpClient;

        public OutfitService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<OutfitResultDto> AnalyzeOutfitAsync(OutfitRequestDto request)
        {
            var images = new[] { request.Top, request.Bottom, request.Shoe, request.Accessory, request.Bag };

            if (images.Any(i => i == null || i.Length == 0))
                throw new OutfitImagesRequiredException();

            if (images.Any(i => !i.ContentType.StartsWith("image/")))
                throw new InvalidImageFileException();


            var aiRequest = new
            {
                top = ToBase64(request.Top),
                bottom = ToBase64(request.Bottom),
                shoe = ToBase64(request.Shoe),
                bag = ToBase64(request.Bag),
                accessory = ToBase64(request.Accessory),

            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(aiRequest), Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("diagnose", jsonContent);

            if (!response.IsSuccessStatusCode)
                throw new OutfitAnalysisFailedException();


            var resultContent = await response.Content.ReadAsStringAsync();


            try
            {
                var result = JsonSerializer.Deserialize<OutfitResultDto>(resultContent);
                if (result == null)
                    throw new InvalidOutfitAnalysisResultException();

                return result;
            }
            catch (JsonException)
            {
                throw new InvalidAiResponseFormatException();
            }

        }

        private string ToBase64(IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
