using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest.Outfit
{
    public class OutfitImagesRequiredException() : BadRequestException("All 5 images are required and must not be empty")
    {

    }
}
