using Shared.Dtos.OutfitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IUserProfileService
    {
        Task<ProfileDto> GetProfileAsync(string userId);
    }
}
