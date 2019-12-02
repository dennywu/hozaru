using Hozaru.ApplicationServices.Orders.Dtos;
using Hozaru.Core.Application.Services;
using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hozaru.ApplicationServices.Orders
{
    public interface IOrderAppService : IApplicationService
    {
        OrderDto CreateOrder(CreateOrderInputDto inputDto);
        OrderDto Get(Guid id);
        PagedResultOutput<ListOrderDto> GetAll(GetListOrderInputDto inputDto);
        Guid Confirmation(ConfirmationOrderInputDto inputDto);
        Stream GetReceiptImage(Guid id, Guid paymentId);
        void Approve(Guid id);
        void Reject(RejectPaymentInputDto inputDto);
        void UpdateAirWaybill(UpdateAirWaybillInputDto inputDto);
    }
}
