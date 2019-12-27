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
using Hozaru.Core;
using Hozaru.ApplicationServices.ImagesGenerator;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp;
using Hozaru.Core.Application.Services.Dto;

namespace Hozaru.ApplicationServices.Products
{
    public class ProductAppService : HozaruApplicationService, IProductAppService
    {
        private IRepository<Product> _productRepo;
        private IRepository<Order> _orderRepo;
        private IImageGenerator _imageGenerator;

        public ProductAppService(IRepository<Product> productRepo, IImageGenerator imageGenerator, IRepository<Order> orderRepo)
        {
            _productRepo = productRepo;
            _imageGenerator = imageGenerator;
            _orderRepo = orderRepo;
        }

        public void Activate(Guid id)
        {
            var product = _productRepo.Get(id);
            Validate.Found(product, "Produk");
            product.Activate();
            _productRepo.Update(product);
        }

        public void Archive(Guid id)
        {
            var product = _productRepo.Get(id);
            Validate.Found(product, "Produk");
            product.Archive();
            _productRepo.Update(product);
        }

        public Guid Create(CreateNewProductInputDto inputDto)
        {
            //if (_productRepo.Exist(i => i.Name == inputDto.Name))
            //    throw new HozaruException(string.Format("Produk {0} sudah terdaftar.", inputDto.Name));

            if (!inputDto.SKU.IsNullOrWhiteSpace() && _productRepo.Exist(i => i.SKU == inputDto.SKU))
                throw new HozaruException(string.Format("Produk dengan SKU {0} sudah terdaftar.", inputDto.SKU));

            var product = Product.Create(inputDto.Name, inputDto.Description, inputDto.Price, inputDto.Weight, inputDto.SKU);
            _productRepo.Insert(product);

            foreach (var imageInputDto in inputDto.Images)
            {
                var fileName = string.Format("{0}_{1}", product.Name, imageInputDto.Priority);
                var imageStream = imageInputDto.Image.OpenReadStream();
                var imageObj = Image.Load(imageStream);
                var filePath = _imageGenerator.SaveProductImage(imageObj, fileName, product, JpegFormat.Instance);
                product.AddImage(filePath, fileName, imageInputDto.Priority);
            }

            _productRepo.Update(product);

            return product.Id;
        }

        public void Delete(Guid id)
        {
            if (_orderRepo.Exist(i => i.Items.Any(e => e.Product.Id == id)))
                throw new HozaruException("Product tidak bisa dihapus karena sudah ada transaksi");
            _productRepo.Delete(id);
        }

        public Guid Edit(EditProductInputDto inputDto)
        {
            var product = _productRepo.Get(inputDto.Id);
            Validate.Found(product, "Produk");

            if (product.SKU != inputDto.SKU && _productRepo.Count(i => i.SKU == inputDto.SKU) > 0)
                throw new HozaruException(string.Format("Produk dengan SKU {0} sudah terdaftar.", inputDto.SKU));

            product.Update(inputDto.Name, inputDto.Description, inputDto.Price, inputDto.Weight, inputDto.SKU);

            var pathFileDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            foreach (var priority in inputDto.DeletedImagesByPriority)
            {
                var productImage = product.Images.FirstOrDefault(i => i.Priority == priority);
                if (productImage.IsNotNull())
                {
                    var imagePath = Path.Combine(pathFileDirectory, productImage.ImageUrl);
                    File.Delete(imagePath);
                    product.RemoveImage(productImage);
                }
            }

            foreach (var image in inputDto.Images)
            {
                var fileName = string.Format("{0}_{1}", product.Name, image.Priority);
                var imageStream = image.Image.OpenReadStream();
                var imageObj = Image.Load(imageStream);
                var filePath = _imageGenerator.SaveProductImage(imageObj, fileName, product, JpegFormat.Instance);
                product.AddImage(filePath, fileName, image.Priority);
            }
            _productRepo.Update(product);

            return product.Id;
        }

        public ProductDto Get(Guid id)
        {
            var product = _productRepo.Get(id);
            Validate.Found(product, "Produk");
            return Mapper.Map<ProductDto>(product);
        }

        public IList<ProductDto> GetAll(ProductStatusInputDto inputDto)
        {
            var products = new List<Product>();
            switch (inputDto)
            {
                case ProductStatusInputDto.ALL:
                    products = _productRepo.GetAll().OrderByDescending(i => i.CreationTime).ToList();
                    break;
                case ProductStatusInputDto.ARCHIVE:
                    products = _productRepo.GetAllList(i => i.Status == ProductStatus.ARCHIVE).OrderByDescending(i => i.CreationTime).ToList();
                    break;
                default:
                    products = _productRepo.GetAllList(i => i.Status == ProductStatus.ACTIVE).OrderByDescending(i => i.CreationTime).ToList();
                    break;
            }

            return Mapper.Map<IList<Product>, IList<ProductDto>>(products);
        }

        public PagedResultOutput<ProductDto> GetProductActive(GetProductPagedInputDto inputDto)
        {
            IQueryable<Product> query;
            var products = _productRepo.GetAll();
            switch (inputDto.Status)
            {
                case ProductStatusInputDto.ARCHIVE:
                    query = products.Where(i => i.Status == ProductStatus.ARCHIVE);
                    break;
                case ProductStatusInputDto.ACTIVE:
                    query = products.Where(i => i.Status == ProductStatus.ACTIVE);
                    break;
                default:
                    query = products;
                    break;
            }

            var result = query.OrderByDescending(i => i.CreationTime).PageBy(inputDto).ToList();
            var resultCount = query.Count();

            return new PagedResultOutput<ProductDto>(resultCount, Mapper.Map<List<ProductDto>>(result));
        }

        public Stream GetImage(Guid productId, Guid productImageId)
        {
            var product = _productRepo.Get(productId);
            Validate.Found(product, "Produk");
            var productImage = product.Images.FirstOrDefault(i => i.Id == productImageId);
            Validate.Found(product, "Gambar Produk");

            var pathFileDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            var filePath = Path.Combine(pathFileDirectory, productImage.ImageUrl);
            return File.OpenRead(filePath);
        }
    }
}
