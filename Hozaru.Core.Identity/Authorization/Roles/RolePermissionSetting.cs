using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Authorization.Roles
{
    public class RolePermissionSetting : PermissionSetting
    {
        /// <summary>
        /// Role id.
        /// </summary>
        public virtual int RoleId { get; set; }
    }
}
