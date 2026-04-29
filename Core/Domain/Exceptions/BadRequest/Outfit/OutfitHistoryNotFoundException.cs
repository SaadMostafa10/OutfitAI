using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest.Outfit
{
    public class OutfitHistoryNotFoundException : BadRequestException
    {
        public OutfitHistoryNotFoundException(int id)
            : base($"The outfit history item with ID: {id} was not found or you don't have permission to access it.")
        {
        }
    }
}
