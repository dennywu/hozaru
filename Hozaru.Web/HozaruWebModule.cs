using Castle.Facilities.Logging;
using Hozaru.ApplicationServices;
using Hozaru.Core.Modules;
using Hozaru.Persistences.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hozaru.Core.Log4net;

namespace Hozaru.Web
{
    [DependsOn(
        //typeof(HozaruDataMockUpModule)
        typeof(HozaruDataModule),
        typeof(HozaruApplicationModule)
        )]
    public class HozaruWebModule : HozaruModule
    {
        public override void PreInitialize()
        {
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
