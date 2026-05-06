using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Outfit;
using Shared;
using Shared.Dtos.AuthDtos;
using Shared.Dtos.OutfitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OutfitAI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Tags("Outfit")]
    public class OutfitController(IOutfitService outfitService) : ControllerBase
    {
        [HttpPost("analyze")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OutfitResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Analyze([FromForm] OutfitRequestDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User ID not found in token." });

            var result = await outfitService.AnalyzeAndSaveOutfitAsync(userId, request);

            return Ok(result);
        }

        [HttpGet("history")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OutfitHistoryDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetHistory([FromQuery] OutfitHistorySpecificationsParameters specParams)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var history = await outfitService.GetUserHistoryAsync(userId,specParams);

            return Ok(history);
        }

        [HttpDelete("history/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var success = await outfitService.DeleteHistoryItemAsync(userId, id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}

