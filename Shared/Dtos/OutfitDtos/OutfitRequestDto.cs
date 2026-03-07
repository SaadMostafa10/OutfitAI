using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OutfitDtos
{
    public class OutfitRequestDto
    {
        public IFormFile  Top { get; set; }
        public IFormFile  Bottom { get; set; }
        public IFormFile  Shoe { get; set; }
        public IFormFile  Accessory { get; set; }
        public IFormFile  Bag { get; set; }
    }
}
