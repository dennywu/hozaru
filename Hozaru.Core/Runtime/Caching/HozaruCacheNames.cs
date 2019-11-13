using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Runtime.Caching
{
    /// <summary>
    /// Names of standard caches used in Hozaru.
    /// </summary>
    public static class HozaruCacheNames
    {
        /// <summary>
        /// Application settings cache: HozaruApplicationSettingsCache.
        /// </summary>
        public const string ApplicationSettings = "HozaruApplicationSettingsCache";

        /// <summary>
        /// Tenant settings cache: HozaruTenantSettingsCache.
        /// </summary>
        public const string TenantSettings = "HozaruTenantSettingsCache";

        /// <summary>
        /// User settings cache: HozaruUserSettingsCache.
        /// </summary>
        public const string UserSettings = "HozaruUserSettingsCache";

        /// <summary>
        /// Localization scripts cache: HozaruLocalizationScripts.
        /// </summary>
        public const string LocalizationScripts = "HozaruLocalizationScripts";
    
    }
}
