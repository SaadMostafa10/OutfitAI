using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Dtos.OutfitDtos
{
    public class OutfitResultDto
    {
        [JsonPropertyName("original_score")]
        public float OriginalScore { get; set; }


        [JsonPropertyName("improved_score")]
        public float? ImprovedScore { get; set; }


        [JsonPropertyName("is_compatible")]
        public bool IsCompatible { get; set; }


        [JsonPropertyName("highlights")]
        public List<int>? Highlights { get; set; }


        [JsonPropertyName("replacements")]
        public Dictionary<string, string>? Replacements { get; set; }

    }
      
}
