using Hozaru.Core.Configurations;
using Hozaru.Core.Identity.NHibernate;
using Hozaru.Core.Modules;
using Hozaru.Domain;
using Hozaru.NHibernate;
using Hozaru.NHibernate.Configuration;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Reflection;

namespace Hozaru.Persistences.NHibernate
{
    [DependsOn(
        typeof(HozaruDomainModule),
        typeof(HozaruNHibernateModule),
        typeof(HozaruCoreIdentityNHibernateModule))]
    public class HozaruDataModule : HozaruModule
    {

        public override void PreInitialize()
        {
            base.PreInitialize();
            var connectionString = AppSettingConfigurationHelper.GetConnectionString("DefaultConnection");
            Configuration.Modules.HozaruNHibernate()
                .FluentConfiguration
                .Database(FluentNHibernate.Cfg.Db.PostgreSQLConfiguration.Standard.ConnectionString(connectionString).Dialect("NHibernate.Dialect.PostgreSQL82Dialect"))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()));

            if (Convert.ToBoolean(AppSettingConfigurationHelper.GetSection("ResetDatabase").Value))
                Configuration.Modules.HozaruNHibernate().FluentConfiguration.ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false));
        }

        public override void Initialize()
        {
            base.Initialize();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //DatabaseMigrator.MigrateToLatest();
        }
    }
}
