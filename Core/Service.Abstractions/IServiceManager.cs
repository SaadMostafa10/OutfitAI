using Services.Abstractions.Outfit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IAuthService AuthService { get; }
        IOutfitService OutfitService { get; }
        IUserProfileService UserProfileService { get; }
        IRecommendationService RecommendationService { get; }
    }
}
