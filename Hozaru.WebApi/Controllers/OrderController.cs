using Hozaru.ApplicationServices.Orders;
using Hozaru.ApplicationServices.Orders.Dtos;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Hozaru.WebApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrderController : HozaruApiController
    {
        private readonly IOrderAppService _orderAppService;

        public OrderController()
        {
            _orderAppService = IocManager.Instance.Resolve<IOrderAppService>();
        }

        [HttpPost]
        public OrderDto CreateOrder(CreateOrderInputDto inputDto)
        {
            var order = _orderAppService.CreateOrder(inputDto);
            return order;
        }

        [HttpGet]
        [Route("")]
        public PagedResultOutput<ListOrderDto> GetAll([FromQuery] GetListOrderInputDto inputDto)
        {
            var result = _orderAppService.GetAll(inputDto);
            return result;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public OrderDto Get(Guid id)
        {
            var order = _orderAppService.Get(id);
            return order;
        }


        [HttpGet]
        [Route("{id:guid}/payments/image/{paymentId:guid}")]
        [AllowAnonymous]
        public IActionResult GetReceiptImage(Guid id, Guid paymentId)
        {
            Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
            Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") });
            var productImageStream = _orderAppService.GetReceiptImage(id, paymentId);
            return File(productImageStream, "image/jpeg");
        }

        [HttpPost]
        [Route("confirmation")]
        public Guid Confirmation([FromForm]ConfirmationOrderInputDto inputDto)
        {
            return _orderAppService.Confirmation(inputDto);
        }

        [HttpPut]
        [Route("approve/{id:guid}")]
        public IActionResult Approve(Guid id)
        {
            _orderAppService.Approve(id);
            return Ok();
        }
        
        [HttpPut]
        [Route("reject")]
        public IActionResult Reject(RejectPaymentInputDto inputDto)
        {
            _orderAppService.Reject(inputDto);
            return Ok();
        }

        [HttpPut]
        [Route("airwaybill")]
        public IActionResult UpdateAirWaybill(UpdateAirWaybillInputDto inputDto)
        {
            _orderAppService.UpdateAirWaybill(inputDto);
            return Ok();
        }
    }
}
