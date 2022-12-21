using System;
using SecretsShare.DTO;

namespace SecretsShare.HelperObject
{
    /// <summary>
    /// a class for sending a response to authentication requests
    /// </summary>
    public class AuthenticateResponse
    {
        /// <summary>
        /// the unique identifier of the authenticated user
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// user's email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// jwt token of the current user session
        /// </summary>
        public string AccessToken { get; set; }
        
        /// <summary>
        /// jwt token for updating or receiving a jwt token of an authorized user's session
        /// </summary>
        public string RefreshToken { get; set; }
        
        /// <summary>
        /// initializes the authentication response
        /// </summary>
        /// <param name="user">the entity of the authorized user</param>
        /// <param name="token">jwt token of the current user session</param>
        /// <param name="refreshToken">jwt refresh token</param>
        public AuthenticateResponse(User user, string token, string refreshToken)
        {
            Id = user.Id;
            Email = user.Email;
            AccessToken = token;
            RefreshToken = refreshToken;
        }
    }
}