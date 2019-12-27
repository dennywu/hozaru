using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Authorization.Users
{
    public class UserPermissionSetting : PermissionSetting
    {
        /// <summary>
        /// User id.
        /// </summary>
        public virtual long UserId { get; set; }
    }
}
