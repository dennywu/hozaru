using Hozaru.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Configurations
{
    public interface IUserManagementConfig
    {
        ITypeList<object> ExternalAuthenticationSources { get; set; }
    }
}
