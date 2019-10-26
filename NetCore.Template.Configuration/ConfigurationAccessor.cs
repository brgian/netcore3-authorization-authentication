using Microsoft.Extensions.Configuration;

namespace NetCore.Template.Configuration
{
    public class ConfigurationAccessor
    {
        public ApiInformation ApiInformation => configuration.GetSection("ApiInformation")
            .Get<ApiInformation>();

        public string ConnectionString => configuration.GetValue<string>("ConnectionString");

        public bool DetailedErrors => configuration.GetValue<bool>("DetailedErrors");

        public SecurityConfiguration SecurityConfiguration => configuration.GetSection("SecurityConfiguration")
            .Get<SecurityConfiguration>();

        private readonly IConfiguration configuration;

        public ConfigurationAccessor(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
