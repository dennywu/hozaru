using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.MultiTenancy
{
    public class NullTenantIdResolver : ITenantIdResolver
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullTenantIdResolver Instance { get { return SingletonInstance; } }
        private static readonly NullTenantIdResolver SingletonInstance = new NullTenantIdResolver();

        public int? TenantId { get { return null; } }
    }
}
