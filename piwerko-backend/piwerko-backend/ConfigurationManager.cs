using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piwerko.Api
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
