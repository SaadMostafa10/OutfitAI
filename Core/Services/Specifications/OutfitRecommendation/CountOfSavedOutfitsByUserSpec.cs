using Domain.Models.Outfit.outfits_recommendation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.OutfitRecommendation
{
    public class CountOfSavedOutfitsByUserSpec : BaseSpecifications<UserSavedOutfit>
    {
        public CountOfSavedOutfitsByUserSpec(string userId , int pageIndex, int pageSize)
            : base(x => x.UserId == userId)
        {
            
        }
    }
}
  