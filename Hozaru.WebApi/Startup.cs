using Hozaru.Authentication;
using Hozaru.Core;
using Hozaru.Core.Application.Services;
using Hozaru.Core.Dependency;
using Hozaru.Core.Reflection;
using Hozaru.Core.Runtime.Session;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.WebApi
{
    public partial class Startup
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

            configureTokenAuthentication(services);

            services.AddCors();
            services.AddControllers();
            services.AddResponseCaching();
            services.AddAuthentication().AddApiKeySupport(options => { });

            services.AddAuthorization();

            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var aspnetCoreServiceResolver = IocManager.Instance.Resolve<IAspnetCoreServiceResolver>();
            aspnetCoreServiceResolver.AddAspnetCoreServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseResponseCaching();

            app.UseCors(i => i.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

            app.UseRouting();


            //app.UseTokenProvider(_tokenProviderOptions);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void configureTokenAuthentication(IServiceCollection services)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AuthSettings:SecretKey").Value));
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var jwtIssuerOptions = IocManager.Instance.Resolve<JwtIssuerOptions>();
            jwtIssuerOptions.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            jwtIssuerOptions.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
            jwtIssuerOptions.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;

                configureOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                            context.Response.Headers.Add("Access-Control-Expose-Headers", "Token-Expired");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
