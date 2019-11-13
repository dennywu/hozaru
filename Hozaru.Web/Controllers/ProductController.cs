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
        [Route("{id}/image")]
        public IActionResult GetImage(Guid id)
        {
            var productImageStream = _productAppService.GetImage(id);
            return File(productImageStream, "image/jpeg");
        }
    }
}