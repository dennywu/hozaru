using Castle.Facilities.Logging;
using Hozaru.ApplicationServices;
using Hozaru.Core.Log4net;
using Hozaru.Core.Modules;
using Hozaru.Persistences.NHibernate;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hozaru.WorkerService.Scheduler
{
    [DependsOn(
        typeof(HozaruDataModule),
        typeof(HozaruApplicationModule))]
    public class HozaruWorkerServiceSchedulerModule : HozaruModule
    {
        public override void PreInitialize()
        {
            Configuration.MultiTenancy.IsEnabled = true;
            base.PreInitialize();
        }

        public override void Initialize()
        {
            base.Initialize();

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.IocContainer.AddFacility<LoggingFacility>(f => f.UseHozaruLog4Net().WithConfig("log4net.config"));
        }
    }
}
