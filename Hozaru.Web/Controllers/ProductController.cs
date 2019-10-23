using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.Products;
using Hozaru.ApplicationServices.Products.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
            var products = IoCManager.GetInstance<IProductAppService>().GetAll();
            return products;
        }

        [HttpGet]
        [Route("{id}/image")]
        public IActionResult GetImage(Guid id)
        {
            var productImageStream = IoCManager.GetInstance<IProductAppService>().GetImage(id);
            //HttpResponseMessage response = new HttpResponseMessage { Content = new StreamContent(productImageStream) };
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            //response.Content.Headers.ContentLength = productImageStream.Length;
            return File(productImageStream, "image/jpeg");
        }
    }
}