using Hozaru.Core;
using Hozaru.Core.Dependency;
using Hozaru.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.Core;
using Hozaru.Core.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Hozaru.Authentication;
using Microsoft.AspNetCore.Authentication;

namespace Hozaru.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var hozaruBootstrapper = new HozaruBootstrapper();
            hozaruBootstrapper.IocManager.RegisterIfNot<IAssemblyFinder, WebAssemblyFinder>();
            hozaruBootstrapper.Initialize();

            services.AddCors();
            services.AddControllers();
            services.AddResponseCaching();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            })
            .AddApiKeySupport(options => { });

            services.AddAuthorization();

            services.AddSingleton<IApiKeyRepository, InMemoryApiKeyRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    //app.UseDeveloperExceptionPage();
            //    app.UseExceptionHandler("/error");
            //}
            //else
            //{
            app.UseExceptionHandler("/error");
            //}

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseResponseCaching();

            //app.UseHttpsRedirection();
            app.UseCors(i => i.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
