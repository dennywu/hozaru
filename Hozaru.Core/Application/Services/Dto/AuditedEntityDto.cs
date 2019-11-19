using Hozaru.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Application.Services.Dto
{
    [Serializable]
    public abstract class AuditedEntityDto<TPrimaryKey> : CreationAuditedEntityDto<TPrimaryKey>, IAudited
    {
        /// <summary>
        /// Last modification date of this entity.
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// </summary>
        public long? LastModifierUserId { get; set; }
    }
}
