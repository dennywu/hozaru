using Castle.Facilities.Logging;
using Hozaru.ApplicationServices;
using Hozaru.Authentication;
using Hozaru.Core.Log4net;
using Hozaru.Core.Modules;
using Hozaru.Persistences.NHibernate;
using System.Reflection;

namespace Hozaru.WebApi
{
    [DependsOn(
         typeof(HozaruDataModule),
         typeof(HozaruApplicationModule),
        typeof(HozaruAuthenticationModule))]
    public class HozaruWebApiModule : HozaruModule
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
