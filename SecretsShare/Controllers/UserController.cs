using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretsShare.Attributes;
using SecretsShare.Models;
using SecretsShare.Managers.ManagersInterfaces;

namespace SecretsShare.Controllers
{
    /// <summary>
    /// controller for routing requests related to users
    /// </summary>
    [ApiController]
    [Route("api/user")]
    public class UserController: ControllerBase
    {
        /// <summary>
        /// abstraction of an object for working with users
        /// </summary>
        private readonly IUserManager _userManager;

        /// <summary>
        /// initializes the user's controller
        /// </summary>
        /// <param name="userManager">service for working with users</param>
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        
        /// <summary>
        /// processes a request to register a new user
        /// </summary>
        /// <param name="model">the model of information about the registered user</param>
        /// <returns>Authorized User's object</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthModel model)
        {
            var response = await _userManager.Register(model);

            if (response == null)
            {
            
                return BadRequest(new {message = "Didn't register!"});
            }

            return Ok(response);
        }
        
        /// <summary>
        /// Processes the authentication request
        /// </summary>
        /// <param name="model">user model for authorization</param>
        /// <returns>Authorized User's object</returns>
        [HttpPost("signin")]
        public IActionResult Authenticate(AuthModel model)
        {
            var response = _userManager.Authenticate(model);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }
        
        /// <summary>
        /// processes a request to update the user's refresh token if it has expired
        /// </summary>
        /// <param name="userId">unique identifier of the user</param>
        /// <param name="refreshToken">outdated token</param>
        /// <returns>new authorized user object</returns>
        [HttpPut("refresh")]
        public async Task<IActionResult> UpdateRefreshToken([FromQuery]Guid userId, [FromQuery]string refreshToken)
        {
            try
            {
                var response = await _userManager.UpdateTokensAsync(userId, refreshToken);
                if (response.RefreshToken != refreshToken)
                {
                    return BadRequest(new { message = "Token not valid" });
                }
                return Ok(response);
            }
            catch
            {
                return NoContent();
            }
        }
        
        /// <summary>
        /// processes a request to receive all files of an authorized user
        /// </summary>
        /// <param name="userId">unique identifier of the user</param>
        /// <returns>list of all files uploaded by the user</returns>
        [Authorize]
        [HttpGet("getAllFiles")]
        public IActionResult GetAllMyFiles([FromQuery] Guid userId)
        {
            var files = _userManager.GetAllUserFiles(userId);
            if (files.Any())
                return Ok(files);
            return NoContent();
        }
    }
}