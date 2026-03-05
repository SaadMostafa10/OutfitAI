using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthService
    {
        Task<UserResultDto> LogInAsync(LoginDto loginDto);
        Task<UserResultDto> CreateAccountAsync(CreateAccountDto accountDto);
        Task<string> ForgetPasswordAsync(ForgetPasswordDto forgetPasswordDto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

    }
}
