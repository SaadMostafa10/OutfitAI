using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthService(UserManager<AppUser> userManager) : IAuthService
    {
        public async Task<UserResultDto> CreateAccountAsync(CreateAccountDto accountDto)
        {
            var user = new AppUser()
            {
                Email = accountDto.Email,
                UserName = accountDto.Email,
                FullName = accountDto.FullName,
                IsAgree = accountDto.IsAgree,
            };
            var result = await userManager.CreateAsync(user, accountDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(errors => errors.Description);
                throw new ValidationException(errors);
            }

            return new UserResultDto
            {
                Email = accountDto.Email,
                FullName = accountDto.FullName,
                Token = "TOKEN"
            };

        }

        public async Task<UserResultDto> LogInAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new UnAuthorizedException();

            var flag = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!flag) throw new UnAuthorizedException();

            return new UserResultDto
            {
                Email = user.Email,
                FullName = user.FullName,
                Token = "TOKEN"
            };
        }
    }
}
