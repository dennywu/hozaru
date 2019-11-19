using Hozaru.ApplicationServices.Orders;
using Hozaru.ApplicationServices.Orders.Dtos;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Hozaru.WebApi.Controllers
{
    [Route("api/[controller]")]
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
        public OrderDto Get(Guid id)
        {
            var order = _orderAppService.Get(id);
            return order;
        }

        [HttpPost("confirmation")]
        public Guid Confirmation([FromForm]ConfirmationOrderInputDto inputDto)
        {
            return _orderAppService.Confirmation(inputDto);
        }
    }
}
