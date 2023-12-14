using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travista.Data;
using Travista.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Travista.Areas.Identity.Data;

namespace Travista.Controllers
{
    public class DestinationController : Controller
    {
        private readonly TravistaContext _dBContext;
        private readonly UserManager<TravistaUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public DestinationController(TravistaContext _dBContext, UserManager<TravistaUser> _userManager, IWebHostEnvironment _env)
        {
            this._dBContext = _dBContext;
            this._userManager = _userManager;
            this._env = _env;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var dest = await _dBContext.Destination.ToListAsync();
            return View(dest);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AddDestination()
        {
            return View();
        }

        [Authorize]
        private async Task<string> SaveImage(IFormFile image)
        {
            if (image != null)
            {
                var allowedExtensions = new[] { ".jpg", ".png" };
                var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    return "noimage";
                }
                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                return "~/uploads/" + fileName;
            }
            return "noimage";
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddDestination(Destination addUserRequest, List<IFormFile> img)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var Dest = new Destination()
            {
                ID_Destination = 0,
                Emri = addUserRequest.Emri,
                Tag = addUserRequest.Tag,
                Description = addUserRequest.Description,
                Address = addUserRequest.Address,
                AdditionalAddress = addUserRequest.AdditionalAddress,
                Longitude = addUserRequest.Longitude,
                Latitude = addUserRequest.Latitude,
                ID_City = addUserRequest.ID_City,
                ID_Users = currentUser.Id,
            };
            await _dBContext.Destination.AddAsync(Dest);
            await _dBContext.SaveChangesAsync();

            foreach (var tempimg in img)
            {
                var imagePath = await SaveImage(tempimg);
                if (imagePath.Equals("noimage") | imagePath == null)
                {
                    continue;
                }

                var permimg = new Images()
                {
                    ID_Destination = Dest.ID_Destination,
                    ImagePath = imagePath
                };

                await _dBContext.Images.AddAsync(permimg);
                await _dBContext.SaveChangesAsync();
            }


            return RedirectToAction("Index");
        }


        public string GetCityNameById(int cityId)
        {
            var city = _dBContext.City.FirstOrDefault(c => c.ID_City == cityId);

            if (city != null)
            {
                return city.name;
            }

            return "City not found";
        }

        public async Task<IActionResult> ShowDestination(int desiredCity)
        {
            var dest = await _dBContext.Destination
                .Include(d => d.Reviews)
                .Include(d => d.FK_City)
                .Include(d => d.FK_Users)
                .Include(d => d.Images)
                .Where(d => d.ID_City == desiredCity)
                .OrderBy(d => Guid.NewGuid())
                .Take(100)
                .ToListAsync();

            if (!dest.Any())
            {
                return RedirectToAction("Oops", "Home");
            }


            return View(dest);
        }


        public async Task<IActionResult> ShowDetails(int clickedDest)
        {
            var dest = await _dBContext.Destination
                .Include(d => d.Reviews)
                .Include(d => d.FK_City)
                .Include(d => d.FK_Users)
                .Include(d => d.Images)
                .FirstOrDefaultAsync(d => d.ID_Destination == clickedDest);

            return View(dest);
        }

        

        /*
        public async Task<IActionResult> FetchWeather(Destination userSearch)
        {
            string cityName = userSearch.FK_City.name;

            string weather = "API_KEY";
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={weather}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string weatherData = await response.Content.ReadAsStringAsync();
                    ViewBag.WeatherData = weatherData;
                }
                else
                {
                    return View("Index");
                }
            }

            return View();
        }
        */

        [Authorize]
        public IActionResult AddUserDestination()
        {
            List<City> cityList = _dBContext.City.ToList();
            ViewBag.Citys = cityList;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddUserDestination(Destination addUserRequest, List<IFormFile> img)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var Dest = new Destination()
            {
                ID_Destination = 0,
                Emri = addUserRequest.Emri,
                Tag = addUserRequest.Tag,
                Description = addUserRequest.Description,
                Address = addUserRequest.Address,
                AdditionalAddress = addUserRequest.AdditionalAddress,
                Longitude = addUserRequest.Longitude,
                Latitude = addUserRequest.Latitude,
                ID_City = addUserRequest.ID_City,
                ID_Users = currentUser.Id,
            };
            await _dBContext.Destination.AddAsync(Dest);
            await _dBContext.SaveChangesAsync();

            foreach (var tempimg in img)
            {
                var imagePath = await SaveImage(tempimg);
                if (imagePath.Equals("noimage") | imagePath == null)  
                {
                    continue;
                }

                var permimg = new Images()
                {
                    ID_Destination = Dest.ID_Destination,
                    ImagePath = imagePath
                };

                await _dBContext.Images.AddAsync(permimg);
                await _dBContext.SaveChangesAsync();
            }

            return RedirectToAction("Success", "Home");
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> ViewDestination(int ID_Destination)
        {
            var dest = await _dBContext.Destination.FirstOrDefaultAsync(x => x.ID_Destination == ID_Destination);

            if (dest != null)
            {
                var viewModel = new Destination()
                {
                    ID_Destination = dest.ID_Destination,
                    Emri = dest.Emri,
                    Tag = dest.Tag,
                    Description = dest.Description,
                    Address = dest.Address,
                    AdditionalAddress = dest.AdditionalAddress,
                    Longitude = dest.Longitude,
                    Latitude = dest.Latitude,
                    ID_City = dest.ID_City,
                    ID_Users = dest.ID_Users,
                };

                return View("ViewDestination", viewModel);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> ViewDestinationPost(Destination model)
        {
            var dest = await _dBContext.Destination.FindAsync(model.ID_Destination);

            var currentUser = await _userManager.GetUserAsync(User);

            if (dest != null)
            {
                dest.ID_Destination = model.ID_Destination;
                dest.Emri = model.Emri;
                dest.Tag = model.Tag;
                dest.Description = model.Description;
                dest.Address = model.Address;
                dest.AdditionalAddress = model.AdditionalAddress;
                dest.Longitude = model.Longitude;
                dest.Latitude = model.Latitude;
                dest.ID_City = model.ID_City;
                dest.ID_Users = currentUser.Id;

                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteDestination(Destination model)
        {
            var dest = await _dBContext.Destination.FindAsync(model.ID_Destination);

            if (dest != null)
            {
                var relatedReview = await _dBContext.Review
                .Where(rev => rev.ID_Destination == model.ID_Destination)
                .ToListAsync();

                foreach (var rev in relatedReview)
                {
                    _dBContext.Review.Remove(rev);
                }


                _dBContext.Destination.Remove(dest);
                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
