﻿using Hozaru.ApplicationServices.Orders;
using Hozaru.ApplicationServices.Orders.Dtos;
using Hozaru.Authentication;
using Hozaru.Core.Application.Services.Dto;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AllScheme)]
        public OrderDto CreateOrder(CreateOrderInputDto inputDto)
        {
            var order = _orderAppService.CreateOrder(inputDto);
            return order;
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AllScheme)]
        public OrderDto Get(Guid id)
        {
            var order = _orderAppService.Get(id);
            return order;
        }

        [HttpGet]
        [Route("{id:guid}/tracking")]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AllScheme)]
        public DetailOrderShipmentTrackingDto GetTrackingInformation(Guid id)
        {
            var result = _orderAppService.GetShipmentTracking(id);
            return result;
        }

        [HttpGet]
        [Route("")]
        public PagedResultOutput<ListOrderDto> GetAll([FromQuery] GetListOrderInputDto inputDto)
        {
            var result = _orderAppService.GetAll(inputDto);
            return result;
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
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AllScheme)]
        public Guid Confirmation([FromForm]ConfirmationOrderInputDto inputDto)
        {
            return _orderAppService.AddPayment(inputDto);
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
        [Route("cancel")]
        public IActionResult Cancel(CancelPaymentInputDto inputDto)
        {
            _orderAppService.Cancel(inputDto);
            return Ok();
        }

        [HttpPut]
        [Route("complete/{id:guid}")]
        public IActionResult CompleteOrder(Guid id)
        {
            _orderAppService.CompleteOrder(id);
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
