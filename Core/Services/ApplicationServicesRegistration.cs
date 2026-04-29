using Domain.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using Services.Abstractions.Outfit;
using Services.Abstractions.URLService;
using Services.Auth;
using Services.MappingProfile;
using Services.Outfit;
using Services.UrlService;
using Shared.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IAuthService, AuthService>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddAutoMapper(typeof(Services.MappingProfile.MappingProfile).Assembly);
            services.AddScoped<ImageUrlResolver>();
            services.AddScoped<IAttachmentService, Services.AttachmentService.AttachmentService>();
            services.AddHttpClient<IOutfitService, OutfitService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IUrlService, Services.UrlService.UrlService>();
            return services;
        }
    }
}
