using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest.Outfit
{
    public class OutfitAnalysisFailedException() : BadRequestException("The outfit analysis failed due to an error in processing the images. Please try again later or contact support if the issue persists.")
    {
    }
}
