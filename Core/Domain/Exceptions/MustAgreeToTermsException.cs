using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class MustAgreeToTermsException : Exception
    {
        public MustAgreeToTermsException()
            : base("You Must Agree To Terms")
        {
        }
    }
}
