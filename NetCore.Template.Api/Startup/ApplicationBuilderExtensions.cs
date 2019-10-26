using Microsoft.AspNetCore.Builder;
using NetCore.Template.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace NetCore.Template.Api.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, ConfigurationAccessor configurationAccessor)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{configurationAccessor.ApiInformation.ApiVersion}/swagger.json", configurationAccessor.ApiInformation.Title);
                c.DocumentTitle = configurationAccessor.ApiInformation.Title;
                c.DocExpansion(DocExpansion.List);
            });

            return app;
        }
    }
}
