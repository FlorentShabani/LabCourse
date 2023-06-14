using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Travista.Data;
using Travista.Models;
using Travista.Models.Domain;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Travista.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DestinationController : Controller
    {
        private readonly DBContext _dBContext;
        private readonly IWebHostEnvironment _env;

        public DestinationController(DBContext _dBContext, IWebHostEnvironment env)
        {
            this._dBContext = _dBContext;
            this._env = env;
        }

        public async Task<IActionResult> Index()
        {
            var dest = await _dBContext.Destination.ToListAsync();
            return View(dest);
        }

        public IActionResult AddDestination()
        {
            return View();
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            if (image != null)
            {
                var allowedExtensions = new[] { ".jpg", ".png" };
                var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("image", "Invalid file type. Only JPG and PNG files are allowed.");
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

        [HttpPost]
        public async Task<IActionResult> AddDestination(Destination addUserRequest, IFormFile Foto)
        {
            var imagePath = await SaveImage(Foto);
            if (imagePath == null)
            {
                return View("AddDestination");
            }

            var Dest = new Destination()
            {
                ID_Destination = 0,
                Emri = addUserRequest.Emri,
                Description = addUserRequest.Description,
                Address = addUserRequest.Address,
                AdditionalAddress = addUserRequest.AdditionalAddress,
                Longitude = addUserRequest.Longitude,
                Latitude = addUserRequest.Latitude,
                Foto = imagePath,
                ID_City = addUserRequest.ID_City,
                ID_Users = addUserRequest.ID_Users,
            };
            await _dBContext.Destination.AddAsync(Dest);
            await _dBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
                    Description = dest.Description,
                    Address = dest.Address,
                    AdditionalAddress = dest.AdditionalAddress,
                    Foto = dest.Foto,
                    Longitude = dest.Longitude,
                    Latitude = dest.Latitude,
                    ID_City = dest.ID_City,
                    ID_Users = dest.ID_Users,
                };

                return View("ViewDestination", viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewDestinationPost(Destination model, IFormFile Foto)
        {
            var dest = await _dBContext.Destination.FindAsync(model.ID_Destination);

            var imagePath = await SaveImage(Foto);
            if (imagePath == null)
            {
                return View(model);
            }

            if (dest != null)
            {
                dest.ID_Destination = model.ID_Destination;
                dest.Emri = model.Emri;
                dest.Description = model.Description;
                dest.Address = model.Address;
                dest.AdditionalAddress = model.AdditionalAddress;
                dest.Foto = imagePath;
                dest.Longitude = model.Longitude;
                dest.Latitude = model.Latitude;
                dest.ID_City = model.ID_City;
                dest.ID_Users = model.ID_Users;

                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteDestination(Destination model)
        {
            var dest = await _dBContext.Destination.FindAsync(model.ID_Destination);

            if (dest != null)
            {
                _dBContext.Destination.Remove(dest);
                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
