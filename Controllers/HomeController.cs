using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Travista.Data;
using Travista.Models.Domain;
using TravistaASP.Models;

namespace TravistaASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TravistaContext _dBContext;

        public HomeController(ILogger<HomeController> logger, TravistaContext _dBContext)
        {
            _logger = logger;
            this._dBContext = _dBContext;
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
        public IActionResult GetSearchValue(string search)
        {
            List<City> allsearch = _dBContext.City
                .Where(x => x.name.Contains(search))
                .Select(x => new City
                {
                    ID_City = x.ID_City,
                    name = x.name
                })
                .ToList();

            return Json(allsearch);
        }

        [HttpPost]
        public async Task<IActionResult> GetPostSearchValue(string search)
        {
            var city = await _dBContext.City.FirstOrDefaultAsync(x => x.name.ToLower() == search.ToLower());

            if (city != null)
            {
                int ID_City = city.ID_City;
            }

            // Use the 'search' parameter in your logic
            return RedirectToAction("ShowDestination", "Destination", new { Id = city.ID_City });
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