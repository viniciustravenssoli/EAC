using EAC.Domain.Entities;
using EAC.Domain.Interfaces;
using EAC.Infra.Context;
using EAC.Infra.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Infra.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configurations)
        {
            var connectionString = configurations.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

    }
}
