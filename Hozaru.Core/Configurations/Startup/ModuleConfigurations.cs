using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Configurations.Startup
{
    internal class ModuleConfigurations : IModuleConfigurations
    {
        public IHozaruStartupConfiguration HozaruConfiguration { get; private set; }

        public ModuleConfigurations(IHozaruStartupConfiguration hozaruConfiguration)
        {
            HozaruConfiguration = hozaruConfiguration;
        }
    }
}
