using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Configurations.Startup
{
    /// <summary>
    /// Used to provide a way to configure modules.
    /// Create entension methods to this class to be used over <see cref="IHozaruStartupConfiguration.Modules"/> object.
    /// </summary>
    public interface IModuleConfigurations
    {
        /// <summary>
        /// Gets the Hozaru configuration object.
        /// </summary>
        IHozaruStartupConfiguration HozaruConfiguration { get; }
    }
}
