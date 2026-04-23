using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OutfitDtos
{
    public class CreateOutfitHistoryDto
    {
        public float OriginalScore { get; set; }
        public float ImprovedScore { get; set; }
        public string TopImagePath { get; set; } = string.Empty;
        public string BottomImagePath { get; set; } = string.Empty;
        public string ShoeImagePath { get; set; } = string.Empty;
        public string? AccessoryImagePath { get; set; }
        public string? BagImagePath { get; set; }
        // AI Suggestions stored as JSON string
        public string? ReplacementsJson { get; set; }
    }
}
