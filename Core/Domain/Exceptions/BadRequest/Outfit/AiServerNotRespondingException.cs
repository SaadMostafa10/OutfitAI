using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest.Outfit
{
    public class AiServerNotRespondingException : BadRequestException
    {
        public AiServerNotRespondingException()
            : base("The AI analysis server is currently unavailable. Please try again later.")
        {
        }
    }
}
