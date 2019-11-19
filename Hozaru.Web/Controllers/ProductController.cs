using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.Products;
using Hozaru.ApplicationServices.Products.Dtos;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : HozaruApiController
    {
        private  IProductAppService _productAppService;

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
        public IActionResult GetImage(Guid id, Guid productImageId)
        {
            Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
            Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") });
            var productImageStream = _productAppService.GetImage(id, productImageId);
            return File(productImageStream, "image/jpeg");
        }
    }
}