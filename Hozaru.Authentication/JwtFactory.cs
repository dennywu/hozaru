using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Authentication.Dtos;
using Hozaru.Core.Domain.Uow;
using Hozaru.Core.Runtime.Security;
using Hozaru.Identity.Users;

namespace Hozaru.Authentication
{
    public class JwtFactory : IJwtFactory
    {
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public JwtFactory(IJwtTokenHandler jwtTokenHandler, JwtIssuerOptions jwtOptions, UserManager userManager, IUnitOfWorkManager unitOfWorkManager)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _jwtOptions = jwtOptions;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<AccessToken> GenerateEncodedToken(string id, string userName, int? tenantId)
        {
            var identity = generateClaimsIdentity(id, userName);
            _unitOfWorkManager.Current.DisableFilter(HozaruDataFilters.MayHaveTenant);
            _unitOfWorkManager.Current.DisableFilter(HozaruDataFilters.MustHaveTenant);
            _unitOfWorkManager.Current.EnableFilter(HozaruDataFilters.MayHaveTenant);
            using (_unitOfWorkManager.Current.SetFilterParameter(HozaruDataFilters.MayHaveTenant, HozaruDataFilters.Parameters.TenantId, tenantId))
            {
                var user = await _userManager.FindByNameAsync(userName);

                var claims = new[]
                {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(HozaruClaimTypes.TenantId, user.TenantId.ToString()),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Rol),
                 identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Id)
             };

                var jwt = new JwtSecurityToken(
                    _jwtOptions.Issuer,
                    _jwtOptions.Audience,
                    claims,
                    _jwtOptions.NotBefore,
                    _jwtOptions.Expiration,
                    _jwtOptions.SigningCredentials);

                return new AccessToken(_jwtTokenHandler.WriteToken(jwt), (int)_jwtOptions.ValidFor.TotalSeconds);
            }
        }

        private static ClaimsIdentity generateClaimsIdentity(string id, string userName)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess)
            });
        }
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);
    }
}
