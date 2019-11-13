using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AutoMapper;
using Hozaru.ApplicationServices.Products.Dtos;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;

namespace Hozaru.ApplicationServices.Products
{
    public class ProductAppService : HozaruApplicationService, IProductAppService
    {
        private IRepository<Product> _productRepo;
        public ProductAppService(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public IList<ProductDto> GetAll()
        {
            var products = _productRepo.GetAllList();
            return Mapper.Map<IList<Product>, IList<ProductDto>>(products);
        }

        public Stream GetImage(Guid productId)
        {
            var product = _productRepo.Get(productId);
            return File.OpenRead(product.ImageUrl);
        }
    }
}
