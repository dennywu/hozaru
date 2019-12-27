﻿using Hozaru.Core.Domain.Entities.Auditing;
using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Core.Identity.MultiTenancy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.Core.Identity.Authorization.Roles
{
    /// <summary>
    /// Represents a role in an application. A role is used to group permissions.
    /// </summary>
    /// <remarks> 
    /// Application should use permissions to check if user is granted to perform an operation.
    /// Checking 'if a user has a role' is not possible until the role is static (<see cref="IsStatic"/>).
    /// Static roles can be used in the code and can not be deleted by users.
    /// Non-static (dynamic) roles can be added/removed by users and we can not know their name while coding.
    /// A user can have multiple roles. Thus, user will have all permissions of all assigned roles.
    /// </remarks>
    public class HozaruRole<TTenant, TUser> : HozaruRoleBase, IFullAudited<TUser>, IAudited<TUser>, IMayHaveTenant<TTenant, TUser>
        where TUser : HozaruUser<TTenant, TUser>
        where TTenant : HozaruTenant<TTenant, TUser>
    {
        /// <summary>
        /// Maximum length of the <see cref="DisplayName"/> property.
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// The Tenant, if this role is a tenant-level role.
        /// </summary>
        public virtual TTenant Tenant { get; set; }

        /// <summary>
        /// Display name of this role.
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Is this a static role?
        /// Static roles can not be deleted, can not change their name.
        /// They can be used programmatically.
        /// </summary>
        public virtual bool IsStatic { get; set; }

        /// <summary>
        /// Is this role will be assigned to new users as default?
        /// </summary>
        public virtual bool IsDefault { get; set; }

        /// <summary>
        /// List of permissions of the role.
        /// </summary>
        public virtual ICollection<RolePermissionSetting> Permissions { get; set; }

        public virtual TUser DeleterUser { get; set; }

        public virtual TUser CreatorUser { get; set; }

        public virtual TUser LastModifierUser { get; set; }

        /// <summary>
        /// Creates a new <see cref="HozaruRole{TTenant,TUser}"/> object.
        /// </summary>
        public HozaruRole()
        {
            Name = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Creates a new <see cref="HozaruRole{TTenant,TUser}"/> object.
        /// </summary>
        /// <param name="tenantId">TenantId or null (if this is not a tenant-level role)</param>
        /// <param name="displayName">Display name of the role</param>
        public HozaruRole(int? tenantId, string displayName)
            : this()
        {
            TenantId = tenantId;
            DisplayName = displayName;
        }

        /// <summary>
        /// Creates a new <see cref="HozaruRole{TTenant,TUser}"/> object.
        /// </summary>
        /// <param name="tenantId">TenantId or null (if this is not a tenant-level role)</param>
        /// <param name="name">Unique role name</param>
        /// <param name="displayName">Display name of the role</param>
        public HozaruRole(int? tenantId, string name, string displayName)
            : this(tenantId, displayName)
        {
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("[Role {0}, Name={1}]", Id, Name);
        }
    }
}
