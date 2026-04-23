using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest.Outfit
{
    public class InvalidImageFileException() : BadRequestException("One or more uploaded files are not valid image files. Please ensure all files are in a supported image format (e.g., JPEG, PNG) and try again.")
    {
    }
}
