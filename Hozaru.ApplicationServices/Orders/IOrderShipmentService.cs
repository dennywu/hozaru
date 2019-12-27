using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders
{
    public interface IOrderShipmentService : IApplicationService
    {
        void UpdateTrackingInfoAllOrders();
    }
}
