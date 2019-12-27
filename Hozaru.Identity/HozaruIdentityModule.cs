using Hozaru.Core.Identity;
using Hozaru.Core.Modules;
using System;
using System.Reflection;

namespace Hozaru.Identity
{
    [DependsOn(typeof(HozaruIdentityCoreModule))]
    public class HozaruIdentityModule : HozaruModule
    {
        public override void PreInitialize()
        {
            //Configuration.Features.Providers.Add<AppFeatureProvider>();

            //Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();
            //Configuration.Settings.Providers.Add<AppSettingProvider>();
            //AppRoleConfig.Configure(Configuration.Modules.HozaruIdentity().RoleManagement);
            Configuration.MultiTenancy.IsEnabled = true;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
