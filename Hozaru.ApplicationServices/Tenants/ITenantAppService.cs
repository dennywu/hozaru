using Hozaru.ApplicationServices.Tenants.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Tenants
{
    public interface ITenantAppService : IApplicationService
    {
        TenantInformationDto GetInformation();
    }
}
