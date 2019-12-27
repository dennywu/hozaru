using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Authentication.Dtos
{
    public class LoginResponseDto
    {
        public bool Success { get; }
        public string Message { get; }
        public AccessToken AccessToken { get; }
        public string RefreshToken { get; }

        public LoginResponseDto(AccessToken accessToken, string refreshToken, bool success = false, string message = null)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Success = success;
            Message = message;
        }
    }
}
