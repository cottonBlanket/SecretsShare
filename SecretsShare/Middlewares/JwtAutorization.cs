using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecretsShare.Managers.ManagersInterfaces;

namespace SecretsShare.Middlewares
{
    /// <summary>
    /// request processing pipeline class
    /// </summary>
    public class JwtMiddleware
    {
        /// <summary>
        /// function to call the next pipeline component
        /// </summary>
        private readonly RequestDelegate _next;
        
        /// <summary>
        /// application configuration
        /// </summary>
        private readonly IConfiguration _configuration;
    
        /// <summary>
        /// class constructor receiving delegate and configuration
        /// </summary>
        /// <param name="next">the next component of the pipeline</param>
        /// <param name="configuration">application configuration</param>
        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        /// <summary>
        /// gets their authorization token request headers and checks it for validity
        /// </summary>
        /// <param name="context">request context</param>
        /// <param name="userManager">service for working with user entities</param>
        public async Task Invoke(HttpContext context, IUserManager userManager)
        {
            var token1 = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ");
            var token = token1?.Last();

            if (token != null)
                AttachUserToContext(context, userManager, token);

            await _next(context);
        }
        
        /// <summary>
        /// adds the "user" header to the response context if the authorization token is valid
        /// </summary>
        /// <param name="context">request context</param>
        /// <param name="userManager">service for working with user entities</param>
        /// <param name="token">jwt token</param>
        public void AttachUserToContext(HttpContext context, IUserManager userManager, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                // min 16 characters
                var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    RoleClaimType = "User",
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);
    
                context.Items["User"] = userManager.GetById(userId);
            }
            catch
            {
                // todo: need to add logger
            }
        }
    }
}
