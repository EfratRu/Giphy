using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Giphy.API
{
    public class ConfigurationHelper
    {
        private static IConfiguration configuration;

        public static T GetValue<T>(string key)
        {
            return configuration.GetValue<T>(key);
        }

        public static void InitConfiguration(IConfiguration iConfiguration)
        {
            configuration = iConfiguration;
        }
    }
}
