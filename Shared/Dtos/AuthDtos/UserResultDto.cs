using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.AuthDtos
{
    public class UserResultDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
    }
}
