using Hozaru.Authentication.Dtos;
using Hozaru.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Authentication
{
    public interface IAuthService : IApplicationService
    {
        Task<LoginResponseDto> Login(LoginInputDto inputDto);
        Task<RefreshTokenResponseDto> RefreshToken(RefreshTokenInputDto inputDto);
    }
}
