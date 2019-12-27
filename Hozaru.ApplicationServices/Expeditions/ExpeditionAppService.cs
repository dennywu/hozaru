using AutoMapper;
using Hozaru.ApplicationServices.Expeditions.Dtos;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Expeditions
{
    public class ExpeditionAppService : HozaruApplicationService, IExpeditionAppService
    {
        private readonly IRepository<ExpeditionService> _expeditionServiceRepository;
        private readonly IRepository<TenantExpeditionService> _tenantExpeditionRepository;

        public ExpeditionAppService(IRepository<ExpeditionService> expeditionServiceRepository, IRepository<TenantExpeditionService> tenantExpeditionRepository)
        {
            _expeditionServiceRepository = expeditionServiceRepository;
            _tenantExpeditionRepository = tenantExpeditionRepository;
        }

        public void CreateOrUpdateDefaultExpeditionTenantIfNeccessary()
        {
            var expeditionServices = _expeditionServiceRepository.GetAll();
            foreach (var expeditionService in expeditionServices)
            {
                if(!_tenantExpeditionRepository.Exist(i => i.ExpeditionService.Id == expeditionService.Id))
                {
                    var tenantExpedition = new TenantExpeditionService(expeditionService);
                    _tenantExpeditionRepository.Insert(tenantExpedition);
                }
            }
        }

        public IList<TenantExpeditionServiceDto> GetAllTenantExpeditionService()
        {
            var result = _tenantExpeditionRepository.GetAllList();
            return Mapper.Map<IList<TenantExpeditionServiceDto>>(result);
        }

        public TenantExpeditionServiceDto UpdateStatusTenantExpeditionService(UpdateStatusTenantExpeditionServiceInputDto inputDto)
        {
            var tenantExpeditionService = _tenantExpeditionRepository.Get(inputDto.Id);
            Validate.Found(tenantExpeditionService, "Jasa Pengiriman");

            if (inputDto.IsActive)
            {
                tenantExpeditionService.Activate();
            }
            else
            {
                tenantExpeditionService.Deactivate();
            }
            _tenantExpeditionRepository.Update(tenantExpeditionService);
            return Mapper.Map<TenantExpeditionServiceDto>(tenantExpeditionService);
        }
    }
}
