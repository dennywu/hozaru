using Hozaru.ApplicationServices.Expeditions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Configurations
{
    public class ConfigurationTenantService : IConfigurationTenantService
    {
        private readonly IExpeditionAppService _expeditionService;

        public ConfigurationTenantService(IExpeditionAppService expeditionService)
        {
            _expeditionService = expeditionService;
        }

        public void Configure()
        {
            _expeditionService.CreateOrUpdateDefaultExpeditionTenantIfNeccessary();
        }
    }
}
