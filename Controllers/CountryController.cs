
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Travista.Data;
using Travista.Models.Domain;

namespace Travista.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CountryController : Controller
    {
        private readonly DBContext _dBContext;

        public CountryController(DBContext _dBContext)
        {
            this._dBContext = _dBContext;
        }

        public async Task<IActionResult> Index()
        {
            var country = await _dBContext.Country.ToListAsync();
            return View(country);
        }

        public IActionResult AddCountry()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddCountry(Country addUserRequest)
        {

            var Country = new Country()
            {
                ID_Country = 0,
                name = addUserRequest.name,
                language = addUserRequest.language
            };
            await _dBContext.Country.AddAsync(Country);
            await _dBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewCountry(int ID_Country)
        {
            var country = await _dBContext.Country.FirstOrDefaultAsync(x => x.ID_Country == ID_Country);

            if (country != null)
            {
                var viewModel = new Country()
                {
                    ID_Country = country.ID_Country,
                    name = country.name,
                    language = country.language
                };

                return View("ViewCountry", viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewCountryPost(Country model)
        {
            var country = await _dBContext.Country.FindAsync(model.ID_Country);


            if (country != null)
            {
                country.ID_Country = model.ID_Country;
                country.name = model.name;
                country.language = model.language;

                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCountry(Country model)
        {
            var country = await _dBContext.Country.FindAsync(model.ID_Country);

            if (country != null)
            {
                _dBContext.Country.Remove(country);
                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
