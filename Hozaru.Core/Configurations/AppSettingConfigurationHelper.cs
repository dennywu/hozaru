using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hozaru.Core.Configurations
{
    public static class AppSettingConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            return configuration;
        }

        public static string GetConnectionString(string name)
        {
            var configuration = GetConfiguration();
            var connectionString = ConfigurationExtensions.GetConnectionString(configuration, name);
            return connectionString;
        }

        public static IConfigurationSection GetSection(string key)
        {
            var configuration = GetConfiguration();
            return configuration.GetSection(key);
        }
    }
}
