﻿using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Core.Identity.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Core.Identity.Authorization.Roles
{
    /// <summary>
    /// Used to perform permission database operations for a role.
    /// </summary>
    public interface IRolePermissionStore<TTenant, TRole, TUser>
        where TRole : HozaruRole<TTenant, TUser>
        where TUser : HozaruUser<TTenant, TUser>
        where TTenant : HozaruTenant<TTenant, TUser>
    {
        /// <summary>
        /// Adds a permission grant setting to a role.
        /// </summary>
        /// <param name="role">Role</param>
        /// <param name="permissionGrant">Permission grant setting info</param>
        Task AddPermissionAsync(TRole role, PermissionGrantInfo permissionGrant);

        /// <summary>
        /// Removes a permission grant setting from a role.
        /// </summary>
        /// <param name="role">Role</param>
        /// <param name="permissionGrant">Permission grant setting info</param>
        Task RemovePermissionAsync(TRole role, PermissionGrantInfo permissionGrant);

        /// <summary>
        /// Gets permission grant setting informations for a role.
        /// </summary>
        /// <param name="role">Role</param>
        /// <returns>List of permission setting informations</returns>
        Task<IList<PermissionGrantInfo>> GetPermissionsAsync(TRole role);

        /// <summary>
        /// Gets permission grant setting informations for a role.
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <returns>List of permission setting informations</returns>
        Task<IList<PermissionGrantInfo>> GetPermissionsAsync(int roleId);

        /// <summary>
        /// Checks whether a role has a permission grant setting info.
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <param name="permissionGrant">Permission grant setting info</param>
        /// <returns></returns>
        Task<bool> HasPermissionAsync(int roleId, PermissionGrantInfo permissionGrant);

        /// <summary>
        /// Deleted all permission settings for a role.
        /// </summary>
        /// <param name="role">Role</param>
        Task RemoveAllPermissionSettingsAsync(TRole role);
    }
}
