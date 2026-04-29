using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OutfitDtos
{
    public class OutfitHistoryDto
    {
        public int Id { get; set; }
        public float OriginalScore { get; set; }
        public float ImprovedScore { get; set; }
        public bool IsCompatible { get; set; }
        public DateTime CreatedAt { get; set; }

        public string TopImagePath { get; set; } = string.Empty;
        public string BottomImagePath { get; set; } = string.Empty;
        public string ShoeImagePath { get; set; } = string.Empty;
        public string? AccessoryImagePath { get; set; } = string.Empty;
        public string? BagImagePath { get; set; } = string.Empty;

        public Dictionary<string, string>? Replacements { get; set; }
    }
}
