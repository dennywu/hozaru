using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Domain.Entities.Auditing
{
    public interface IModificationAudited<TUser> : IModificationAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// </summary>
        TUser LastModifierUser { get; set; }
    }
}
