using Domain.Models.Outfit.outfits_recommendation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.OutfitRecommendation
{
    public class SavedOutfitExistsSpec : BaseSpecifications<UserSavedOutfit>
    {
        public SavedOutfitExistsSpec(string userId, int outfitId)
            : base(x => x.UserId == userId && x.OutfitId == outfitId)
        {
        }
    }
}
