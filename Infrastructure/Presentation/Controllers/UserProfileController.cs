using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dtos.OutfitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController(IServiceManager serviceManager) : ControllerBase

    {
        [HttpGet("my-info")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProfileDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProfileDto>> GetMyProfile()
        {

            // 1. Extract UserId from JWT Claims

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            // 2. Call service through ServiceManager
            var profile = await serviceManager.UserProfileService.GetProfileAsync(userId);

            return Ok(profile);

        }

    }

}
