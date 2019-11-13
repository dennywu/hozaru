using Hozaru.Core.Modules;
using Hozaru.Domain;
using Hozaru.NHibernate;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Hozaru.NHibernate.Configuration;
using NHibernate.Tool.hbm2ddl;
using Microsoft.Extensions.Configuration;
using System.IO;
using Hozaru.Core.Configurations;

namespace Hozaru.Persistences.NHibernate
{
    [DependsOn(
        typeof(HozaruDomainModule),
        typeof(HozaruNHibernateModule))]
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
