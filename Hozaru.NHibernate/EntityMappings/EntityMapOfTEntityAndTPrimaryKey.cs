using FluentNHibernate.Mapping;
using Hozaru.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.NHibernate.EntityMappings
{
    public abstract class EntityMap<TEntity, TPrimaryKey> : ClassMap<TEntity> where TEntity : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tableName">Table name</param>
        protected EntityMap(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName)) //TODO: Use code contracts?
            {
                throw new ArgumentNullException("tableName");
            }

            Table(tableName);
            Id(x => x.Id);
            //if (typeof(IMustHaveTenant).IsAssignableFrom(typeof(TEntity)))
            //    Map(Reveal.Member<TEntity>("TenantId")).Column("TenantId").Not.Nullable().Index("tenantid");
            //if (typeof(IMayHaveTenant).IsAssignableFrom(typeof(TEntity)))
            //    Map(Reveal.Member<TEntity>("TenantId")).Column("TenantId").Nullable();

            //if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            //{
            //    Where("IsDeleted = 0"); //TODO: Test with other DBMS then SQL Server
            //}

            //if (typeof(IMustHaveTenant).IsAssignableFrom(typeof(TEntity)))
            //    ApplyFilter<MustHaveTenantFilter>();
            //if (typeof(IMayHaveTenant).IsAssignableFrom(typeof(TEntity)))
            //    ApplyFilter<MayHaveTenantFilter>();
        }
    }
}
