using Domain.Models.Outfit;
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
        public OutfitHistoryWithUserSpecification(string userId)
            : base(x => x.UserId == userId)
        {
            AddOrderByDescending(x => x.CreatedAt);
        }
    }
}
