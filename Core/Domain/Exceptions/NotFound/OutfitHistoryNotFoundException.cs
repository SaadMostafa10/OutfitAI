using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFound
{
    public class OutfitHistoryNotFoundException : NotFoundException
    {
        // Constructor that takes the ID and passes a clean message to the base Exception
        public OutfitHistoryNotFoundException(int historyId)
            : base($"Outfit history with ID {historyId} was not found.")
        {
        }
    }
}
