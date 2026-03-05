using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthService(UserManager<AppUser>userManager, IOptions<JwtOptions> options ,IEmailSender emailSender) : IAuthService
    {
        public async Task<UserResultDto> CreateAccountAsync(CreateAccountDto accountDto)
        {
            var userFromDb = await userManager.FindByEmailAsync(accountDto.Email);

            if (userFromDb != null)
                throw new EmailAlreadyExistsException(accountDto.Email);

            if (!accountDto.IsAgree)
                throw new MustAgreeToTermsException();


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
                Token = await GenerateJwtToken(user)
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
                Token = await GenerateJwtToken(user)
            };
        }

        private async Task<string> GenerateJwtToken(AppUser user)
        {
            // Token => 
            // Header.
            // Payload. 
            // Signature

            var jwtOptions = options.Value;

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.Email , user.Email),
            };

            // Put Roles in Token throw authClaims
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }



            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: authClaims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)

                );

            // Now Token Created

            // Encrypt This Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> ForgetPasswordAsync(ForgetPasswordDto forgetPasswordDto)
        {
            // 1. Verify if the user exists in the database 
            var user = await userManager.FindByEmailAsync(forgetPasswordDto.Email);
            if (user == null) throw new UnAuthorizedException();

            // 2. Generate a unique password reset token 
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            // 3. Prepare the email message with the token 
            ////// will change to send link 
            var emailModel = new Email
            {
                To = forgetPasswordDto.Email,
                Subject = "Reset Your Password",
                Body = $"Your reset token is: {token}" // Basic text body as requested 
            };

            // 4. Send the email 
            await emailSender.SendEmailAsync(emailModel);

            // 5. Return the token for Postman testing 
            return token;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null) throw new UnAuthorizedException();

            // Validate token and update password
            var result = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ValidationException(errors);
            }

            return true;
        }
    }
}
