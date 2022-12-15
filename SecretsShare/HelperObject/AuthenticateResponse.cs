using System;
using SecretsShare.DTO;

namespace SecretsShare.HelperObject
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string token, string refreshToken)
        {
            Id = user.Id;
            Email = user.Email;
            AccessToken = token;
            RefreshToken = refreshToken;
        }
    }
}