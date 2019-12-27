using Hozaru.AutoMapper;
using Hozaru.Core;
using Hozaru.Core.Modules;
using Hozaru.Domain;
using Hozaru.Identity;
using Hozaru.Whatsapp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hozaru.ApplicationServices
{
    [DependsOn(
        typeof(HozaruDomainModule),
        typeof(HozaruWhatsappModule),
        typeof(HozaruKernelModule),
        typeof(HozaruAutoMapperModule),
        typeof(HozaruIdentityModule)
        )]
    public class HozaruApplicationModule : HozaruModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            DtoMapper.Map();
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
        }
    }
}
