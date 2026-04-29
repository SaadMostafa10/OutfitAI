
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OutfitAI.API.Extentions;
using Persistance.Identity;
using Services.Abstractions.Outfit;
using Services.Outfit;

namespace OutfitAI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();

            // Middlewares

            app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
