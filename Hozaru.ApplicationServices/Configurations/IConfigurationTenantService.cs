using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Configurations
{
    public interface IConfigurationTenantService : IApplicationService
    {
        void Configure();
    }
}
