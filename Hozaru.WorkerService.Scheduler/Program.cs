using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.Core;
using Hozaru.Core.Dependency;
using Hozaru.Core.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hozaru.WorkerService.Scheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hozaruBootstrapper = new HozaruBootstrapper();
            hozaruBootstrapper.IocManager.RegisterIfNot<IAssemblyFinder, WorkerServiceAssemblyFinder>();
            hozaruBootstrapper.Initialize();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
