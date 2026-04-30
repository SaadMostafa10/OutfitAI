using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest.Outfit
{
    public class Outfitalreadysaved() : BadRequestException("this outfit is already saved")
    {
    }
}
