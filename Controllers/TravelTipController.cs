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
using Microsoft.AspNetCore.Identity;
using Travista.Areas.Identity.Data;

namespace Travista.Controllers
{
    public class TravelTipController : Controller
    {
        private readonly TravistaContext _dBContext;
        private readonly UserManager<TravistaUser> _userManager;

        public TravelTipController(TravistaContext _dBContext, UserManager<TravistaUser> userManager) 
        {
            this._dBContext = _dBContext;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var traveltip = await _dBContext.TravelTips.ToListAsync();
            return View(traveltip);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AddTravelTip()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddTravelTip(TravelTips addUserRequest)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var traveltip = new TravelTips()
            {
                ID_TravelTips = 0,
                Title = addUserRequest.Title,
                Description = addUserRequest.Description,
                TravelTips_Date = DateTime.Now,
                ID_Users = currentUser.Id,
                ID_Country = addUserRequest.ID_Country
            };
            await _dBContext.TravelTips.AddAsync(traveltip);
            await _dBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult AddUserTravelTip()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddUserTravelTip(TravelTips addUserRequest)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var traveltip = new TravelTips()
            {
                ID_TravelTips = 0,
                Title = addUserRequest.Title,
                Description = addUserRequest.Description,
                TravelTips_Date = DateTime.Now,
                ID_Users = currentUser.Id,
                ID_Country = addUserRequest.ID_Country
            };
            await _dBContext.TravelTips.AddAsync(traveltip);
            await _dBContext.SaveChangesAsync();
            return RedirectToAction("Success", "Home");
        }


        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> ViewTravelTips(int ID_TravelTips)
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


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> ViewTravelTipsPost(TravelTips model)
        {
            var traveltip = await _dBContext.TravelTips.FindAsync(model.ID_TravelTips);

            var currentUser = await _userManager.GetUserAsync(User);

            if (traveltip != null)
            {
                traveltip.ID_TravelTips = model.ID_TravelTips;
                traveltip.Title = model.Title;
                traveltip.Description = model.Description;
                traveltip.TravelTips_Date = DateTime.Now;
                traveltip.ID_Users = currentUser.Id;
                traveltip.ID_Country = model.ID_Country;

                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
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
