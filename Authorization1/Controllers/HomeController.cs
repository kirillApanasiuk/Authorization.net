using Microsoft.AspNetCore.Mvc;

namespace Authorization1.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
