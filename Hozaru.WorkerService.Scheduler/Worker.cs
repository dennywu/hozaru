using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;

namespace Hozaru.WorkerService.Scheduler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger; 
        private StdSchedulerFactory _schedulerFactory;
        private CancellationToken _stopppingToken;
        private IScheduler _scheduler;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartJobs();
            _stopppingToken = stoppingToken;
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
            await _scheduler.Shutdown();
        }

        protected async Task StartJobs()
        {
            _schedulerFactory = new StdSchedulerFactory();

            _scheduler = await _schedulerFactory.GetScheduler();
            await _scheduler.Start();

            IJobDetail updateOrderShipmentTrackingJob = JobBuilder.Create<UpdateOrderShipmentTrackingJob>()
                .WithIdentity("update order shipment tracking", "order")
                .Build();

            ITrigger triggerUpdateOrderShipmentTrackingJob = TriggerBuilder.Create()
                .WithIdentity("trigger_10_sec", "group")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(1)
                    .RepeatForever())
            .Build();

            await _scheduler.ScheduleJob(updateOrderShipmentTrackingJob, triggerUpdateOrderShipmentTrackingJob, _stopppingToken);
        }
    }
}
