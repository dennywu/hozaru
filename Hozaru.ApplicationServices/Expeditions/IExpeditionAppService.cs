using Hozaru.ApplicationServices.Expeditions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Expeditions
{
    public interface IExpeditionAppService
    {
        void CreateOrUpdateDefaultExpeditionTenantIfNeccessary();
        IList<TenantExpeditionServiceDto> GetAllTenantExpeditionService();
        TenantExpeditionServiceDto UpdateStatusTenantExpeditionService(UpdateStatusTenantExpeditionServiceInputDto inputDto);
    }
}
