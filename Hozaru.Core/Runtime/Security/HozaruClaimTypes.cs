using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Runtime.Security
{
    /// <summary>
    /// Used to get Hozaru-specific claim type names.
    /// </summary>
    public static class HozaruClaimTypes
    {
        /// <summary>
        /// TenantId.
        /// </summary>
        public const string TenantId = "http://www.hozaru.com/identity/claims/tenantId";

        /// <summary>
        /// ImpersonatorUserId.
        /// </summary>
        public const string ImpersonatorUserId = "http://www.hozaru.com/identity/claims/impersonatorUserId";

        /// <summary>
        /// ImpersonatorTenantId
        /// </summary>
        public const string ImpersonatorTenantId = "http://www.hozaru.com/identity/claims/impersonatorTenantId";
    }
}
