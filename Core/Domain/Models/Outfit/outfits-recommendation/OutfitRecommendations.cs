using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Outfit.outfits_recommendation
{
    public class OutfitRecommendations
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new();
        public string Description { get; set; } = string.Empty;
        public double AiScore { get; set; }
        public List<string> Tags { get; set; } = new();
        public List<UserSavedOutfit> SavedByUsers { get; set; } = new();

    }
}
