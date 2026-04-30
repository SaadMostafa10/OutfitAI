using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OutfitDtos
{
    public class OutfitRecommendationsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }   
        public string Gender { get; set; }  
        public string Category { get; set; }  
        public List<string> Images { get; set; } 
        public string Description { get; set; }  
        public double AiScore { get; set; }
        public List<string> Tags { get; set; }  
     

    }
}
