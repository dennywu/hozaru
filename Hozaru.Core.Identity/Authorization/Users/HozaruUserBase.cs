using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hozaru.Core.Identity.Authorization.Users
{
    [Table("HozaruUsers")]
    public abstract class HozaruUserBase : FullAuditedEntity<long>, IUser<long>, IMayHaveTenant
    {
        /// <summary>
        /// Maximum length of the <see cref="UserName"/> property.
        /// </summary>
        public const int MaxUserNameLength = 32;

        /// <summary>
        /// User name.
        /// User name must be unique for it's tenant.
        /// </summary>
        [Required]
        [StringLength(MaxUserNameLength)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// Tenant Id of this user.
        /// </summary>
        public virtual int? TenantId { get; set; }
    }
}
