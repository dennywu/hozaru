using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Domain.Entities.Auditing
{
    public interface IDeletionAudited<TUser> : IDeletionAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the deleter user of this entity.
        /// </summary>
        TUser DeleterUser { get; set; }
    }
}
