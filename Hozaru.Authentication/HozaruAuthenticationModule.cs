using Hozaru.Core;
using Hozaru.Core.Modules;
using Hozaru.Identity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hozaru.Authentication
{
    [DependsOn(typeof(HozaruIdentityModule), typeof(HozaruKernelModule))]
    public class HozaruAuthenticationModule : HozaruModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //IocManager.Register<IAuthService, AuthService>(Core.Dependency.DependencyLifeStyle.Transient);
            IocManager.Register<ITokenFactory, TokenFactory>(Core.Dependency.DependencyLifeStyle.Transient);
            IocManager.Register<IJwtFactory, JwtFactory>(Core.Dependency.DependencyLifeStyle.Transient);
            IocManager.Register<IJwtTokenHandler, JwtTokenHandler>(Core.Dependency.DependencyLifeStyle.Transient);
            //IocManager.Register<IRefreshTokenService, RefreshTokenService>(Core.Dependency.DependencyLifeStyle.Transient);
            IocManager.Register<JwtIssuerOptions, JwtIssuerOptions>(Core.Dependency.DependencyLifeStyle.Singleton);
            IocManager.Register<IJwtTokenValidator, JwtTokenValidator>(Core.Dependency.DependencyLifeStyle.Transient);
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
        }
    }
}