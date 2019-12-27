using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Domain.Entities.Auditing
{
    public interface ICreationAudited<TUser> : ICreationAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// </summary>
        TUser CreatorUser { get; set; }
    }
}
