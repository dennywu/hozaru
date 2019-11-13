using Hozaru.Core;
using Hozaru.Core.Dependency;
using Hozaru.Core.Modules;
using Hozaru.NHibernate.Interceptors;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Hozaru.NHibernate.Configuration;
using Hozaru.NHibernate.Filters;
using Hozaru.NHibernate.Repositories;

namespace Hozaru.NHibernate
{
    [DependsOn(typeof(HozaruKernelModule))]
    public class HozaruNHibernateModule : HozaruModule
    {
        /// <summary>
        /// NHibernate session factory object.
        /// </summary>
        private ISessionFactory _sessionFactory;

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.Register<HozaruNHibernateInterceptor>(DependencyLifeStyle.Transient);

            _sessionFactory = Configuration.Modules.HozaruNHibernate().FluentConfiguration
                .Mappings(m => m.FluentMappings.Add(typeof(MayHaveTenantFilter)))
                .Mappings(m => m.FluentMappings.Add(typeof(MustHaveTenantFilter)))
                .ExposeConfiguration(config => config.SetInterceptor(IocManager.Resolve<HozaruNHibernateInterceptor>()))
                .BuildSessionFactory();

            IocManager.IocContainer.Install(new NhRepositoryInstaller(_sessionFactory));
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        /// <inheritdoc/>
        public override void Shutdown()
        {
            _sessionFactory.Dispose();
        }
    }
}
