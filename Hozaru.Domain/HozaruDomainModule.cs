using Hozaru.Core.Modules;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hozaru.Domain
{
    public class HozaruDomainModule : HozaruModule
    {
        public override void Initialize()
        {
            base.Initialize();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
