using Hozaru.Core.Application.Editions;
using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Core.Identity.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.Core.Identity.MultiTenancy
{
    public class HozaruTenant<TTenant, TUser> : FullAuditedEntity<int, TUser>, IPassivable
         where TUser : HozaruUser<TTenant, TUser>
         where TTenant : HozaruTenant<TTenant, TUser>
    {
        /// <summary>
        /// "Default".
        /// </summary>
        public const string DefaultTenantName = "Default";

        /// <summary>
        /// "^[a-zA-Z][a-zA-Z0-9_-]{1,}$".
        /// </summary>
        public const string TenancyNameRegex = "^[a-zA-Z][a-zA-Z0-9_-]{1,}$";

        /// <summary>
        /// Max length of the <see cref="TenancyName"/> property.
        /// </summary>
        public const int MaxTenancyNameLength = 64;

        /// <summary>
        /// Max length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxNameLength = 128;

        /// <summary>
        /// Tenancy name. This property is the UNIQUE name of this Tenant.
        /// It can be used as subdomain name in a web application.
        /// </summary>
        [Required]
        [StringLength(MaxTenancyNameLength)]
        public virtual string TenancyName { get; set; }

        /// <summary>
        /// Current <see cref="Edition"/> of the Tenant.
        /// </summary>
        public virtual Edition Edition { get; set; }
        public virtual int? EditionId { get; set; }

        /// <summary>
        /// Display name of the Tenant.
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Is this tenant active?
        /// If as tenant is not active, no user of this tenant can use the application.
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Defined settings for this tenant.
        /// </summary>
        public virtual ICollection<Setting> Settings { get; set; }

        /// <summary>
        /// Creates a new tenant.
        /// </summary>
        public HozaruTenant()
        {
            IsActive = true;
        }

        /// <summary>
        /// Creates a new tenant.
        /// </summary>
        /// <param name="tenancyName">UNIQUE name of this Tenant</param>
        /// <param name="name">Display name of the Tenant</param>
        public HozaruTenant(string tenancyName, string name)
            : this()
        {
            TenancyName = tenancyName;
            Name = name;
        }
    }
}
