using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Identity;
using Persistance.Repositories;
using Services;
using Services.Abstractions.Outfit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
             
            services.AddDbContext<OutfitIdentityDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            //services.AddDbContext<OutfitIdentityDbContext>(options =>
            //options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")),
            //contextLifetime: ServiceLifetime.Singleton,
            //optionsLifetime: ServiceLifetime.Singleton);

            // 1. Register the UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // 2. Register the Generic Repository (Note the typeof syntax for open generics)
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IDbIntializer, DbIntializer>();
             

            return services;
        }
    }
}
