using Hozaru.Core.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Runtime.Session
{
    /// <summary>
    /// Implements null object pattern for <see cref="IHozaruSession"/>.
    /// </summary>
    public class NullHozaruSession : IHozaruSession
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullHozaruSession Instance { get { return SingletonInstance; } }
        private static readonly NullHozaruSession SingletonInstance = new NullHozaruSession();

        /// <inheritdoc/>
        public long? UserId { get { return null; } }

        /// <inheritdoc/>
        public int? TenantId { get { return null; } }

        public MultiTenancySides MultiTenancySide { get { return MultiTenancySides.Tenant; } }

        public long? ImpersonatorUserId { get { return null; } }

        public int? ImpersonatorTenantId { get { return null; } }

        private NullHozaruSession()
        {

        }
    }
}
