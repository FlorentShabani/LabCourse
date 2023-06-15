using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravistaASP.Models;

namespace TravistaASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            ViewData["Title"] = "Sign in";
            return View("~/Views/Home/Forms/login.cshtml");

        }

        public IActionResult Signup()
        {
            ViewData["Title"] = "Sign up";
            return View("~/Views/Home/Forms/signup.cshtml");
        }

        public IActionResult Success()
        {
            return View("~/Views/Home/Success.cshtml");
        }

        public IActionResult Review() 
        {
            ViewData["Title"] = "Create a review for a destination";
            return View("~/Views/Home/Forms/review_form.cshtml");
        }
        public IActionResult TravelAgency()
        {
            ViewData["Title"] = "Create a travel agency for Travista";
            return View("~/Views/Home/Forms/travelagency_form.cshtml");
        }
        public IActionResult TravelTips()
        {
            ViewData["Title"] = "Create a travel tip for Travista";
            return View("~/Views/Home/Forms/traveltips_form.cshtml");
        }

        public IActionResult Dashboard() 
        {
            ViewData["Title"] = "Admin Dashboard";
            return View("~/Views/Home/Dashboard/dashboard.cshtml");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}