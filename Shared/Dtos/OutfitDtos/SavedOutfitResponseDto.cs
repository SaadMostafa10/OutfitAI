using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OutfitDtos
{
    public class SavedOutfitResponseDto
    {
        public int OutfitId { get; set; }
        public DateTime SavedAt { get; set; }
        public OutfitRecommendationsDto Outfit { get; set; }
    }
}
