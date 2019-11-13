using Hozaru.Core.Configurations.Startup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.NHibernate.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Hozaru NHibernate module.
    /// </summary>
    public static class HozaruNHibernateConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Hozaru NHibernate module.
        /// </summary>
        public static IHozaruNHibernateModuleConfiguration HozaruNHibernate(this IModuleConfigurations configurations)
        {
            return configurations.HozaruConfiguration.GetOrCreate("Modules.Hozaru.NHibernate", () => new HozaruNHibernateModuleConfiguration());
        }
    }
}
