using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NetCore.Template.Configuration;
using NetCore.Template.Repositories;
using NetCore.Template.Repositories.Implementation;
using NetCore.Template.Services;
using NetCore.Template.Services.Implementation;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace NetCore.Template.Api.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InjectCustomDependencies(this IServiceCollection services)
        {
            services.AddTransient<IMyEntityRepository, MyEntityRepository>();
            services.AddTransient<IMyEntityService, MyEntityService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<ConfigurationAccessor>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, ConfigurationAccessor configurationAccessor)
        {
            services
            .AddSwaggerGen(c =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                };

                c.AddSecurityDefinition("Bearer", securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } } });

                c.SwaggerDoc(configurationAccessor.ApiInformation.ApiVersion, new OpenApiInfo
                {
                    Title = configurationAccessor.ApiInformation.Title,
                    Version = configurationAccessor.ApiInformation.ApiVersion,
                    Description = configurationAccessor.ApiInformation.Description,
                    Contact = new OpenApiContact
                    {
                        Name = configurationAccessor.ApiInformation.ContactName,
                        Email = configurationAccessor.ApiInformation.ContactEmail
                    },
                    License = new OpenApiLicense
                    {
                        Name = configurationAccessor.ApiInformation.LicenseName
                    }
                }
                );
            });

            return services;
        }
    }
}
