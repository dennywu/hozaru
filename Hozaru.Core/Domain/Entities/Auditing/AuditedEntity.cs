using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Domain.Entities.Auditing
{
    [Serializable]
    public abstract class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// Last modification date of this entity.
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }
    }
}
