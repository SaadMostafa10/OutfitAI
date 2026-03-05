using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class AuthResponseDto
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
       
        public static AuthResponseDto PasswordResetSuccess() => new()
        {
            Message = "Password has been reset successfully. You can now login with your new password.",
            IsSuccess = true,
            Token = null
        };


        public static AuthResponseDto ForgetPasswordSuccess(string token) => new()
        {
            Message = "Reset token generated successfully. Use this token to reset your password.",
            IsSuccess = true,
            Token = token
        };
    }
}
