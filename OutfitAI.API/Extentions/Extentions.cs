//using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OutfitAI.API.Middleware;
using Persistance;
using Persistance.Identity;
using Services;
using Services.Abstractions.Outfit;
using Services.Outfit;
using System.Text;
namespace OutfitAI.API.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInServices();
            services.AddSwaggerServices();
            services.AddControllers();
            services.AddInfrastructureServices(configuration);
            services.AddIdentityServices(configuration);
            services.AddApplicationServices(configuration);
            services.AddAIIntegration(configuration);
            services.AddMvcConfiguration();
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

            //Configures Swagger to support JWT Bearer authentication by adding the Authorize button 
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your token"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

            });

            return services;
        }
        private static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Register Identity Requirements
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<OutfitIdentityDbContext>()
                    .AddDefaultTokenProviders();

            // 2. Add Authentication Scheme to the system
            services.AddAuthentication(options =>
            {
                // Set Default Schemes to JWT Bearer
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // 3. Configure Token Validation 
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtOptions:Issuer"], 

                    ValidateAudience = true,
                    ValidAudience = configuration["JwtOptions:Audience"], 

                    ValidateLifetime = true, // Check if token is expired

                    ValidateIssuerSigningKey = true,
                    // Key must match the one used in GenerateJwtToken method in AuthService
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:SecretKey"]))
                };
            });

            return services;
        }

        // TO DO 
        // private static IServiceCollection ConfigureServices()

        public static WebApplication ConfigureMiddlewares(this WebApplication app)
        {
            

            

            app.UseSwagger();
            app.UseSwaggerUI();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

            app.UseStaticFiles();
           
            app.UseHttpsRedirection();
            //
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGlobalErrorHandling(); // User-Defined Middleware

            app.MapControllers();

            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
        }

        private static IServiceCollection AddAIIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            // If you change the model later, you only change this block
            services.AddHttpClient<IOutfitService, OutfitService>(client =>
            {
                client.BaseAddress = new Uri(configuration["AIModel:BaseUrl"]);
                client.Timeout = TimeSpan.FromSeconds(60);
            });
            return services;
        }
        private static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            return services;
        }
    }
}
