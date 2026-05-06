using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
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
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OutfitRecommendationsController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OutfitRecommendationsDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<OutfitRecommendationsDto>>> GetRecommendations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
        
            var recommendations = await _serviceManager.RecommendationService.GetRecommendationsAsync(userId);
        
            return Ok(recommendations);
        }




        [HttpPost("saved/{outfitId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SavedOutfitResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<SavedOutfitResponseDto>> SaveOutfit(int outfitId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _serviceManager.RecommendationService.SaveOutfitAsync(userId, outfitId);
            return Ok(result);
        }


        [HttpGet("saved")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<SavedOutfitResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<PaginationResponse<SavedOutfitResponseDto>>> GetSavedOutfits(int pageIndex = 1, int pageSize = 10)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _serviceManager.RecommendationService.GetSavedOutfitsAsync(userId , pageIndex ,pageSize);
            return Ok(result);
        }


        [HttpDelete("saved/{outfitId}")]
        [ProducesResponseType(StatusCodes.Status200OK ,Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult> DeleteSavedOutfit(int outfitId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            await _serviceManager.RecommendationService.DeleteSavedOutfitAsync(userId, outfitId);
            return Ok(new { message = "Outfit removed successfully" });
        }
    }


}
