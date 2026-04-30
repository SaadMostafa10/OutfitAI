using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFound.Outfit
{
    public class OutfitNotFoundException() : NotFoundException("Outfit not found")
    {
    }
}
