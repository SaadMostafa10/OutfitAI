using Shared;
using Shared.Dtos.OutfitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Abstractions.Outfit
{
    public interface IRecommendationService
    {
        Task<IEnumerable<OutfitRecommendationsDto>> GetRecommendationsAsync(string userId);
        Task<SavedOutfitResponseDto> SaveOutfitAsync(string userId, int outfitId);
        Task<PaginationResponse<SavedOutfitResponseDto>> GetSavedOutfitsAsync(string userId , int pageIndex, int pageSize);
        Task DeleteSavedOutfitAsync(string userId, int outfitId);
    }
}
