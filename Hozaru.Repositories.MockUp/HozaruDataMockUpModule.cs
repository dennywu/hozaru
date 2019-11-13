using Hozaru.Core.Domain.Repositories;
using Hozaru.Core.Modules;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hozaru.Repositories.MockUp
{
    public class HozaruDataMockUpModule : HozaruModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.Register<IRepository<Product>, ProductMockUpRepository>(Core.Dependency.DependencyLifeStyle.Singleton);
            IocManager.Register<IRepository<City>, CityMockUpRepository>(Core.Dependency.DependencyLifeStyle.Singleton);
            IocManager.Register<IRepository<Districts>, DistrictMockUpRepository>(Core.Dependency.DependencyLifeStyle.Singleton);
            IocManager.Register<IRepository<Freight>, FreightMockUpRepository>(Core.Dependency.DependencyLifeStyle.Singleton);
            IocManager.Register<IRepository<PaymentType>, PaymentTypeMockUpRepository>(Core.Dependency.DependencyLifeStyle.Singleton);
            IocManager.Register<IRepository<Order>, OrderMockUpRepository>(Core.Dependency.DependencyLifeStyle.Singleton);
            IocManager.Register<IAutoNumberRepository, AutoNumberMockUpRepository>(Core.Dependency.DependencyLifeStyle.Singleton);
        }
    }
}
