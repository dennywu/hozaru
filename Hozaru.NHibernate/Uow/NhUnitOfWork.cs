﻿using Hozaru.Core.Dependency;
using Hozaru.Core.Domain.Uow;
using Hozaru.Core.Runtime.Session;
using NHibernate;
using System;
using Hozaru.Core.Transactions.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Hozaru.NHibernate.Uow
{
    /// <summary>
    /// Implements Unit of work for NHibernate.
    /// </summary>
    public class NhUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// Used to get current session values.
        /// </summary>
        public IHozaruSession HozaruSession { get; set; }
        /// <summary>
        /// Gets NHibernate session object to perform queries.
        /// </summary>
        public ISession Session { get; private set; }

        /// <summary>
        /// <see cref="NhUnitOfWork"/> uses this DbConnection if it's set.
        /// This is usually set in tests.
        /// </summary>
        public IDbConnection DbConnection { get; set; }

        private readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;

        /// <summary>
        /// Creates a new instance of <see cref="NhUnitOfWork"/>.
        /// </summary>
        public NhUnitOfWork(ISessionFactory sessionFactory, IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
            HozaruSession = NullHozaruSession.Instance;
            _sessionFactory = sessionFactory;
        }

        protected override void BeginUow()
        {
            Session = DbConnection != null
                ? _sessionFactory.OpenSession((DbConnection)DbConnection)
                : _sessionFactory.OpenSession();



            if (Options.IsTransactional == true)
            {
                _transaction = Options.IsolationLevel.HasValue
                    ? Session.BeginTransaction(Options.IsolationLevel.Value.ToSystemDataIsolationLevel())
                    : Session.BeginTransaction();
            }

            this.CheckAndSetMayHaveTenant();
            this.CheckAndSetMustHaveTenant();
        }

        protected virtual void CheckAndSetMustHaveTenant()
        {
            if (this.IsFilterEnabled(HozaruDataFilters.MustHaveTenant)) return;
            if (HozaruSession.TenantId == null) return;
            ApplyEnableFilter(HozaruDataFilters.MustHaveTenant); //Enable Filters
            ApplyFilterParameterValue(HozaruDataFilters.MustHaveTenant,
                HozaruDataFilters.Parameters.TenantId,
                HozaruSession.GetTenantId()); //ApplyFilter
        }

        protected virtual void CheckAndSetMayHaveTenant()
        {
            if (this.IsFilterEnabled(HozaruDataFilters.MayHaveTenant)) return;
            if (HozaruSession.TenantId == null) return;
            ApplyEnableFilter(HozaruDataFilters.MayHaveTenant); //Enable Filters
            ApplyFilterParameterValue(HozaruDataFilters.MayHaveTenant,
                HozaruDataFilters.Parameters.TenantId,
                HozaruSession.TenantId); //ApplyFilter
        }

        public override void SaveChanges()
        {
            Session.Flush();
        }

        public override Task SaveChangesAsync()
        {
            Session.Flush();
            return Task.FromResult(0);
        }

        /// <summary>
        /// Commits transaction and closes database connection.
        /// </summary>
        protected override void CompleteUow()
        {
            SaveChanges();
            if (_transaction != null)
            {
                _transaction.Commit();
            }
        }

        protected override Task CompleteUowAsync()
        {
            CompleteUow();
            return Task.FromResult(0);
        }

        /// <summary>
        /// Rollbacks transaction and closes database connection.
        /// </summary>
        protected override void DisposeUow()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            Session.Dispose();
        }

        protected override void ApplyEnableFilter(string filterName)
        {
            if (Session.GetEnabledFilter(filterName) == null)
                Session.EnableFilter(filterName);
        }
        protected override void ApplyDisableFilter(string filterName)
        {
            if (Session.GetEnabledFilter(filterName) != null)
                Session.DisableFilter(filterName);
        }

        protected override void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            Session.GetEnabledFilter(filterName)
                .SetParameter(parameterName, value);
        }
    }
}
