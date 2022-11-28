using Microsoft.AspNetCore.Mvc;

namespace centre_booking.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
