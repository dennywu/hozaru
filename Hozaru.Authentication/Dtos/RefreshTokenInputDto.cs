using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Authentication.Dtos
{
    public class RefreshTokenInputDto : IInputDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
