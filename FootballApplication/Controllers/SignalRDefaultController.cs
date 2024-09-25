using Microsoft.AspNetCore.Mvc;

namespace FootballApplication1.Controllers
{
    public class SignalRDefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
