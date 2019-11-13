using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.NHibernate.EntityMappings
{
    public abstract class EntityMap<TEntity> : EntityMap<TEntity, Guid> where TEntity : IEntity<Guid>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tableName">Table name</param>
        protected EntityMap(string tableName)
            : base(tableName)
        {

        }
    }
}
