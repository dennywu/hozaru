using Hozaru.Core.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Configurations
{
    public class StaticRoleDefinition
    {
        public string RoleName { get; private set; }

        public MultiTenancySides Side { get; private set; }

        public StaticRoleDefinition(string roleName, MultiTenancySides side)
        {
            RoleName = roleName;
            Side = side;
        }
    }
}
