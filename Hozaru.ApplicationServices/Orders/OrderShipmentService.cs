using Hozaru.Core.Domain.Repositories;
using Hozaru.Core.Domain.Uow;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Orders
{
    public class OrderShipmentService : HozaruApplicationService, IOrderShipmentService
    {
        private IRepository<Order> _orderRepository;
        private IOrderAppService _orderService;

        public OrderShipmentService(IRepository<Order> orderRepository, IOrderAppService orderService)
        {
            _orderRepository = orderRepository;
            _orderService = orderService;
        }

        public void UpdateTrackingInfoAllOrders()
        {
            using (CurrentUnitOfWork.DisableFilter(HozaruDataFilters.MustHaveTenant))
            {
                var orders = _orderRepository.GetAllList(i => i.Status == OrderStatus.SHIPPING);
                foreach (var order in orders)
                {
                    _orderService.UpdateTrackingInfo(order.Id);
                }
            }
        }
    }
}
