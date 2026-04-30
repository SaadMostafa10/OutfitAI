using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions.BadRequest.Outfit;
using Domain.Exceptions.NotFound;
using Domain.Exceptions.NotFound.Outfit;
using Domain.Models.Outfit.outfits_recommendation;
using Microsoft.AspNetCore.Http.Metadata;
using Services.Abstractions.Outfit;
using Services.Specifications.OutfitRecommendation;
using Shared;
using Shared.Dtos.OutfitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Outfit
{
    public class RecommendationService(IUnitOfWork _unitOfWork , IMapper _mapper) : IRecommendationService
    {
        public async Task<IEnumerable<OutfitRecommendationsDto>> GetRecommendationsAsync(string userId)
        {
            // Step 1.  Get the user's saved outfits
            var savedSpec = new SavedOutfitsByUserSpec(userId);

            var savedOutfits = await _unitOfWork.Repository<UserSavedOutfit>().GetAllWithSpecAsync(savedSpec);

            var SavedOutfitIDs = savedOutfits.Select(s => s.OutfitId).ToList();


            // Step 2. Calculate Preferences based on saved outfits
            
            var now = DateTime.UtcNow;

            var categoryScore = new Dictionary<string, double>();

            foreach (var saved in savedOutfits)
            {

                var daysOld = (now - saved.SavedAt).TotalDays;

                var weight = Math.Exp(-0.05 * daysOld); // Exponential decay with half-life of ~14 days

                var category = saved.Outfit.Category;
                if (categoryScore.ContainsKey(category))
                    categoryScore[category] += weight;
                else
                    categoryScore[category] = weight;
            }

            // Step 3. Convert Preferences to distribution

            int totalRecommendations = 20;

            var categories = new[] { "casual", "formal", "sports", "semi-formal" };

            var distribution = new Dictionary<string, int>();

            int maxPerCategory = (int)(totalRecommendations * 0.4);
            int minPerCategory = 1;

            if (categoryScore.Count > 0)
            {

                var totalScore = categoryScore.Values.Sum();

                foreach (var category in categories)
                {
                    var score = categoryScore.ContainsKey(category) ? categoryScore[category] : 0;

                    var weight = totalScore > 0 ? score / totalScore : 0 ;

                    var calculated = (int)Math.Round(weight * totalRecommendations);

                    calculated = Math.Max(calculated, minPerCategory);
                    calculated = Math.Min(calculated, maxPerCategory);

                    distribution[category] = calculated;
 
                }
            }
            else
            {
                int perCategory = totalRecommendations / categories.Length;

                foreach (var category in categories)
                {
                    distribution[category] = perCategory;
                }
            }

            // Step 4. Fetch Recommendations per Category

            var result = new List<OutfitRecommendations>();

            var actualCount = new Dictionary<string, int>();

            foreach (var item in distribution)
            {
                var category = item.Key;
                var count = item.Value;

                var spec = new OutfitRecommendationSpec(category, SavedOutfitIDs);

                var Outfits = await _unitOfWork.Repository<OutfitRecommendations>().GetAllWithSpecAsync(spec);

                var selected = Outfits.Take(count).ToList();

                result.AddRange(selected);

                actualCount[category] = selected.Count; // In case we have less than desired due to exclusions
            }

            // Step 5. Fix the total count if we have less than desired due to exclusions
  
            if (result.Count < totalRecommendations)
            {
                var needed = totalRecommendations - result.Count;
 

                var spec = new OutfitRecommendationSpec(null, new List<int>());

                var additionalOutfits = await _unitOfWork.Repository<OutfitRecommendations>().GetAllWithSpecAsync(spec);

                var filtered = additionalOutfits.Where(o => !result.Any(r => r.Id == o.Id)).ToList();

                var newcategories = new[] { "casual", "formal", "sports", "semi-formal" };

                var selectedAdditional = new List<OutfitRecommendations>();
                int perCategory = needed / newcategories.Length;
                foreach (var category in newcategories)
                {
                    var categoryOutfits = filtered.Where(o => o.Category == category).Take(perCategory).ToList();
                    selectedAdditional.AddRange(categoryOutfits);
                }

                var remaining = needed - selectedAdditional.Count;

                if (remaining > 0)
                {
                    var rest = filtered
                        .Where(o => !selectedAdditional.Any(s => s.Id == o.Id))
                        .Take(remaining)
                        .ToList();

                    selectedAdditional.AddRange(rest);
                }

                result.AddRange(selectedAdditional);
            }

            // Step 6. Map to DTOs

            var finalResult = _mapper.Map<List<OutfitRecommendationsDto>>(result);

            // step 7. Return the final list of recommendations
            return finalResult
                  .OrderBy(x => Guid.NewGuid()) // shuffle
                  .Take(totalRecommendations).ToList();

        }
 
        public async Task<SavedOutfitResponseDto> SaveOutfitAsync(string userId, int outfitId)
        {
            // Step 1. Check if already exists
            var outfitspec = new OutfitRecommendationSpec(outfitId);
            var outfit =await _unitOfWork.Repository<OutfitRecommendations>().GetWithSpecAsync(outfitspec);
            if (outfit is null)
                throw new OutfitNotFoundException();

            // Step 2. Check if already saved
            var savedspec = new SavedOutfitExistsSpec(userId, outfitId);
            var alreadySaved =await _unitOfWork.Repository<UserSavedOutfit>().GetWithSpecAsync(savedspec);
            if (alreadySaved is not null)
                throw new Outfitalreadysaved();

            // Step 3. Save the outfit
            var savedOutfit = new UserSavedOutfit
            {
                UserId = userId,
                OutfitId = outfitId,
                SavedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<UserSavedOutfit>().AddAsync(savedOutfit);
            await _unitOfWork.CompleteAsync();

            // Step 4. Return response
             return new SavedOutfitResponseDto
             {
                    OutfitId = outfitId,
                    SavedAt = savedOutfit.SavedAt,
                    Outfit = _mapper.Map<OutfitRecommendationsDto>(outfit)
             };
        }

        public async Task<PaginationResponse<SavedOutfitResponseDto>> GetSavedOutfitsAsync(string userId, int pageIndex, int pageSize)
        {
            var spec = new SavedOutfitsByUserSpec(userId , pageIndex , pageSize);
            var savedOutfits =await _unitOfWork.Repository<UserSavedOutfit>().GetAllWithSpecAsync(spec);

            if (!savedOutfits.Any())
                throw new NoSavedOutfitNotFoundException();

            var result = _mapper.Map<List<SavedOutfitResponseDto>>(savedOutfits);

            var Speccount = new CountOfSavedOutfitsByUserSpec(userId , pageIndex , pageSize);

            var totalCount = await _unitOfWork.Repository<UserSavedOutfit>().CountAsync(Speccount);

            return new PaginationResponse<SavedOutfitResponseDto>(pageIndex, pageSize, totalCount, result);
        }

        public async Task DeleteSavedOutfitAsync(string userId, int outfitId)
        {
            var spec = new SavedOutfitExistsSpec(userId, outfitId);
            var savedOutfit = await _unitOfWork.Repository<UserSavedOutfit>().GetWithSpecAsync(spec);

            if (savedOutfit is null)
                throw new SavedOutfitNotFoundException();

            _unitOfWork.Repository<UserSavedOutfit>().Delete(savedOutfit);
            await _unitOfWork.CompleteAsync();
        }
    } 
}
