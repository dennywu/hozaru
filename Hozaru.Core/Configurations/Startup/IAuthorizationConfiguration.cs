using Hozaru.Core.Authorization;
using Hozaru.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Configurations.Startup
{
    /// <summary>
    /// Used to configure authorization system.
    /// </summary>
    public interface IAuthorizationConfiguration
    {
        /// <summary>
        /// List of authorization providers.
        /// </summary>
        ITypeList<AuthorizationProvider> Providers { get; }
    }
}
