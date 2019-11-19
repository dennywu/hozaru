using Hozaru.Core.Modules;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hozaru.Authentication
{
    public class HozaruAuthenticationModule : HozaruModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
