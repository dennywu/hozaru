using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Authentication.ApiKeyProvider.Dtos;
using Hozaru.Core;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Identity.MultiTenancy;

namespace Hozaru.Authentication.ApiKeyProvider
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly IRepository<ApiKey> _apiKeyRepository;
        private readonly TenantManager _tenantManager;

        public ApiKeyService(IRepository<ApiKey> apiKeyRepository, TenantManager tenantManager)
        {
            _apiKeyRepository = apiKeyRepository;
            _tenantManager = tenantManager;
        }

        public void CreateApiKey(int tenantId)
        {
            if (_apiKeyRepository.Exist(i => i.TenantId == tenantId))
                throw new HozaruException(string.Format("ApiKey for {0} Already exist", tenantId));

            var apiKey = new ApiKey(tenantId, Guid.NewGuid().ToString());
            _apiKeyRepository.Insert(apiKey);
        }

        public async Task<ApiKeyDto> GetApiKey(string tenancyName)
        {
            var tenant = await _tenantManager.FindByTenancyNameAsync(tenancyName);
            if (tenant.IsNull())
                throw new HozaruException("Api Key not found.");

            var apiKey = _apiKeyRepository.FirstOrDefault(i => i.TenantId == tenant.Id).Key;
            return new ApiKeyDto()
            {
                TenancyName = tenant.TenancyName,
                ApiKey = apiKey
            };
        }
    }
}
