using Domain.Models.Outfit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configurations
{
    public class OutfitHistoryConfigurations : IEntityTypeConfiguration<OutfitHistory>
    {
        public void Configure(EntityTypeBuilder<OutfitHistory> builder)
        {
            // PK
            builder.HasKey(h => h.Id);

            builder.Property(h => h.OriginalScore).IsRequired();
            builder.Property(h => h.ImprovedScore);
            builder.Property(h => h.IsCompatible).IsRequired(); 
           
            builder.Property(h => h.TopImagePath).IsRequired();
            builder.Property(h => h.BottomImagePath).IsRequired();
            builder.Property(h => h.ShoeImagePath).IsRequired();      
            builder.Property(h => h.AccessoryImagePath).IsRequired(); 
            builder.Property(h => h.BagImagePath).IsRequired();       

            builder.Property(h => h.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()"); 

            // 5. Relationship with AppUser
            builder.HasOne(h => h.User)
                   .WithMany() 
                   .HasForeignKey(h => h.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(h => h.ReplacementsJson)
                   .HasColumnType("nvarchar(max)");
        }
    }
}
