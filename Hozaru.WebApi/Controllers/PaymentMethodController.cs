using Hozaru.ApplicationServices.Banks;
using Hozaru.ApplicationServices.Banks.Dtos;
using Hozaru.ApplicationServices.PaymentMethods;
using Hozaru.ApplicationServices.PaymentMethods.Dtos;
using Hozaru.Authentication;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Hozaru.WebApi.Controllers
{
    [Route("api/paymentmethods")]
    [ApiController]
    [Authorize]
    public class PaymentMethodController : HozaruApiController
    {
        private readonly IPaymentMethodAppService _paymentMethodAppService;
        private readonly IBankService _bankService;

        public PaymentMethodController()
        {
            _paymentMethodAppService = IocManager.Instance.Resolve<IPaymentMethodAppService>();
            _bankService = IocManager.Instance.Resolve<IBankService>();
        }

        [HttpPost]
        public PaymentMethodFullDto CreatePaymentMethod(CreatePaymentMethodInputDto inputDto)
        {
            var paymentMethod = _paymentMethodAppService.Create(inputDto);
            return paymentMethod;
        }

        [HttpPut]
        public PaymentMethodFullDto EditPaymentMethod(EditPaymentMethodInputDto inputDto)
        {
            var paymentMethod = _paymentMethodAppService.Edit(inputDto);
            return paymentMethod;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AllScheme)]
        public IEnumerable<PaymentMethodDto> GetAll()
        {
            var paymentMethods = _paymentMethodAppService.GetAll();
            return paymentMethods;
        }

        [HttpGet]
        [Route("{id}")]
        public PaymentMethodFullDto GetById(Guid id)
        {
            var paymentMethod = _paymentMethodAppService.Get(id);
            return paymentMethod;
        }

        [HttpGet]
        [Route("full")]
        public PagedResultOutput<PaymentMethodFullDto> GetFullByPagging([FromQuery] GetListPaymentMethodInputDto inputDto)
        {
            var paymentMethods = _paymentMethodAppService.GetAll(inputDto);
            return paymentMethods;
        }

        [HttpGet]
        [Route("banks")]
        public IList<BankDto> GetAllBank(string searchKey = "")
        {
            searchKey = searchKey.IsNullOrWhiteSpace() ? string.Empty : searchKey;
            var banks = _bankService.Search(searchKey);
            return banks;
        }

        [HttpGet]
        [Route("image/{code}")]
        [AllowAnonymous]
        public IActionResult GetImage(string code)
        {
            Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
            Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") });
            var paymentMethodImageStream = _paymentMethodAppService.GetImage(code);
            return File(paymentMethodImageStream, "image/png");
        }
    }
}
