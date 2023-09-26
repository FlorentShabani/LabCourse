using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Travista.Data;
using Travista.Models.Domain;
using Travista.Models;
using Microsoft.AspNetCore.Identity;
using Travista.Areas.Identity.Data;

namespace Travista.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TravistaContext _dBContext;
        private readonly UserManager<TravistaUser> _userManager;

        public HomeController(ILogger<HomeController> _logger, TravistaContext _dBContext, UserManager<TravistaUser> _userManager)
        {
            this._logger = _logger;
            this._dBContext = _dBContext;
            this._userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageModel
            {
                DestinationData = await _dBContext.Destination.FromSqlRaw("EXEC GetRandomDest").ToListAsync(),
                TravelAgencyData = await _dBContext.TravelAgency.FromSqlRaw("EXEC GetRandomTravelAgencies").ToListAsync(),
                PromoData = await _dBContext.Promo.FromSqlRaw("EXEC GetTop3PromoItems").ToListAsync(),
            };

            foreach (var destination in model.DestinationData)
            {
                destination.Images = await _dBContext.Images
                    .Where(img => img.ID_Destination == destination.ID_Destination)
                    .ToListAsync();
            }

            foreach (var travelagency in model.TravelAgencyData)
            {
                travelagency.Images = await _dBContext.Images
                    .Where(img => img.ID_TravelAgency == travelagency.ID_TravelAgency)
                    .ToListAsync();
            }

            return View(model);
        }

        public IActionResult Oops()
        {
            return View("Oops");
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
            if (string.IsNullOrWhiteSpace(search))
            {
                return RedirectToAction("Oops", "Home");
            }

            var city = await _dBContext.City.FirstOrDefaultAsync(x => x.name.ToLower() == search.ToLower());

            int ID_City;

            if (city != null)
            {
                ID_City = city.ID_City;
                return RedirectToAction("ShowDestination", "Destination", new { desiredCity = city.ID_City });
            }

            return RedirectToAction("Oops", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddContactUs(IFormCollection form)
        {

            var currentUser = await _userManager.GetUserAsync(User);

            var name = form["Name"];
            var subject = form["Subject"];
            var message = form["Message"];
            var email = form["Email"];

            var contactus = new ContactUs()
            {
                ID_ContactUs = 0,
                Name = name,
                Subject = subject,
                Message = message,
                Email = email,
                ID_Users = currentUser.Id
            };

            await _dBContext.ContactUs.AddAsync(contactus);
            await _dBContext.SaveChangesAsync();

            return RedirectToAction("Success");
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