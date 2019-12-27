using System;
using System.Collections.Generic;
using System.Text;
using Hozaru.ApplicationServices.Freights.Dtos;
using Hozaru.Core.Domain.Repositories;
using System.Linq;
using Hozaru.Domain;
using Hozaru.ApplicationServices.RajaOngkir;
using Hozaru.ApplicationServices.RajaOngkir.Dtos;
using Hozaru.Core;

namespace Hozaru.ApplicationServices.Freights
{
    public class FreightAppService : HozaruApplicationService, IFreightAppService
    {
        private IRepository<Product> _productRepository;
        private IRepository<Freight> _freightRepository;
        private IRajaOngkirService _rajaOngkirService;
        private IRepository<TenantExpeditionService> _tenantExpeditionRepo;
        private IRepository<Districts> _districtRepo;
        private IRepository<City> _cityRepo;

        public FreightAppService(IRepository<Product> productRepository, IRepository<Freight> freightRepository, IRajaOngkirService rajaOngkirService, IRepository<TenantExpeditionService> tenantExpeditionRepo, IRepository<Districts> districtRepo, IRepository<City> cityRepo)
        {
            _productRepository = productRepository;
            _freightRepository = freightRepository;
            _rajaOngkirService = rajaOngkirService;
            _tenantExpeditionRepo = tenantExpeditionRepo;
            _districtRepo = districtRepo;
            _cityRepo = cityRepo;
        }

        public IList<FreightDto> GetFreight(GetFreightInputDto inputDto)
        {
            var weightShoppingCart = calculateWeightShoppingCart(inputDto);
            var tenantExpeditions = _tenantExpeditionRepo.GetAllList(i => i.IsActive);
            var originDistrict = GetCurrentTenant().District;
            var destinationDistrict = _districtRepo.FirstOrDefault(i => i.Id == inputDto.DistrictId);

            var activeExpeditionServices = tenantExpeditions.Select(i => i.ExpeditionService).ToList();
            var activeExpeditions = new List<Expedition>();
            activeExpeditionServices.ForEach(i =>
            {
                if (!activeExpeditions.Any(activeExpedition => activeExpedition.Id == i.Expedition.Id))
                {
                    activeExpeditions.Add(i.Expedition);
                }
            });

            var getShippingCostInputDto = new GetShippingCostInputDto
            {
                Expeditions = activeExpeditions,
                Origin = originDistrict,
                Destination = destinationDistrict,
                Weight = weightShoppingCart
            };

            IList<FreightDto> result = new List<FreightDto>();
            var rajaOngkirResponses = _rajaOngkirService.GetShippingCost(getShippingCostInputDto);
            foreach (var rajaOngkirResponse in rajaOngkirResponses)
            {
                foreach (var rajaOngkirResponseResult in rajaOngkirResponse.Results)
                {
                    if (activeExpeditionServices.Any(i => i.Expedition.RajaOngkirCode.ToLower() == rajaOngkirResponse.Code.ToLower() && i.RajaOngkirCode.ToLower() == rajaOngkirResponseResult.ServiceName.ToLower()))
                    {
                        var expeditionService = activeExpeditionServices.FirstOrDefault(i => i.Expedition.RajaOngkirCode.ToLower() == rajaOngkirResponse.Code.ToLower() && i.RajaOngkirCode.ToLower() == rajaOngkirResponseResult.ServiceName.ToLower());
                        var estimatedTimeDelivery = new EstimatedTimeDelivery(rajaOngkirResponseResult.EstimatedToDelivery);
                        var freightDto = new FreightDto()
                        {
                            ExpeditionServiceId = expeditionService.Id,
                            ExpeditionFullName = expeditionService.FullName,
                            Cost = rajaOngkirResponseResult.Cost,
                            EstimatedTimeDelivery = estimatedTimeDelivery,
                            Description = estimatedTimeDelivery.GetEstimatedTimeDeliverySentence(DateTime.Now),
                            TotalWeight = weightShoppingCart,
                            ExpeditionServiceGroupName = expeditionService.GroupName
                        };
                        result.Add(freightDto);
                    }
                }
            }

            return result;
        }

        public FreightDto GetFreightByExpeditionService(GetFreightByServiceInputDto inputDto)
        {
            var tenantExpedition = _tenantExpeditionRepo.FirstOrDefault(i => i.ExpeditionService.Id == inputDto.ExpeditionService.Expedition.Id);
            Validate.Found(tenantExpedition, "Expedition");
            if (!tenantExpedition.IsActive)
                throw new HozaruException("Expedition tidak aktif");

            var getShippingCostInputDto = new GetShippingCostInputDto
            {
                Expeditions = new List<Expedition>() { inputDto.ExpeditionService.Expedition },
                Origin = inputDto.Origin,
                Destination = inputDto.Destination,
                Weight = inputDto.Weight
            };

            var rajaOngkirResponses = _rajaOngkirService.GetShippingCost(getShippingCostInputDto);
            if (rajaOngkirResponses.IsNullOrEmpty())
                throw new HozaruException("Ongkos Kirim tidak ditemukan");
            var rajaOngkirResponseResult = rajaOngkirResponses.FirstOrDefault().Results.FirstOrDefault(i => i.ServiceName.ToLower() == inputDto.ExpeditionService.RajaOngkirCode.ToLower());

            var estimatedTimeDelivery = new EstimatedTimeDelivery(rajaOngkirResponseResult.EstimatedToDelivery);
            var freightDto = new FreightDto()
            {
                ExpeditionServiceId = inputDto.ExpeditionService.Id,
                ExpeditionFullName = inputDto.ExpeditionService.FullName,
                Cost = rajaOngkirResponseResult.Cost,
                EstimatedTimeDelivery = estimatedTimeDelivery,
                Description = estimatedTimeDelivery.GetEstimatedTimeDeliverySentence(DateTime.Now),
                TotalWeight = inputDto.Weight
            };

            return freightDto;
        }

        private decimal calculateWeightShoppingCart(GetFreightInputDto inputDto)
        {
            decimal weightShoppingCart = 0;
            foreach (var item in inputDto.Items)
            {
                var product = _productRepository.Get(item.ProductId);
                var weight = product.Weight * item.Quantity;
                weightShoppingCart += weight;
            }
            return weightShoppingCart;
        }
    }
}
