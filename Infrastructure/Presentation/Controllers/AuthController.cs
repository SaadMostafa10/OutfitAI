using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstractions;
using Shared.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager serviceManager) : ControllerBase
    {
        // login
        [HttpPost("login")] // POST: /api/auth/login
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await serviceManager.AuthService.LogInAsync(loginDto);
            return Ok(result);
        }

        // createacc
        [HttpPost("register")] // POST: /api/auth/register
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount(CreateAccountDto accountDto)
        {
            var result = await serviceManager.AuthService.CreateAccountAsync(accountDto);
            return Ok(result);
        }

        [HttpPost("forget-password")] // POST: /api/auth/forget-password
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
        {
            // Use the ServiceManager you created before to reach AuthService
            var token = await serviceManager.AuthService.ForgetPasswordAsync(forgetPasswordDto);

            // Returning a structured response instead of an anonymous object
            return Ok(AuthResponseDto.ForgetPasswordSuccess(token));
        }

        [HttpPost("reset-password")] // POST: /api/auth/reset-password
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var result = await serviceManager.AuthService.ResetPasswordAsync(resetPasswordDto);

            return Ok(AuthResponseDto.PasswordResetSuccess());

        }
    }
}
