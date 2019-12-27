using Hozaru.Core.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Configurations
{
    /// <summary>
    /// Extension methods for <see cref="ICacheManager"/> to get setting caches.
    /// </summary>
    public static class CacheManagerSettingExtensions
    {
        /// <summary>
        /// Gets application settings cache.
        /// </summary>
        public static ITypedCache<string, Dictionary<string, SettingInfo>> GetApplicationSettingsCache(this ICacheManager cacheManager)
        {
            return cacheManager
                .GetCache<string, Dictionary<string, SettingInfo>>(HozaruCacheNames.ApplicationSettings);
        }

        /// <summary>
        /// Gets tenant settings cache.
        /// </summary>
        public static ITypedCache<int, Dictionary<string, SettingInfo>> GetTenantSettingsCache(this ICacheManager cacheManager)
        {
            return cacheManager
                .GetCache<int, Dictionary<string, SettingInfo>>(HozaruCacheNames.TenantSettings);
        }

        /// <summary>
        /// Gets user settings cache.
        /// </summary>
        public static ITypedCache<long, Dictionary<string, SettingInfo>> GetUserSettingsCache(this ICacheManager cacheManager)
        {
            return cacheManager
                .GetCache<long, Dictionary<string, SettingInfo>>(HozaruCacheNames.UserSettings);
        }
    }
}
