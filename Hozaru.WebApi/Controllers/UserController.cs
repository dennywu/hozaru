using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Hozaru.Core.Dependency;
using Hozaru.Identity.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozaru.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager _userManager;

        public UserController()
        {
            _userManager = IocManager.Instance.Resolve<UserManager>();
        }

        [HttpGet]
        [Route("me")]
        public async Task<object> GetUser()
        {
            var user = await _userManager.FindByIdAsync(Convert.ToInt64(User.FindFirst("id").Value));
            return new
            {
                userName = user.UserName,
                email = user.EmailAddress,
                name = user.FullName
            };
        }
    }
}