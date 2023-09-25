using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Travista.Data;
using Travista.Models.Domain;

namespace Travista.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private readonly TravistaContext _dBContext;
        private readonly IWebHostEnvironment _env;

        public ImageController(TravistaContext _dBContext, IWebHostEnvironment _env)
        {
            this._dBContext = _dBContext;
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

        [Authorize]
        public async Task<IActionResult> IndexDestination(int clickedDest)
        {
            var images = await _dBContext.Images.Where(c => c.ID_Destination == clickedDest).ToListAsync();

            return View(images);
        }

        [Authorize]
        public async Task<IActionResult> IndexTravelAgency(int clickedTravel)
        {
            var images = await _dBContext.Images.Where(c => c.ID_TravelAgency == clickedTravel).ToListAsync();

            return View(images);
        }

        [Authorize]
        public IActionResult AddDestinationImages()
        {
            return View();
        }

        [Authorize]
        public IActionResult AddTravelAgencyImages()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddDestinationImages(int ID_Destination)
        {
            var viewModel = new Images
            {
                ID_Destination = ID_Destination
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddTravelAgencyImages(int ID_TravelAgency)
        {
            var viewModel = new Images
            {
                ID_TravelAgency = ID_TravelAgency
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddDestinationImagesPost(int ID_Destination, List<IFormFile> img)
        {
            foreach (var tempimg in img)
            {
                var imagePath = await SaveImage(tempimg);
                if (imagePath.Equals("noimage") | imagePath == null)
                {
                    continue;
                }

                var permimg = new Images()
                {
                    ID_Destination = ID_Destination,
                    ImagePath = imagePath
                };

                await _dBContext.Images.AddAsync(permimg);
                await _dBContext.SaveChangesAsync();
            }

            return RedirectToAction("IndexDestination", "Image", new { clickedDest = ID_Destination });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTravelAgencyImagesPost(int ID_TravelAgency, List<IFormFile> img)
        {

            foreach (var tempimg in img)
            {
                var imagePath = await SaveImage(tempimg);
                if (imagePath.Equals("noimage") | imagePath == null)
                {
                    continue;
                }

                var permimg = new Images()
                {
                    ID_TravelAgency = ID_TravelAgency,
                    ImagePath = imagePath
                };

                await _dBContext.Images.AddAsync(permimg);
                await _dBContext.SaveChangesAsync();
            }

            return RedirectToAction("IndexTravelAgency", "Image", new { clickedTravel = ID_TravelAgency });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ViewDestinationImages(int clickedDest)
        {
            var images = await _dBContext.Images.FirstOrDefaultAsync(x => x.ID_Image == clickedDest);

            if (images != null)
            {
                var viewModel = new Images()
                {
                    ID_Image = images.ID_Image,
                    ImagePath = images.ImagePath,
                    ID_Destination = images.ID_Destination,
                };

                return View("ViewDestinationImages", viewModel);
            }
            return RedirectToAction("IndexDestination");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ViewTravelAgencyImages(int clickedTravel)
        {
            var images = await _dBContext.Images.FirstOrDefaultAsync(x => x.ID_Image == clickedTravel);

            if (images != null)
            {
                var viewModel = new Images()
                {
                    ID_Image = images.ID_Image,
                    ImagePath = images.ImagePath,
                    ID_TravelAgency = images.ID_TravelAgency,
                };

                return View("ViewTravelAgencyImages", viewModel);
            }
            return RedirectToAction("IndexTravelAgency");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ViewDestinationImagesPost(Images model, IFormFile img)
        {
            var imgModel = await _dBContext.Images.FindAsync(model.ID_Image);

            var tempimg = await SaveImage(img);
            if (tempimg == null | tempimg.Equals("noimage"))
            {
                return RedirectToAction("IndexTravelAgency", "Image", new { clickedDest = model.ID_Destination });
            }

            if(imgModel != null)
            {
                imgModel.ID_Image = model.ID_Image;
                imgModel.ID_Destination = model.ID_Destination;
                imgModel.ImagePath = tempimg;

                await _dBContext.SaveChangesAsync();
            }

            return RedirectToAction("IndexDestination", "Image", new { clickedDest = model.ID_Destination });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ViewTravelAgencyImagesPost(Images model, IFormFile img)
        {
            var imgModel = await _dBContext.Images.FindAsync(model.ID_Image);

            var tempimg = await SaveImage(img);
            if (tempimg == null | tempimg.Equals("noimage"))
            {
                return RedirectToAction("IndexTravelAgency", "Image", new { clickedTravel = model.ID_TravelAgency });
            }

            if (imgModel != null)
            {
                imgModel.ID_Image = model.ID_Image;
                imgModel.ID_TravelAgency = model.ID_TravelAgency;
                imgModel.ImagePath = tempimg;

                await _dBContext.SaveChangesAsync();
            }

            return RedirectToAction("IndexTravelAgency", "Image", new { clickedTravel = model.ID_TravelAgency });
        }

        [Authorize]
        public async Task<IActionResult> DeleteImagesDest(Images model)
        {
            var images = await _dBContext.Images.FindAsync(model.ID_Image);

            if (images != null)
            {
                _dBContext.Images.Remove(images);
                await _dBContext.SaveChangesAsync();
            }

            return Redirect(Url.Action("IndexDestination", "Image", new { clickedDest = images.ID_Destination }));

            //return Redirect(Url.Action("IndexDestination", "Image", new { ID_Destination = model.ID_Destination }));
        }

        [Authorize]
        public async Task<IActionResult> DeleteImagesTravel(Images model)
        {
            var images = await _dBContext.Images.FindAsync(model.ID_Image);

            if (images != null)
            {
                _dBContext.Images.Remove(images);
                await _dBContext.SaveChangesAsync();
            }

            return Redirect(Url.Action("IndexTravelAgency", "Image", new { clickedTravel = images.ID_TravelAgency }));
        }
    }
}