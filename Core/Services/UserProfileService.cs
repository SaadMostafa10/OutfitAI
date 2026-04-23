using Domain.Contracts;
using Domain.Exceptions.NotFound;
using Domain.Models.Identity;
using Domain.Models.Outfit;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Services.Specifications;
using Shared.Dtos.OutfitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserProfileService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager) : IUserProfileService
    {
        public async Task<ProfileDto> GetProfileAsync(string userId)
        {
            // 1. Get user data from Identity
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) throw new UserNotFoundException();

            // 2. Use Specification to filter OutfitHistory by UserId
            var spec = new OutfitHistoryWithUserIdSpecification(userId);

            // 3. Fetch filtered data using the Generic Repository
            var userHistory = await unitOfWork.Repository<OutfitHistory>().GetAllWithSpecAsync(spec);

            // 4. Map data to ProfileDto and calculate statistics
            return new ProfileDto
            {
                FullName = user.FullName ?? user.UserName,
                Email = user.Email,
                TotalAnalyses = userHistory.Count(),
                //AverageScore = userHistory.Any()
                //               ? Math.Round(userHistory.Average(h => h.ImprovedScore ?? 0), 1)
                //               : 0

                AverageScore = userHistory.Any(h => h.OriginalScore > 0)
                       ? (float)Math.Round(userHistory.Where(h => h.OriginalScore > 0)
                                                .Average(h => h.OriginalScore) * 100, 1)
                       : 0f
            };
        }
    }
}
