using Hozaru.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Configuration
{
    public interface IHozaruIdentityConfig
    {
        /// <summary>
        /// Gets role management config.
        /// </summary>
        IRoleManagementConfig RoleManagement { get; }

        /// <summary>
        /// Gets user management config.
        /// </summary>
        IUserManagementConfig UserManagement { get; }

        //ILanguageManagementConfig LanguageManagement { get; }
    }
}