using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NetCore.Template.Configuration;
using NetCore.Template.Repositories;
using NetCore.Template.Repositories.Implementation;
using NetCore.Template.Services;
using NetCore.Template.Services.Implementation;
using Swashbuckle.AspNetCore.Swagger;

namespace NetCore.Template.Api.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InjectCustomDependencies(this IServiceCollection services)
        {
            services.AddTransient<IMyEntityRepository, MyEntityRepository>();
            services.AddTransient<IMyEntityService, MyEntityService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<ConfigurationAccessor>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, ConfigurationAccessor configurationAccessor)
        {
            services
            .AddSwaggerGen(c =>
            {
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
                c.DescribeAllEnumsAsStrings();
            });

            return services;
        }
    }
}
