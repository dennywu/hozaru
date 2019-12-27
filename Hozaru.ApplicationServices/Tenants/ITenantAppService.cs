using Hozaru.ApplicationServices.Tenants.Dtos;
using Hozaru.Authentication.ApiKeyProvider.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.ApplicationServices.Tenants
{
    public interface ITenantAppService : IApplicationService
    {
        TenantInformationDto GetInformation();
        Task CreateTenant(CreateTenantInputDto inputDto);
        TenantDto Get();
        Task<bool> Exist(string tenancyName);
        Task<Stream> GetFaviconImage(string tenancyName);
        Task<Stream> GetBrandImage(string tenancyName);
        TenantInformationDto GetByExternalDomain(string externalDomain);
        void Edit(EditTenantInputDto inputDto);
    }
}
