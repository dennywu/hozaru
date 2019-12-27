using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Application.Services
{
    public interface IAspnetCoreServiceResolver
    {
        void AddAspnetCoreServices(IServiceCollection services);
        IServiceProvider GetServiceProvider();
    }
}
