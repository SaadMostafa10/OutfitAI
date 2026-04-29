using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.URLService
{
    public interface IUrlService
    {
        string BuildImageUrl(string? fileName);
    }
}
