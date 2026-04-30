using Domain.Models.Outfit.outfits_recommendation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configurations
{

    public class OutfitConfigurations : IEntityTypeConfiguration<OutfitRecommendations>
    {
        public void Configure(EntityTypeBuilder<OutfitRecommendations> builder)
        {
            builder.HasKey(o => o.Id);
 
            builder.Property(o => o.Category)
                .HasMaxLength(50)
                .IsRequired();

        }
    }
}
