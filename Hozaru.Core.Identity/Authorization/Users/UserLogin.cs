using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hozaru.Core.Identity.Authorization.Users
{
    [Table("HozaruUserLogins")]
    public class UserLogin : Entity<long>
    {
        /// <summary>
        /// Maximum length of <see cref="LoginProvider"/> property.
        /// </summary>
        public const int MaxLoginProviderLength = 128;

        /// <summary>
        /// Maximum length of <see cref="ProviderKey"/> property.
        /// </summary>
        public const int MaxProviderKeyLength = 256;

        /// <summary>
        /// Id of the User.
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// Login Provider.
        /// </summary>
        [Required]
        [MaxLength(MaxLoginProviderLength)]
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Key in the <see cref="LoginProvider"/>.
        /// </summary>
        [Required]
        [MaxLength(MaxProviderKeyLength)]
        public virtual string ProviderKey { get; set; }
    }
}
