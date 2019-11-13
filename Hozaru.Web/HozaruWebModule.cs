using Hozaru.ApplicationServices;
using Hozaru.Core.Modules;
using Hozaru.Persistences.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hozaru.Web
{
    [DependsOn(
        typeof(HozaruApplicationModule),
        //typeof(HozaruDataMockUpModule)
        typeof(HozaruDataModule)
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
        }
    }
}
