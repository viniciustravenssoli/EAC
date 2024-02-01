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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using EAC.Application.Email;

namespace EAC.Application.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddSingleton<SmtpClient>(provider =>
            {
                var emailSettings = provider.GetRequiredService<IOptions<EmailSettings>>().Value;

                return new SmtpClient(emailSettings.SmtpServer)
                {
                    Port = emailSettings.SmtpPort,
                    Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password),
                    EnableSsl = true,
                };
            });
            
            services.AddHttpContextAccessor();
            services.AddScoped<IEmailService, EmailService>();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining(typeof(CreateAddressCommandValidator));

            services.AddScoped<ITokenGenerator, TokenGenerator>();


            return services;
        }
    }
}
