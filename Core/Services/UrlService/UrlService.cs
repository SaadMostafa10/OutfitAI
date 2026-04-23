using Microsoft.Extensions.Configuration;
using Services.Abstractions.URLService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UrlService
{
    public class UrlService(IConfiguration configuration) : IUrlService
    {
        public string BuildImageUrl(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            return $"{configuration["BaseUrl"]}/files/outfits/{fileName}";
        }
    }
}
