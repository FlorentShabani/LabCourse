
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travista.Data;
using Travista.Models.Domain;

namespace Travista.Controllers
{
    public class CityController : Controller
    {
        private readonly DBContext _dBContext;

        public CityController(DBContext _dBContext)
        {
            this._dBContext = _dBContext;
        }

        public async Task<IActionResult> Index(int ID_Country)
        {
            // Retrieve the cities for the specified country
            var cities = _dBContext.City.Where(c => c.ID_Country == ID_Country).ToList();

            // Pass the list of cities to the view
            return View(cities);
        }

        public IActionResult AddCity()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddCity(int ID_Country)
        {
            var viewModel = new City
            {
                ID_Country = ID_Country
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddCityPost(City addUserRequest)
        {

            var city = new City()
            {
                ID_City = 0,
                name = addUserRequest.name,
                ID_Country = addUserRequest.ID_Country,
            };
            await _dBContext.City.AddAsync(city);
            await _dBContext.SaveChangesAsync();
            return Redirect(Url.Action("Index", "City", new { ID_Country = addUserRequest.ID_Country }));
        }

        [HttpGet]
        public async Task<IActionResult> ViewCity(int ID_City)
        {
            var city = await _dBContext.City.FirstOrDefaultAsync(x => x.ID_City == ID_City);

            if (city != null)
            {
                var viewModel = new City()
                {
                    ID_City = city.ID_City,
                    name = city.name,
                    ID_Country = city.ID_Country,
                };

                return View("ViewCity", viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewCityPost(City model)
        {
            var city = await _dBContext.City.FindAsync(model.ID_City);


            if (city != null)
            {
                city.ID_City = model.ID_City;
                city.name = model.name;
                city.ID_Country = model.ID_Country;

                await _dBContext.SaveChangesAsync();

                return RedirectToAction("ViewCity");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCity(City model)
        {
            var city = await _dBContext.City.FindAsync(model.ID_City);

            if (city != null)
            {
                _dBContext.City.Remove(city);
                await _dBContext.SaveChangesAsync();

                return Redirect(Url.Action("Index", "City", new { ID_Country = model.ID_Country }));
            }

            return Redirect(Url.Action("Index", "City", new { ID_Country = model.ID_Country }));
        }
    }
}
