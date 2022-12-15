using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Secret_Share.Models;

namespace Secret_Share.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController: ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthModel model)
        {
            return Ok();
        }
        
        [HttpPost("signin")]
        public IActionResult Authenticate(AuthModel model)
        {
            return Ok();
        }
    }
}