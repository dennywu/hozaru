using Hozaru.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Configurations.Startup
{
    internal class SettingsConfiguration : ISettingsConfiguration
    {
        public ITypeList<SettingProvider> Providers { get; private set; }

        public SettingsConfiguration()
        {
            Providers = new TypeList<SettingProvider>();
        }
    }
}
