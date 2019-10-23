using Hozaru.ApplicationServices.Cities;
using Hozaru.ApplicationServices.Districtses;
using Hozaru.ApplicationServices.Freights;
using Hozaru.ApplicationServices.PaymentTypes;
using Hozaru.ApplicationServices.Products;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Repositories.MockUp;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozaru.Web
{
    public class IoCManager
    {
        static Container containers;

        public static void RegisterAssembly()
        {
            containers= new Container(_ =>
            {
                _.AddRegistry<RepositoryRegistry>();
                _.AddRegistry<ApplicationServiceRegistry>();
            });
        }

        public static T GetInstance<T>()
        {
            return containers.GetInstance<T>();
        }
    }

    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For<IRepository<City>>().Use<CityMockUpRepository>();
            For<IRepository<Districts>>().Use<DistrictMockUpRepository>();
            For<IRepository<Product>>().Use<ProductMockUpRepository>();
            For<IRepository<Freight>>().Use<FreightMockUpRepository>();
            For<IRepository<PaymentType>>().Use<PaymentTypeMockUpRepository>();
        }
    }

    public class ApplicationServiceRegistry : Registry
    {
        public ApplicationServiceRegistry()
        {
            For<ICityAppService>().Use<CityAppService>();
            For<IDistrictAppService>().Use<DistrictAppService>();
            For<IProductAppService>().Use<ProductAppService>();
            For<IFreightAppService>().Use<FreightAppService>();
            For<IPaymentTypeAppService>().Use<PaymentTypeAppService>();
        }
    }
}
