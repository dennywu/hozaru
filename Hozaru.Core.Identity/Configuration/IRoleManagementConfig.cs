using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Configurations
{
    public interface IRoleManagementConfig
    {
        List<StaticRoleDefinition> StaticRoles { get; }
    }
}
