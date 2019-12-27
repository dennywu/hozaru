using Hozaru.Core.Domain.Entities.Auditing;
using Hozaru.Core.Identity.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.MultiTenancy
{
    public interface IMustHaveTenant<TTenant, TUser> : IMustHaveTenant
        where TTenant : HozaruTenant<TTenant, TUser>
        where TUser : HozaruUser<TTenant, TUser>
    {
        /// <summary>
        /// Tenant.
        /// </summary>
        TTenant Tenant { get; set; }
    }
}
