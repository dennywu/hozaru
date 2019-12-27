using System;
using System.Collections.Generic;
using System.Text;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Identity.Users;
using System.Linq;

namespace Hozaru.Authentication
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ITokenFactory _tokenFactory;
        private readonly IRepository<RefreshToken> _refreshTokenRepo;

        public RefreshTokenService(ITokenFactory tokenFactory, IRepository<RefreshToken> refreshTokenRepo)
        {
            _tokenFactory = tokenFactory;
            _refreshTokenRepo = refreshTokenRepo;
        }

        public RefreshToken CreateRefreshToken(User user, string remoteIpAddress, double daysToExpire = 5)
        {
            var token = _tokenFactory.GenerateToken();
            var refreshToken = new RefreshToken(token, DateTime.UtcNow.AddDays(daysToExpire), user, remoteIpAddress);
            _refreshTokenRepo.Insert(refreshToken);
            //removeInActiveRefreshToken(user);
            return refreshToken;
        }

        public RefreshToken Get(string userId)
        {
            return _refreshTokenRepo.FirstOrDefault(i => i.User.Id.ToString() == userId);
        }

        public bool HasValidRefreshToken(string refreshToken)
        {
            var result = _refreshTokenRepo.FirstOrDefault(rt => rt.Token == refreshToken);
            if (result.IsNotNull() && result.Active)
                return true;
            return false;
        }

        public void Remove(string refreshToken)
        {
            _refreshTokenRepo.Delete(i => i.Token == refreshToken);
        }

        private void removeInActiveRefreshToken(User user)
        {
            var refreshTokens = _refreshTokenRepo.GetAllList(i => i.User.Id == user.Id);
            foreach(var token in refreshTokens)
            {
                if (!token.Active)
                    _refreshTokenRepo.Delete(token);
            }
        }
    }
}
