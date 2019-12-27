using Hozaru.ApplicationServices.Products;
using Hozaru.ApplicationServices.Products.Dtos;
using Hozaru.Authentication;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Hozaru.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductController : HozaruApiController
    {
        private IProductAppService _productAppService;

        public ProductController()
        {
            _productAppService = IocManager.Instance.Resolve<IProductAppService>();
        }

        [HttpPost]
        public ProductDto CreateProduct([FromForm] CreateNewProductInputDto inputDto)
        {
            var productId = _productAppService.Create(inputDto);
            return _productAppService.Get(productId);
        }

        [HttpPut]
        public ProductDto Edit([FromForm] EditProductInputDto inputDto)
        {
            var productId = _productAppService.Edit(inputDto);
            return _productAppService.Get(productId);
        }

        [HttpPut]
        [Route("{id}/archive")]
        public IActionResult Archive(Guid id)
        {
            _productAppService.Archive(id);
            return Ok();
        }

        [HttpPut]
        [Route("{id}/activate")]
        public IActionResult Activate(Guid id)
        {
            _productAppService.Activate(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AllScheme)]
        public PagedResultOutput<ProductDto> Get([FromQuery] GetProductPagedInputDto inputDto)
        {
            var products = _productAppService.GetProductActive(inputDto);
            return products;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<ProductDto> GetAllStatus()
        {
            var products = _productAppService.GetAll(ProductStatusInputDto.ALL);
            return products;
        }

        [HttpGet]
        [Route("archive")]
        public IEnumerable<ProductDto> GetProductArchive()
        {
            var products = _productAppService.GetAll(ProductStatusInputDto.ARCHIVE);
            return products;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AllScheme)]
        public ProductDto GetById(Guid id)
        {
            var product = _productAppService.Get(id);
            return product;
        }

        [HttpGet]
        [Route("image/{id}/{productImageId}")]
        [AllowAnonymous]
        public IActionResult GetImage(Guid id, Guid productImageId)
        {
            Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
            Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") });
            var productImageStream = _productAppService.GetImage(id, productImageId);
            return File(productImageStream, "image/jpeg");
        }
    }
}
