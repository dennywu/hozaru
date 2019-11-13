using Hozaru.Core.Application.Services;
using Hozaru.Core.Auditing;
using Hozaru.Core.Dependency;
using Hozaru.Core.Domain.Uow;
using Hozaru.Core.Modules;
using Hozaru.Core.MultiTenancy;
using Hozaru.Core.Runtime.Caching;
using Hozaru.Core.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hozaru.Core
{
    /// <summary>
    /// Kernel (core) module of the Hozaru system.
    /// No need to depend on this, it's automatically the first module always.
    /// </summary>
    public sealed class HozaruKernelModule : HozaruModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());

            //ValidationInterceptorRegistrar.Initialize(IocManager);

            //FeatureInterceptorRegistrar.Initialize(IocManager);
            AuditingInterceptorRegistrar.Initialize(IocManager);

            UnitOfWorkRegistrar.Initialize(IocManager);

            //AuthorizationInterceptorRegistrar.Initialize(IocManager);

            Configuration.Auditing.Selectors.Add(
                new NamedTypeSelector(
                    "Hozaru.ApplicationServices",
                    type => typeof(IApplicationService).IsAssignableFrom(type)
                    )
                );

            //Configuration.Localization.Sources.Add(
            //    new DictionaryBasedLocalizationSource(
            //        HozaruConsts.LocalizationSourceName,
            //        new XmlEmbeddedFileLocalizationDictionaryProvider(
            //            Assembly.GetExecutingAssembly(), "Hozaru.Localization.Sources.HozaruXmlSource"
            //            )));

            //Configuration.Settings.Providers.Add<LocalizationSettingProvider>();
            //Configuration.Settings.Providers.Add<EmailSettingProvider>();

            Configuration.UnitOfWork.RegisterFilter(HozaruDataFilters.SoftDelete, true);
            Configuration.UnitOfWork.RegisterFilter(HozaruDataFilters.MustHaveTenant, true);
            Configuration.UnitOfWork.RegisterFilter(HozaruDataFilters.MayHaveTenant, true);

            ConfigureCaches();
        }

        public override void Initialize()
        {
            base.Initialize();

            //IocManager.IocContainer.Install(new EventBusInstaller(IocManager));

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(),
                new ConventionalRegistrationConfig
                {
                    InstallInstallers = false
                });
        }

        public override void PostInitialize()
        {
            RegisterMissingComponents();

            //IocManager.Resolve<SettingDefinitionManager>().Initialize();
            //IocManager.Resolve<FeatureManager>().Initialize();
            //IocManager.Resolve<NavigationManager>().Initialize();
            //IocManager.Resolve<PermissionManager>().Initialize();
            //IocManager.Resolve<LocalizationManager>().Initialize();
        }

        private void ConfigureCaches()
        {
            Configuration.Caching.Configure(HozaruCacheNames.ApplicationSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(8);
            });

            Configuration.Caching.Configure(HozaruCacheNames.TenantSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(60);
            });

            Configuration.Caching.Configure(HozaruCacheNames.UserSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(20);
            });
        }

        private void RegisterMissingComponents()
        {
            IocManager.RegisterIfNot<IUnitOfWork, NullUnitOfWork>(DependencyLifeStyle.Transient);
            IocManager.RegisterIfNot<IAuditInfoProvider, NullAuditInfoProvider>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IAuditingStore, SimpleLogAuditingStore>(DependencyLifeStyle.Transient);
            IocManager.RegisterIfNot<ITenantIdResolver, NullTenantIdResolver>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IHozaruSession, ClaimsHozaruSession>(DependencyLifeStyle.Singleton);
        }
    }
}
