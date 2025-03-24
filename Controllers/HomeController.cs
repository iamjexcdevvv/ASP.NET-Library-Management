using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
