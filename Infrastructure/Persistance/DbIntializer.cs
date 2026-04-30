using Domain.Contracts;
using Domain.Models.Outfit.outfits_recommendation;
using Microsoft.EntityFrameworkCore;
using Persistance.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance
{
    public class DbIntializer(OutfitIdentityDbContext _outfitDbContext) : IDbIntializer
    {
        public async Task InitializeAsync()
        {
            //1.Create database
            //2.Update database with migrations
            if (_outfitDbContext.Database.GetAppliedMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _outfitDbContext.Database.MigrateAsync();
            }
            //3.Seed data
            if (!_outfitDbContext.Outfits.Any())
            {
                //1. Read all data from json
                var outfitdata = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\final_all_200.json");

                //2. convert data from json to list of outfit
                var options = new JsonSerializerOptions
                {
                    NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
                    PropertyNameCaseInsensitive = true
                };

                var outfits = JsonSerializer.Deserialize<List<OutfitRecommendations>>(outfitdata, options);

                //3. add data to database
                if (outfits is not null && outfits.Count > 0)
                {
                    using var transaction = await _outfitDbContext.Database.BeginTransactionAsync();

                    await _outfitDbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [Outfits] ON");
                    await _outfitDbContext.Outfits.AddRangeAsync(outfits);
                    await _outfitDbContext.SaveChangesAsync();
                    await _outfitDbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [Outfits] OFF");

                    await transaction.CommitAsync();
                }
            }
        }
    }
}
