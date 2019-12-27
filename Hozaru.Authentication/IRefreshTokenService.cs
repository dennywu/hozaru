using Hozaru.Core.Application.Services;
using Hozaru.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Authentication
{
    public interface IRefreshTokenService : IApplicationService
    {
        RefreshToken CreateRefreshToken(User user, string remoteIpAddress, double daysToExpire = 5);
        RefreshToken Get(string value);
        bool HasValidRefreshToken(string refreshToken);
        void Remove(string refreshToken);
    }
}
