
using Shared;
using Shared.Dtos.OutfitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Outfit
{
    public interface IOutfitService
    {
        Task<OutfitResultDto> AnalyzeAndSaveOutfitAsync(string userId, OutfitRequestDto request);
        Task<PaginationResponse<OutfitHistoryDto>> GetUserHistoryAsync(string userId, OutfitHistorySpecificationsParameters specParams);
        Task<bool> DeleteHistoryItemAsync(string userId, int historyId);
    }
}
