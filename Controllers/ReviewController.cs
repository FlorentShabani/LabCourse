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
    public class ReviewController : Controller
    {

        private readonly TravistaContext _dBContext;
        private readonly UserManager<TravistaUser> _userManager;

        public ReviewController(TravistaContext _dBContext, UserManager<TravistaUser> userManager)
        {
            this._dBContext = _dBContext;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var review = await _dBContext.Review.ToListAsync();
            return View(review);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AddReview()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddReview(Review addUserRequest)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var review = new Review()
            {
                ID_Reviews = 0,
                Rating = addUserRequest.Rating,
                Comment = addUserRequest.Comment,
                Review_Date = DateTime.Now,
                ID_Users = currentUser.Id,
                ID_Destination = addUserRequest.ID_Destination,
            };
            await _dBContext.Review.AddAsync(review);
            await _dBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult AddUserReview()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddUserReview(int clickedDest)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var review = new Review()
            {
                ID_Users = currentUser.Fullname,
                ID_Destination = clickedDest
            };
            return View(review);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddUserReviewPost(Review addUserRequest)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var review = new Review()
            {
                ID_Reviews = 0,
                Rating = addUserRequest.Rating,
                Comment = addUserRequest.Comment,
                Review_Date = DateTime.Now,
                ID_Users = currentUser.Id,
                ID_Destination = addUserRequest.ID_Destination,
            };
            await _dBContext.Review.AddAsync(review);
            await _dBContext.SaveChangesAsync();
            return RedirectToAction("Success", "Home");
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> ViewReview(int ID_Reviews)
        {
            var review = await _dBContext.Review.FirstOrDefaultAsync(x => x.ID_Reviews == ID_Reviews);

            if (review != null)
            {
                var viewModel = new Review()
                {
                    ID_Reviews = review.ID_Reviews,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    Review_Date = review.Review_Date,
                    ID_Users = review.ID_Users,
                    ID_Destination = review.ID_Destination
                };

                return View("ViewReview", viewModel);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> ViewReviewPost(Review model)
        {
            var review = await _dBContext.Review.FindAsync(model.ID_Reviews);

            var currentUser = await _userManager.GetUserAsync(User);

            if (review != null)
            {
                review.ID_Reviews = model.ID_Reviews;
                review.Rating = model.Rating;
                review.Comment = model.Comment;
                review.Review_Date = model.Review_Date;
                review.ID_Users = currentUser.Id;
                review.ID_Destination = model.ID_Destination;

                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteReview(Review model)
        {
            var review = await _dBContext.Review.FindAsync(model.ID_Reviews);

            if (review != null)
            {
                _dBContext.Review.Remove(review);
                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
