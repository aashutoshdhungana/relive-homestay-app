using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Relive.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public async Task<IActionResult> Login()
        {
            return StatusCode(500);
        }

        public async Task<IActionResult> Register()
        {
            return StatusCode(500);
        }
    }
}
