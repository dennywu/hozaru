using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Identity.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.MultiTenancy
{
    public interface IMayHaveTenant<TTenant, TUser> : IMayHaveTenant
        where TTenant : HozaruTenant<TTenant, TUser>
        where TUser : HozaruUser<TTenant, TUser>
    {
        /// <summary>
        /// Tenant.
        /// </summary>
        TTenant Tenant { get; set; }
    }
}
