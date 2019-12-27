using Hozaru.ApplicationServices.Orders;
using Hozaru.Core.Dependency;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.WorkerService.Scheduler
{
    public class UpdateOrderShipmentTrackingJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var orderService = IocManager.Instance.Resolve<IOrderShipmentService>();
            orderService.UpdateTrackingInfoAllOrders();
            return Task.FromResult(0);
        }
    }
}
