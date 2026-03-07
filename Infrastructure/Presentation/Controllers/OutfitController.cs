using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Abstractions.Outfit;
using Shared;
using Shared.Dtos.OutfitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OutfitController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpPost("analyze")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OutfitResultDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult> AnalyzeOutfit([FromForm] OutfitRequestDto request)
        {
              
             var result = await _serviceManager.OutfitService.AnalyzeOutfitAsync(request);
             return Ok(result);
             
        }  
    }
}
