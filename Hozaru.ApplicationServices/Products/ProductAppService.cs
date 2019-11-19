using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using AutoMapper;
using Hozaru.ApplicationServices.Products.Dtos;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Domain;
using Hozaru.Core.Configurations;

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

        public Stream GetImage(Guid productId, Guid productImageId)
        {
            var product = _productRepo.Get(productId);
            var productImage = product.Images.FirstOrDefault(i => i.Id == productImageId);

            var pathFileDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            var filePath = Path.Combine(pathFileDirectory, productImage.ImageUrl);
            return File.OpenRead(filePath);
        }
    }
}
