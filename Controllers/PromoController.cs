using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Travista.Areas.Identity.Data;
using Travista.Data;
using Travista.Models.Domain;

namespace Travista.Controllers
{
    public class PromoController : Controller
    {
        private readonly TravistaContext _dBContext;
        private readonly UserManager<TravistaUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public PromoController(TravistaContext _dBContext, UserManager<TravistaUser> _userManager, IWebHostEnvironment _env)
        {
            this._dBContext = _dBContext;
            this._userManager = _userManager;
            this._env = _env;
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

        public async Task<IActionResult> Index()
        {
            var promo = await _dBContext.Promo.ToListAsync();
            return View(promo);
        }

        public IActionResult AddPromo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPromo(Promo addUserRequest, IFormFile Foto)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var imagePath = await SaveImage(Foto);
            if (imagePath == null | imagePath.Equals("noimage"))
            {
                return View("AddPromo");
            }

            var Promo = new Promo()
            {
                ID_Promo = 0,
                Title = addUserRequest.Title,
                Subtitle = addUserRequest.Subtitle,
                Description = addUserRequest.Description,
                Picture = imagePath,
                DestLink = addUserRequest.DestLink,
                ID_Users = currentUser.Id
            };

            await _dBContext.Promo.AddAsync(Promo);
            await _dBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewPromo(int ID_Promo)
        {
            var promo = await _dBContext.Promo.FirstOrDefaultAsync(x => x.ID_Promo == ID_Promo);

            if (promo != null)
            {
                var viewModel = new Promo()
                {
                    ID_Promo = promo.ID_Promo,
                    Title = promo.Title,
                    Subtitle = promo.Subtitle,
                    Description = promo.Description,
                    Picture = promo.Picture,
                    DestLink = promo.DestLink,
                    ID_Users = promo.ID_Users
                };

                return View("ViewPromo", viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewPromoPost(Promo addUserRequest, IFormFile Foto)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var imagePath = await SaveImage(Foto);

            var promo = await _dBContext.Promo.FindAsync(addUserRequest.ID_Promo);

            if(promo  != null)
            {
                promo.ID_Promo = addUserRequest.ID_Promo;
                promo.Title = addUserRequest.Title;
                promo.Subtitle = addUserRequest.Subtitle;
                promo.Description = addUserRequest.Description;
                promo.DestLink = addUserRequest.DestLink;
                promo.ID_Users = currentUser.Id;
                if (imagePath != null && !imagePath.Equals("noimage"))
                {
                    promo.Picture = imagePath;
                }

                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletePromo(Promo model)
        {
            var promo = await _dBContext.Promo.FindAsync(model.ID_Promo);

            if (promo != null)
            {
                _dBContext.Promo.Remove(promo);
                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
