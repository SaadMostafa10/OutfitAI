using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager serviceManager) : ControllerBase
    {
        // login
        [HttpPost("login")] // POST: /api/auth/login
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await serviceManager.AuthService.LogInAsync(loginDto);
            return Ok(result);
        }

        // createacc
        [HttpPost("register")] // POST: /api/auth/register
        public async Task<IActionResult> CreateAccount(CreateAccountDto accountDto)
        {
            var result = await serviceManager.AuthService.CreateAccountAsync(accountDto);
            return Ok(result);
        }
    }
}
