using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hozaru.ApplicationServices.Orders;
using Hozaru.ApplicationServices.Orders.Dtos;
using Hozaru.Core.Dependency;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
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