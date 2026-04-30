using Domain.Models.Identity;
using Domain.Models.Outfit;
using Domain.Models.Outfit.outfits_recommendation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistance.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Identity
{
    public class OutfitIdentityDbContext(DbContextOptions<OutfitIdentityDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<OutfitHistory> OutfitHistories { get; set; }

        public DbSet<OutfitRecommendations> Outfits { get; set; }
        public DbSet<UserSavedOutfit> UserSavedOutfits { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // This single line will find all classes implementing IEntityTypeConfiguration 
        //    // in the current assembly and apply them automatically.
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutfitHistoryConfigurations).Assembly);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


    }
}
