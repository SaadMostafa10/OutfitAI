using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Outfit.outfits_recommendation;

namespace Services.Specifications.OutfitRecommendation
{
    public class OutfitRecommendationSpec : BaseSpecifications<OutfitRecommendations>
    {
        public OutfitRecommendationSpec(string? category,List<int> excludedIds) : base(o =>
                                           (string.IsNullOrEmpty(category) || o.Category == category)
                                           &&
                                           !excludedIds.Contains(o.Id)
                                       )
        {
            AddOrderBy(o => Guid.NewGuid());
             
        }

        public OutfitRecommendationSpec(int id): base(o => o.Id == id)
        {
        } 


    }
}
