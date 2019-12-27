using Hozaru.Core.Collections;
using Hozaru.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Configuration
{
    internal class UserManagementConfig : IUserManagementConfig
    {
        public ITypeList<object> ExternalAuthenticationSources { get; set; }

        public UserManagementConfig()
        {
            ExternalAuthenticationSources = new TypeList();
        }
    }
}
