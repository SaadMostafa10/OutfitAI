using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OutfitDtos
{
    public class ProfileDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int TotalAnalyses { get; set; }
        public double AverageScore { get; set; }
    }
}
