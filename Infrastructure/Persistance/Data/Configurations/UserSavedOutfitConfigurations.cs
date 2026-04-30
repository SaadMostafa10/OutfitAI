using Domain.Models.Outfit;
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
    public class UserSavedOutfitConfigurations : IEntityTypeConfiguration<UserSavedOutfit>
    {

        public void Configure(EntityTypeBuilder<UserSavedOutfit> builder)
        {
            builder.HasKey(u => new { u.UserId, u.OutfitId });

            builder.Property(u => u.UserId)
                .IsRequired();

            builder.Property(u => u.OutfitId)
                .IsRequired();

            builder.Property(u => u.SavedAt)
                .IsRequired();

            builder.HasOne(u => u.Outfit)
                .WithMany(o => o.SavedByUsers)
                .HasForeignKey(u => u.OutfitId);

            builder.HasIndex(u => u.UserId);
        }
    }
}
