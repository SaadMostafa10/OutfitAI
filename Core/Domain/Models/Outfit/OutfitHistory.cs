using Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Outfit
{
    public class OutfitHistory
    {
        public int Id { get; set; } // PK
        public string UserId { get; set; } // FK
        public AppUser User { get; set; } // Navigation Property

        // Scores and Status
        public float OriginalScore { get; set; }
        public float? ImprovedScore { get; set; }
        public bool IsCompatible { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Image Paths (Strings)
        public string TopImagePath { get; set; }
        public string BottomImagePath { get; set; }
        public string ShoeImagePath { get; set; }
        public string AccessoryImagePath { get; set; }
        public string BagImagePath { get; set; }

        // AI Suggestions stored as JSON string
        public string? ReplacementsJson { get; set; }
    }
}
