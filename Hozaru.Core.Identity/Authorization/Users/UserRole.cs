using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Authorization.Users
{
    public class UserRole : CreationAuditedEntity<long>
    {
        /// <summary>
        /// User id.
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// Role id.
        /// </summary>
        public virtual int RoleId { get; set; }

        public virtual string RoleName { get; set; }

        /// <summary>
        /// Creates a new <see cref="UserRole"/> object.
        /// </summary>
        public UserRole()
        {

        }

        /// <summary>
        /// Creates a new <see cref="UserRole"/> object.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="roleId">Role id</param>
        public UserRole(long userId, int roleId, string roleName)
        {
            UserId = userId;
            RoleId = roleId;
            RoleName = roleName;
        }
    }
}
