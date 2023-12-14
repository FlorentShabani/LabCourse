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
    public class LiftiController : Controller
    {
        private readonly TravistaContext _dBContext;

        public LiftiController(TravistaContext _dBContext)
        {
            this._dBContext = _dBContext;
        }
        public async Task<IActionResult> Index()
        {
            // Retrieve the cities for the specified country
            var lifti = _dBContext.Lifti.ToList();

            // Pass the list of cities to the view
            return View(lifti);
        }

        public IActionResult AddLifti()
        {
            List<Ndertesa> ndList = _dBContext.Ndertesa.ToList();
            ViewBag.Buildings = ndList;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddLiftiPost(Lifti addUserRequest)
        {

            var lifti = new Lifti()
            {
                ID_Lifti = 0,
                Emertimi = addUserRequest.Emertimi,
                ID_Ndertesa = addUserRequest.ID_Ndertesa,
            };
            await _dBContext.Lifti.AddAsync(lifti);
            await _dBContext.SaveChangesAsync();
            return Redirect(Url.Action("Index", "Lifti", new { ID_Lifti = addUserRequest.ID_Lifti }));
        }

        [HttpGet]
        public async Task<IActionResult> ViewLifti(int ID_Lifti)
        {
            var city = await _dBContext.Lifti.FirstOrDefaultAsync(x => x.ID_Lifti == ID_Lifti);

            if (city != null)
            {
                var viewModel = new Lifti()
                {
                    ID_Lifti = 0,
                    Emertimi = city.Emertimi,
                    ID_Ndertesa = city.ID_Ndertesa
                };

                return View("ViewLifti", viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewLiftiPost(Lifti model)
        {
            var city = await _dBContext.Lifti.FindAsync(model.ID_Lifti);


            if (city != null)
            {
                city.ID_Lifti = model.ID_Lifti;
                city.Emertimi = model.Emertimi;
                city.ID_Ndertesa = model.ID_Ndertesa;

                await _dBContext.SaveChangesAsync();

                return RedirectToAction("ViewLifti");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteLifti(Lifti model)
        {
            var city = await _dBContext.Lifti.FindAsync(model.ID_Lifti);

            if (city != null)
            {
                _dBContext.Lifti.Remove(city);
                await _dBContext.SaveChangesAsync();

                return Redirect(Url.Action("Index", "Lifti", new { ID_Ndertesa = model.ID_Ndertesa }));
            }

            return Redirect(Url.Action("Index", "City", new { ID_Ndertesa = model.ID_Ndertesa }));
        }
    }
}
