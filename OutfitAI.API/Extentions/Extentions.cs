//using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services;
using OutfitAI.API.Middleware;
using Persistance;
using Persistance.Identity;
namespace OutfitAI.API.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInServices();

            services.AddSwaggerServices();

            services.AddInfrastructureServices(configuration);

            services.AddIdentityServices();
            
            services.AddApplicationServices(configuration);

            // TO DO 
            // .ConfigureServices()

            return services;
        }
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<OutfitIdentityDbContext>()
                    .AddDefaultTokenProviders(); 

            return services;
        }

        // TO DO 
        // private static IServiceCollection ConfigureServices()

        public static WebApplication ConfigureMiddlewares(this WebApplication app)
        {
            

            app.UseGlobalErrorHandling(); // User-Defined Middleware

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }

        
        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
        }
    }
}
