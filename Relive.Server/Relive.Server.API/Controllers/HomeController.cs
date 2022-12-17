using Microsoft.AspNetCore.Mvc;

namespace Relive.Server.API.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
