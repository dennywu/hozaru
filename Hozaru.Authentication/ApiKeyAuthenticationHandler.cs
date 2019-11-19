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

namespace Hozaru.Authentication
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private const string ProblemDetailsContentType = "application/problem+json";
        private readonly IApiKeyRepository _apiKeyRepository;
        private const string ApiKeyHeaderName = "X-Api-Key";
        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IApiKeyRepository getApiKeyQuery) : base(options, logger, encoder, clock)
        {
            _apiKeyRepository = getApiKeyQuery ?? throw new ArgumentNullException(nameof(getApiKeyQuery));
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
            {
                return AuthenticateResult.NoResult();
            }

            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

            if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(providedApiKey))
            {
                return AuthenticateResult.NoResult();
            }

            var existingApiKey = await _apiKeyRepository.Get(providedApiKey);

            if (existingApiKey != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existingApiKey.Owner)
                };

                //claims.AddRange(existingApiKey.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
                var identities = new List<ClaimsIdentity> { identity };
                var principal = new ClaimsPrincipal(identities);
                var ticket = new AuthenticationTicket(principal, Options.Scheme);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Invalid API Key provided.");
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
