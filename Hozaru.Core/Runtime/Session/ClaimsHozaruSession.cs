using Hozaru.Core.Configurations.Startup;
using System.Linq;
using Hozaru.Core.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using Hozaru.Core.Runtime.Security;
using Microsoft.AspNetCore.Http;
using Hozaru.Core.Dependency;
using Hozaru.Core.Application.Services;

namespace Hozaru.Core.Runtime.Session
{
    /// <summary>
    /// Implements <see cref="IHozaruSession"/> to get session properties from claims of <see cref="Thread.CurrentPrincipal"/>.
    /// </summary>
    public class ClaimsHozaruSession : IHozaruSession
    {
        private const int DefaultTenantId = 1;

        public virtual long? UserId
        {
            get
            {
                var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
                if (claimsPrincipal == null)
                {
                    return null;
                }

                var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
                if (claimsIdentity == null)
                {
                    return null;
                }

                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                {
                    return null;
                }

                long userId;
                if (!long.TryParse(userIdClaim.Value, out userId))
                {
                    return null;
                }

                return userId;
            }
        }

        public virtual int? TenantId
        {
            get
            {
                if (!_multiTenancy.IsEnabled)
                {
                    return DefaultTenantId;
                }

                var aspnetServiceResolver = IocManager.Instance.Resolve<IAspnetCoreServiceResolver>();
                var serviceProvider = aspnetServiceResolver.GetServiceProvider();

                if (serviceProvider.IsNull())
                {
                    return null;
                }

                var httpContextAccessor = (IHttpContextAccessor)serviceProvider.GetService(typeof(IHttpContextAccessor));
                var tenantId = httpContextAccessor.HttpContext.User.FindFirst(HozaruClaimTypes.TenantId)?.Value;

                if (tenantId.IsNotNull())
                {
                    return Convert.ToInt32(tenantId);
                }

                return null;

                //if (!_multiTenancy.IsEnabled)
                //{
                //    return DefaultTenantId;
                //}

                //var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
                //if (claimsPrincipal == null)
                //{
                //    return null;
                //}

                //var tenantIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == HozaruClaimTypes.TenantId);
                //if (tenantIdClaim == null || string.IsNullOrEmpty(tenantIdClaim.Value))
                //{
                //    return null;
                //}

                //return Convert.ToInt32(tenantIdClaim.Value);
            }
        }

        public virtual MultiTenancySides MultiTenancySide
        {
            get
            {
                return _multiTenancy.IsEnabled && !TenantId.HasValue
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant;
            }
        }

        public virtual long? ImpersonatorUserId
        {
            get
            {
                var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
                if (claimsPrincipal == null)
                {
                    return null;
                }

                var impersonatorUserIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == HozaruClaimTypes.ImpersonatorUserId);
                if (impersonatorUserIdClaim == null || string.IsNullOrEmpty(impersonatorUserIdClaim.Value))
                {
                    return null;
                }

                return Convert.ToInt64(impersonatorUserIdClaim.Value);
            }
        }

        public virtual int? ImpersonatorTenantId
        {
            get
            {
                if (!_multiTenancy.IsEnabled)
                {
                    return DefaultTenantId;
                }

                var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
                if (claimsPrincipal == null)
                {
                    return null;
                }

                var impersonatorTenantIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == HozaruClaimTypes.ImpersonatorTenantId);
                if (impersonatorTenantIdClaim == null || string.IsNullOrEmpty(impersonatorTenantIdClaim.Value))
                {
                    return null;
                }

                return Convert.ToInt32(impersonatorTenantIdClaim.Value);
            }
        }

        private readonly IMultiTenancyConfig _multiTenancy;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ClaimsHozaruSession(IMultiTenancyConfig multiTenancy)
        {
            _multiTenancy = multiTenancy;
        }
    }
}
