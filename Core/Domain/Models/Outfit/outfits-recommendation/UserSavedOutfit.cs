using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Outfit.outfits_recommendation
{
    public class UserSavedOutfit
    {
        public string UserId { get; set; } 
        public int OutfitId { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
        public OutfitRecommendations Outfit { get; set; } = null!;
    }
}
