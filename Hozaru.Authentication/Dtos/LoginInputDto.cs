using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Authentication.Dtos
{
    public class LoginInputDto : IInputDto
    {
        public string UsernameOrEmailAddress { get; set; }
        public string Password { get; set; }
        public string RemoteIpAddress { get; set; }
        public string TenancyName { get; set; }
    }
}
