
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OutfitAI.API.Extentions;
using Persistance.Identity;
using Services.Abstractions.Outfit;
using Services.Outfit;
using System.Threading.Tasks;

namespace OutfitAI.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();

            // Middlewares

            await app.ConfigureMiddlewaresAsync();

            app.Run();
        }
    }
}
