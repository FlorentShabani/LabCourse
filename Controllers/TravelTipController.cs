using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Travista.Data;
using Travista.Models;
using Travista.Models.Domain;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Travista.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TravelTipController : Controller
    {
        private readonly DBContext _dBContext;

        public TravelTipController(DBContext _dBContext) 
        {
            this._dBContext = _dBContext;
        }
        public async Task<IActionResult> Index()
        {
            var traveltip = await _dBContext.TravelTips.ToListAsync();
            return View(traveltip);
        }

        public IActionResult AddTravelTip()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTravelTip(TravelTips addUserRequest)
        {
            var traveltip = new TravelTips()
            {
                ID_TravelTips = 0,
                Title = addUserRequest.Title,
                Description = addUserRequest.Description,
                TravelTips_Date = DateTime.Now,
                ID_Users = 1,
                ID_Country = 6
            };
            await _dBContext.TravelTips.AddAsync(traveltip);
            await _dBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewTravelTip(int ID_TravelTips)
        {
            var traveltip = await _dBContext.TravelTips.FirstOrDefaultAsync(x => x.ID_TravelTips == ID_TravelTips);

            if (traveltip != null)
            {
                var viewModel = new TravelTips()
                {
                    ID_TravelTips = traveltip.ID_TravelTips,
                    Title = traveltip.Title,
                    Description = traveltip.Description,
                    TravelTips_Date = traveltip.TravelTips_Date,
                    ID_Users = traveltip.ID_Users,
                    ID_Country = traveltip.ID_Country
                };

                return View("ViewTravelTip", viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewTravelTipPost(TravelTips model)
        {
            var traveltip = await _dBContext.TravelTips.FindAsync(model.ID_TravelTips);

            if (traveltip != null)
            {
                traveltip.ID_TravelTips = model.ID_TravelTips;
                traveltip.Title = model.Title;
                traveltip.Description = model.Description;
                traveltip.TravelTips_Date = DateTime.Now;
                traveltip.ID_Users = model.ID_Users;
                traveltip.ID_Country = model.ID_Country;

                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteTravelTip(TravelTips model)
        {
            var traveltip = await _dBContext.TravelTips.FindAsync(model.ID_TravelTips);

            if (traveltip != null)
            {
                _dBContext.TravelTips.Remove(traveltip);
                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
