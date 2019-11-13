using Hozaru.Core.Authorization;
using Hozaru.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Configurations.Startup
{
    public class AuthorizationConfiguration : IAuthorizationConfiguration
    {
        public ITypeList<AuthorizationProvider> Providers { get; private set; }

        public AuthorizationConfiguration()
        {
            Providers = new TypeList<AuthorizationProvider>();
        }
    }
}
