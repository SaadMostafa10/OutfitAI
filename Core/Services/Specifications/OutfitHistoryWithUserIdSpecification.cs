using Domain.Models.Outfit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OutfitHistoryWithUserIdSpecification : BaseSpecifications<OutfitHistory>
    {
        public OutfitHistoryWithUserIdSpecification(string userId)
            : base(h => h.UserId == userId)
        {
        }
    }
}
