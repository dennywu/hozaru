using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Authorization.Roles
{
    /// <summary>
    /// Used to cache permissions of a role.
    /// </summary>
    [Serializable]
    public class RolePermissionCacheItem
    {
        public const string CacheStoreName = "HozaruIdentityRolePermissions";

        /// <summary>
        /// Gets/sets expire time for cache items.
        /// Default: 20 minutes.
        /// TODO: This is not used yet!
        /// </summary>
        public static TimeSpan CacheExpireTime { get; private set; }

        public long RoleId { get; set; }

        public HashSet<string> GrantedPermissions { get; set; }

        public HashSet<string> ProhibitedPermissions { get; set; }

        static RolePermissionCacheItem()
        {
            CacheExpireTime = TimeSpan.FromMinutes(120);
        }

        public RolePermissionCacheItem()
        {
            GrantedPermissions = new HashSet<string>();
            ProhibitedPermissions = new HashSet<string>();
        }

        public RolePermissionCacheItem(int roleId)
            : this()
        {
            RoleId = roleId;
        }
    }
}
