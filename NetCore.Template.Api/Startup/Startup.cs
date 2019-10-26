using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetCore.Template.Api.Configuration;
using NetCore.Template.Configuration;
using NetCore.Template.Context;
using NetCore.Template.Infrastructure;
using NetCore.Template.Repositories;
using NetCore.Template.Services.Helper;

namespace NetCore.Template.Api
{
    public class Startup
    {
        private readonly ConfigurationAccessor configurationAccessor;

        public Startup(IConfiguration configuration)
        {
            configurationAccessor = new ConfigurationAccessor(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = true,
                          ValidateAudience = true,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,

                          ValidIssuer = configurationAccessor.SecurityConfiguration.Issuer,
                          ValidAudience = configurationAccessor.SecurityConfiguration.Audience,
                          IssuerSigningKey = SecurityHelper.GetSecurityKey(configurationAccessor.SecurityConfiguration.SigningKey)
                      };
                  });

            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(configurationAccessor.ConnectionString));
            services.AddSwagger(configurationAccessor);
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.InjectCustomDependencies();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.ConfigureSwagger(configurationAccessor);
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action=index}/{id}");
            });
        }
    }
}