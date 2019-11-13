using FluentNHibernate.Mapping;
using Hozaru.Core.Domain.Uow;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.NHibernate.Filters
{
    /// <summary>
    /// Add filter MayHaveTenant 
    /// </summary>
    public class MayHaveTenantFilter : FilterDefinition
    {
        /// <summary>
        /// Contructor
        /// </summary>
        public MayHaveTenantFilter()
        {
            WithName(HozaruDataFilters.MayHaveTenant)
                .AddParameter("tenantId", NHibernateUtil.Int32)
                .WithCondition("TenantId = :tenantId");
        }
    }
}
