using EAC.Application.Token;
using EAC.Application.Validations.Addressess;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Application.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining(typeof(CreateAddressCommandValidator));
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            return services;
        }
    }
}
