using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Core.Dependency;
using Hozaru.Identity.MultiTenancy;
using Hozaru.Core.Runtime.Security;
using System.Diagnostics;

namespace Hozaru.Authentication
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private const string ProblemDetailsContentType = "application/problem+json";
        private readonly IRepository<ApiKey> _apiKeyRepository;
        private const string _clientIdHeaderName = "X-Api-Key";
        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _apiKeyRepository = IocManager.Instance.Resolve<IRepository<ApiKey>>();
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(_clientIdHeaderName, out var apiKeyHeaderValues))
            {
                return AuthenticateResult.NoResult();
            }

            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

            if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(providedApiKey))
            {
                return AuthenticateResult.NoResult();
            }

            var existingApiKey = await _apiKeyRepository.FirstOrDefaultAsync(i => i.Key == providedApiKey);
            if (existingApiKey != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existingApiKey.TenantId.ToString())
                };

                //claims.AddRange(existingApiKey.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
                claims.Add(new Claim(HozaruClaimTypes.TenantId, existingApiKey.TenantId.ToString()));

                var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
                var identities = new List<ClaimsIdentity> { identity };
                var principal = new ClaimsPrincipal(identities);
                var ticket = new AuthenticationTicket(principal, Options.Scheme);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Invalid Client Id provided.");
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;
            Response.ContentType = ProblemDetailsContentType;
            var problemDetails = new UnauthorizedProblemDetails();

            await Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 403;
            Response.ContentType = ProblemDetailsContentType;
            var problemDetails = new ForbiddenProblemDetails();

            await Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
        }
    }
}
