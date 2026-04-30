using Domain.Models.Outfit.outfits_recommendation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.OutfitRecommendation
{
    public class SavedOutfitsByUserSpec : BaseSpecifications<UserSavedOutfit>
    {
        public SavedOutfitsByUserSpec(string userId)
            : base(x => x.UserId == userId)
        {
            Includes.Add(x => x.Outfit);
            
        }
        public SavedOutfitsByUserSpec(string userId, int pageIndex, int pageSize)
                : base(x => x.UserId == userId)
        {
            Includes.Add(x => x.Outfit);
            ApplyPagination(pageIndex, pageSize);

        }
    }
}
