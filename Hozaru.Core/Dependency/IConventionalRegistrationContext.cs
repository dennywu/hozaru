using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hozaru.Core.Dependency
{
    /// <summary>
    /// Used to pass needed objects on conventional registration process.
    /// </summary>
    public interface IConventionalRegistrationContext
    {
        /// <summary>
        /// Gets the registering Assembly.
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Reference to the IOC Container to register types.
        /// </summary>
        IIocManager IocManager { get; }

        /// <summary>
        /// Registration configuration.
        /// </summary>
        ConventionalRegistrationConfig Config { get; }
    }
}
