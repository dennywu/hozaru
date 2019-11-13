using Hozaru.ApplicationServices.Orders.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders
{
    public interface IOrderAppService : IApplicationService
    {
        OrderDto CreateOrder(CreateOrderInputDto inputDto);
        OrderDto Get(Guid id);

        Guid Confirmation(ConfirmationOrderInputDto inputDto);
    }
}
