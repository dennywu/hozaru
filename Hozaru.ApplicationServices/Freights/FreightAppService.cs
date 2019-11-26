using System;
using System.Collections.Generic;
using System.Text;
using Hozaru.ApplicationServices.Freights.Dtos;
using Hozaru.Core.Domain.Repositories;
using System.Linq;
using Hozaru.Domain;

namespace Hozaru.ApplicationServices.Freights
{
    public class FreightAppService : IFreightAppService
    {
        private IRepository<Product> _productRepository;
        private IRepository<Freight> _freightRepository;

        public FreightAppService(IRepository<Product> productRepository, IRepository<Freight> freightRepository)
        {
            _productRepository = productRepository;
            _freightRepository = freightRepository;
        }

        public IList<FreightDto> GetFreight(GetFreightInputDto inputDto)
        {
            var weightShoppingCart = calculateWeightShoppingCart(inputDto);

            var freight = _freightRepository.FirstOrDefault(i => i.DestinationDistricts.Code == inputDto.Districts);
            Validate.Found(freight, "Ongkos Kirim", inputDto.Districts);

            IList<FreightDto> result = new List<FreightDto>();
            if (freight == null)
                return result;

            foreach (var item in freight.Items)
            {
                if (item.Expedition.Disabled)
                    continue;

                var weightInKG = Math.Ceiling(weightShoppingCart / 1000);
                var shippingRate = weightInKG * item.Rate;
                var description = item.GetEstimatedTimeDepartureInString();

                result.Add(new FreightDto()
                {
                    ExpeditionCode = item.Expedition.Code,
                    ExpeditionName = item.Expedition.Name,
                    ExpeditionFullName = item.Expedition.FullName,
                    Id = item.Id,
                    Rate = shippingRate,
                    Description = description,
                    TotalWeight = weightInKG
                });
            }

            return result;
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
