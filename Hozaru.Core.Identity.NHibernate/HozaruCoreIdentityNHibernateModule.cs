using Hozaru.Core.Modules;
using Hozaru.NHibernate;
using Hozaru.NHibernate.Configuration;
using System.Reflection;

namespace Hozaru.Core.Identity.NHibernate
{
    /// <summary>
    /// Startup class for Hozaru Identity NHibernate module.
    /// </summary>
    [DependsOn(typeof(HozaruIdentityCoreModule), typeof(HozaruNHibernateModule))]
    public class HozaruCoreIdentityNHibernateModule : HozaruModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.HozaruNHibernate().FluentConfiguration
                .Mappings(
                    m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
