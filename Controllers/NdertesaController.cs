using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Travista.Data;
using Travista.Models.Domain;

namespace Travista.Controllers
{
    public class NdertesaController : Controller
    {
        private readonly TravistaContext _dBContext;

        public NdertesaController(TravistaContext _dBContext)
        {
            this._dBContext = _dBContext;
        }
        public async Task<IActionResult> Index()
        {
            // Retrieve the cities for the specified country
            var ndertesa = _dBContext.Ndertesa.ToList();

            // Pass the list of cities to the view
            return View(ndertesa);
        }

        public IActionResult AddNdertesa()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNdertesaPost(Ndertesa addUserRequest)
        {

            var lifti = new Ndertesa()
            {
                ID_Ndertesa = 0,
                Emertimi = addUserRequest.Emertimi,
                DataPT = addUserRequest.DataPT,
            };
            await _dBContext.Ndertesa.AddAsync(lifti);
            await _dBContext.SaveChangesAsync();
            return Redirect(Url.Action("Index", "Ndertesa", new { ID_Ndertesa = addUserRequest.ID_Ndertesa }));
        }

        [HttpGet]
        public async Task<IActionResult> ViewNdertesa(int ID_Ndertesa)
        {
            var city = await _dBContext.Ndertesa.FirstOrDefaultAsync(x => x.ID_Ndertesa == ID_Ndertesa);

            if (city != null)
            {
                var viewModel = new Ndertesa()
                {
                    ID_Ndertesa = 0,
                    Emertimi = city.Emertimi,
                    DataPT = city.DataPT
                };

                return View("ViewNdertesa", viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewNdertesaPost(Ndertesa model)
        {
            var city = await _dBContext.Ndertesa.FindAsync(model.ID_Ndertesa);


            if (city != null)
            {
                city.ID_Ndertesa = model.ID_Ndertesa;
                city.Emertimi = model.Emertimi;
                city.DataPT = model.DataPT;

                await _dBContext.SaveChangesAsync();

                return RedirectToAction("ViewNdertesa");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteNdertesa(Ndertesa model)
        {
            var city = await _dBContext.Ndertesa.FindAsync(model.ID_Ndertesa);

            if (city != null)
            {
                _dBContext.Ndertesa.Remove(city);
                await _dBContext.SaveChangesAsync();

                return Redirect(Url.Action("Index", "Ndertesa", new { ID_Ndertesa = model.ID_Ndertesa }));
            }

            return Redirect(Url.Action("Index", "Ndertesa", new { ID_Ndertesa = model.ID_Ndertesa }));
        }
    }
}
