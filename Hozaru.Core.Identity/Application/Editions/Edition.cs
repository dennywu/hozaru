using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.Core.Application.Editions
{
    public class Edition : FullAuditedEntity<int>
    {
        /// <summary>
        /// Maximum length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// Maximum length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// Unique name of this edition.
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Display name of this edition.
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        public Edition()
        {
            Name = Guid.NewGuid().ToString("N");
        }

        public Edition(string displayName)
            : this()
        {
            DisplayName = displayName;
        }
    }
}
