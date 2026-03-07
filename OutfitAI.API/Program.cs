
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


            builder.Services.AddHttpClient<IOutfitService, OutfitService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["AIModel:BaseUrl"]);
                client.Timeout = TimeSpan.FromSeconds(60);
            });

            // Suppress automatic model state validation to allow custom error handling
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });


            var app = builder.Build();

            // Middlewares

            app.ConfigureMiddlewares();

            

            app.Run();
        }
    }
}
