using Hozaru.Authentication.ApiKeyProvider.Dtos;
using Hozaru.Core.Application.Services;
using Hozaru.Identity.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Authentication.ApiKeyProvider
{
    public interface IApiKeyService : IApplicationService
    {
        void CreateApiKey(int tenantId);
        Task<ApiKeyDto> GetApiKey(string tenancyName);
    }
}
