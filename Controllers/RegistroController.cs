
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Controllers
{
    [Area("Identity")]
    [Route("Identity/Account/Register")]
    public class RegistroController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar()
        {
            return NotFound();
        }
    }
}
