using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Application.Services
{
    public class AspnetCoreServiceResolver : IAspnetCoreServiceResolver
    {
        private static WindsorContainer container;
        private IServiceProvider serviceProvider;
        private IServiceCollection _services;

        public AspnetCoreServiceResolver()
        {
            container = new WindsorContainer();
            //Register your components in container
            //then
        }

        public void AddAspnetCoreServices(IServiceCollection services)
        {
            _services = services;
            serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(container, services);
        }

        public IServiceProvider GetServiceProvider()
        {
            return serviceProvider;
        }
    }
}
