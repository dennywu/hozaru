using FluentNHibernate.Mapping;
using Hozaru.Core.Domain.Uow;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.NHibernate.Filters
{
    /// <summary>
    /// Add filter MustHaveTenant 
    /// </summary>
    public class MustHaveTenantFilter : FilterDefinition
    {
        /// <summary>
        /// Contructor
        /// </summary>
        public MustHaveTenantFilter()
        {
            WithName(HozaruDataFilters.MustHaveTenant)
                .AddParameter("tenantId", NHibernateUtil.Int32)
                .WithCondition("TenantId = :tenantId");
        }
    }
}
