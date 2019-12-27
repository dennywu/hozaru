using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Authentication.Dtos;
using Hozaru.Core;
using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Identity;
using Hozaru.Identity.MultiTenancy;
using Hozaru.Identity.Roles;
using Hozaru.Identity.Users;
using System.Linq;
using Hozaru.Core.Configurations;

namespace Hozaru.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager _userManager;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IJwtFactory _jwtFactory;
        private readonly IJwtTokenValidator _jwtTokenValidator;
        private readonly JwtIssuerOptions _jwtIssuerOptions;

        public AuthService(UserManager userManager, IRefreshTokenService refreshTokenService, IJwtFactory jwtFactory, IJwtTokenValidator jwtTokenValidator, JwtIssuerOptions jwtIssuerOptions)
        {
            _userManager = userManager;
            _refreshTokenService = refreshTokenService;
            _jwtFactory = jwtFactory;
            _jwtTokenValidator = jwtTokenValidator;
            _jwtIssuerOptions = jwtIssuerOptions;
        }

        public async Task<LoginResponseDto> Login(LoginInputDto inputDto)
        {
            var loginResult = await getLoginResultAsync(inputDto.UsernameOrEmailAddress, inputDto.Password, inputDto.TenancyName);

            if(loginResult.Result == HozaruLoginResultType.Success)
            {
                var user = loginResult.User;
                var refreshToken = _refreshTokenService.CreateRefreshToken(user, inputDto.RemoteIpAddress);
                var jwtToken = await _jwtFactory.GenerateEncodedToken(user.Id.ToString(), user.UserName, user.TenantId);
                return new LoginResponseDto(jwtToken, refreshToken.Token, true);
            }

            return new LoginResponseDto(null, "", false, "");
        }

        public async Task<RefreshTokenResponseDto> RefreshToken(RefreshTokenInputDto inputDto)
        {
            var signingKey = AppSettingConfigurationHelper.GetSection("AuthSettings:SecretKey").Value;
            var claimPrincipal = _jwtTokenValidator.GetPrincipalFromToken(inputDto.AccessToken, signingKey);
            if (claimPrincipal != null)
            {
                var id = claimPrincipal.Claims.First(c => c.Type == "id");
                var user = await _userManager.FindByIdAsync(Convert.ToInt64(id.Value));

                if (_refreshTokenService.HasValidRefreshToken(inputDto.RefreshToken))
                {
                    var jwtToken = await _jwtFactory.GenerateEncodedToken(user.Id.ToString(), user.UserName, user.TenantId);
                    _refreshTokenService.Remove(inputDto.RefreshToken); // delete the token we've exchanged
                    var refreshToken = _refreshTokenService.CreateRefreshToken(user, ""); // add the new one
                    return new RefreshTokenResponseDto(jwtToken, refreshToken.Token, true);
                }
                else
                {
                    return new RefreshTokenResponseDto(null, "", false, "Invalid Refresh Token");
                }
            }
            return new RefreshTokenResponseDto(null, "", false, "Invalid Access Token");
        }

        private async Task<HozaruUserManager<Tenant, Role, User>.HozaruLoginResult> getLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _userManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case HozaruLoginResultType.Success:
                    return loginResult;
                default:
                    throw createExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private Exception createExceptionForFailedLoginAttempt(HozaruLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case HozaruLoginResultType.Success:
                    return new ApplicationException("Don't call this method with a success result!");
                case HozaruLoginResultType.InvalidUserNameOrEmailAddress:
                case HozaruLoginResultType.InvalidPassword:
                    return new HozaruException(IdentityMessages.InvalidUserNameOrPassword);
                case HozaruLoginResultType.InvalidTenancyName:
                    return new HozaruException(string.Format(IdentityMessages.ThereIsNoTenantDefinedWithName, tenancyName));
                case HozaruLoginResultType.TenantIsNotActive:
                    return new HozaruException(string.Format(IdentityMessages.TenantIsNotActive, tenancyName));
                case HozaruLoginResultType.UserIsNotActive:
                    return new HozaruException(string.Format(IdentityMessages.UserIsNotActiveAndCanNotLogin, usernameOrEmailAddress));
                case HozaruLoginResultType.UserEmailIsNotConfirmed:
                    return new HozaruException("Your email address is not confirmed. You can not login"); //TODO: localize message
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    //Logger.Warn("Unhandled login fail reason: " + result);
                    return new HozaruException(IdentityMessages.LoginFailed);
            }
        }
    }
}
