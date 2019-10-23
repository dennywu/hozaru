using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.PaymentTypes;
using Hozaru.ApplicationServices.PaymentTypes.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<PaymentTypeDto> Get()
        {
            var paymentTypes = IoCManager.GetInstance<IPaymentTypeAppService>().GetAll();
            return paymentTypes;
        }

        [HttpGet]
        [Route("{code}/image")]
        public IActionResult GetImage(string code)
        {
            var paymentTypeImageStream = IoCManager.GetInstance<IPaymentTypeAppService>().GetImage(code);
            return File(paymentTypeImageStream, "image/png");
        }
    }
}