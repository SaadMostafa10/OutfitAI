using Domain.Models.Outfit;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OutfitHistoryWithUserSpecification : BaseSpecifications<OutfitHistory>
    {
        // Filtering by UserId and sorting by latest CreatedAt
        public OutfitHistoryWithUserSpecification(string userId, OutfitHistorySpecificationsParameters specParams)
            : base(x => x.UserId == userId)
        {
            AddOrderByDescending(x => x.CreatedAt);
            ApplyPagination(specParams.PageIndex, specParams.PageSize);
        }
        public OutfitHistoryWithUserSpecification(string userId)
        : base(h => h.UserId == userId)
        {
        }
    }
}
