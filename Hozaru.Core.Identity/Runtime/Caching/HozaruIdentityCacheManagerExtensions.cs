using Hozaru.Core.Identity.Application.Editions;
using Hozaru.Core.Identity.Authorization.Roles;
using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Core.Identity.MultiTenancy;
using Hozaru.Core.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Runtime.Caching
{
    public static class HozaruIdentityCacheManagerExtensions
    {
        public static ITypedCache<long, UserPermissionCacheItem> GetUserPermissionCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<long, UserPermissionCacheItem>(UserPermissionCacheItem.CacheStoreName);
        }

        public static ITypedCache<int, RolePermissionCacheItem> GetRolePermissionCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<int, RolePermissionCacheItem>(RolePermissionCacheItem.CacheStoreName);
        }

        public static ITypedCache<int, TenantFeatureCacheItem> GetTenantFeatureCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<int, TenantFeatureCacheItem>(TenantFeatureCacheItem.CacheStoreName);
        }

        public static ITypedCache<int, EditionfeatureCacheItem> GetEditionFeatureCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<int, EditionfeatureCacheItem>(EditionfeatureCacheItem.CacheStoreName);
        }
    }
}
