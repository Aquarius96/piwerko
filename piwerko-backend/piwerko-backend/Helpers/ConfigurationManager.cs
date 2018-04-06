using Microsoft.Extensions.Configuration;

namespace Piwerko.Api.Helpers
{
    public class ConfigurationManager 
    {
        private readonly IConfiguration _configuration;

        public ConfigurationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetValue(string key)
        {
            //to jest to samo co w startup
            return _configuration[key];
        }

    }
}
