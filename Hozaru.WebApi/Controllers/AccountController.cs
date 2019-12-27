using Hozaru.Authentication;
using Hozaru.Authentication.Dtos;
using Hozaru.Core;
using Hozaru.Core.Dependency;
using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Identity;
using Hozaru.Identity.MultiTenancy;
using Hozaru.Identity.Roles;
using Hozaru.Identity.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hozaru.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : HozaruApiController
    {
        private readonly IAuthService _authService;

        public AccountController()
        {
            _authService = IocManager.Instance.Resolve<IAuthService>();
        }

        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult> Authenticate(LoginInputDto inputDto)
        {
            var response = await _authService.Login(inputDto);
            return new ContentResult
            {
                ContentType = "application/json",
                Content = JsonSerializer.Serialize(response),
                StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError)
            };
        }

        [HttpPost]
        [Route("refreshtoken")]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshToken(RefreshTokenInputDto inputDto)
        {
            var response = await _authService.RefreshToken(inputDto);
            return new ContentResult
            {
                ContentType = "application/json",
                Content = JsonSerializer.Serialize(response),
                StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError)
            };
        }
    }
}