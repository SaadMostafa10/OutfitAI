
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
        Task<OutfitResultDto> AnalyzeOutfitAsync(OutfitRequestDto request);
    }
}
