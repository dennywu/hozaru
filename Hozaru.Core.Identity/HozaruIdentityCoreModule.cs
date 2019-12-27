using Hozaru.Core.Configurations;
using Hozaru.Core.Identity.Configuration;
using Hozaru.Core.Modules;
using System;
using System.Reflection;

namespace Hozaru.Core.Identity
{
    [DependsOn(typeof(HozaruKernelModule))]
    public class HozaruIdentityCoreModule : HozaruModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IRoleManagementConfig, RoleManagementConfig>();
            IocManager.Register<IUserManagementConfig, UserManagementConfig>();
            //IocManager.Register<ILanguageManagementConfig, LanguageManagementConfig>();
            IocManager.Register<IHozaruIdentityConfig, HozaruIdentityConfig>();

            Configuration.Settings.Providers.Add<HozaruIdentitySettingProvider>();

            //Configuration.Localization.Sources.Add(
            //    new DictionaryBasedLocalizationSource(
            //        HozaruIdentityConsts.LocalizationSourceName,
            //        new XmlEmbeddedFileLocalizationDictionaryProvider(
            //            Assembly.GetExecutingAssembly(), "Hozaru.Identity.Localization.Source"
            //            )));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //IocManager.Register<IMultiTenantLocalizationDictionary, MultiTenantLocalizationDictionary>(DependencyLifeStyle.Transient);
        }
    }
}
