﻿using Hozaru.ApplicationServices.PaymentTypes;
using Hozaru.ApplicationServices.PaymentTypes.Dtos;
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
    public class PaymentTypeController : HozaruApiController
    {
        private readonly IPaymentTypeAppService _paymentTypeAppService;

        public PaymentTypeController()
        {
            _paymentTypeAppService = IocManager.Instance.Resolve<IPaymentTypeAppService>();
        }

        [HttpGet]
        public IEnumerable<PaymentTypeDto> Get()
        {
            var paymentTypes = _paymentTypeAppService.GetAll();
            return paymentTypes;
        }

        [HttpGet]
        [Route("image/{code}")]
        public IActionResult GetImage(string code)
        {
            Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
            Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") });
            var paymentTypeImageStream = _paymentTypeAppService.GetImage(code);
            return File(paymentTypeImageStream, "image/png");
        }
    }
}