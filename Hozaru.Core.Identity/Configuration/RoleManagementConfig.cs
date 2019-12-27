using Hozaru.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Configuration
{
    internal class RoleManagementConfig : IRoleManagementConfig
    {
        public List<StaticRoleDefinition> StaticRoles { get; private set; }

        public RoleManagementConfig()
        {
            StaticRoles = new List<StaticRoleDefinition>();
        }
    }
}
