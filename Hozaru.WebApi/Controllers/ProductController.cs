using Hozaru.ApplicationServices.Products;
using Hozaru.ApplicationServices.Products.Dtos;
using Hozaru.Authentication;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Hozaru.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : HozaruApiController
    {
        private IProductAppService _productAppService;

        public ProductController()
        {
            _productAppService = IocManager.Instance.Resolve<IProductAppService>();
        }

        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
            var products = _productAppService.GetAll();
            return products;
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
