using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretsShare.Models;
using SecretsShare.Managers.ManagersInterfaces;

namespace SecretsShare.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController: ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        
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
    }
}