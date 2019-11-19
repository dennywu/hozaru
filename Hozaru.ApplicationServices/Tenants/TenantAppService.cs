using System;
using System.Collections.Generic;
using System.Text;
using Hozaru.ApplicationServices.Tenants.Dtos;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;

namespace Hozaru.ApplicationServices.Tenants
{
    public class TenantAppService : HozaruApplicationService, ITenantAppService
    {
        private IRepository<Order> _orderRepository;
        private IRepository<Product> _productRepository;

        public TenantAppService(IRepository<Order> orderRepository, IRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public TenantInformationDto GetInformation()
        {
            var totalOrder = _orderRepository.Count(i => i.Status == OrderStatus.DONE);
            var totalProduct = _productRepository.Count();

            var info = new TenantInformationDto()
            {
                Name = "Mumubeautyhouse",
                TotalOrder = 123 + totalOrder,
                TotalProduct = totalProduct
            };

            return info;
        }
    }
}
