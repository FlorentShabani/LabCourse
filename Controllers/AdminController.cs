using Microsoft.AspNetCore.Mvc;

namespace Travista.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            ViewData["Title"] = "Manage Users";
            return View("~/Views/Users/Index.cshtml");
        }
    }
}
