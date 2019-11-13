using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.NHibernate.Repositories
{
    public class NhRepositoryBase<TEntity> : NhRepositoryBase<TEntity, Guid>, IRepository<TEntity> where TEntity : class, IEntity<Guid>
    {
        /// <summary>
        /// Creates a new <see cref="NhRepositoryBase{TEntity,TPrimaryKey}"/> object.
        /// </summary>
        /// <param name="sessionProvider">A session provider to obtain session for database operations</param>
        public NhRepositoryBase(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }
    }
}
