using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Travista.Data;
using Travista.Models;
using Travista.Models.Domain;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace Travista.Controllers
{
    public class ReviewController : Controller
    {

        private readonly DBContext _dBContext;

        public ReviewController(DBContext _dBContext)
        {
            this._dBContext = _dBContext;
        }

        public async Task<IActionResult> Index()
        {
            var review = await _dBContext.Review.ToListAsync();
            return View(review);
        }

        public IActionResult AddReview()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(Review addUserRequest)
        {
            var review = new Review()
            {
                ID_Reviews = 0,
                Rating = addUserRequest.Rating,
                Comment = addUserRequest.Comment,
                Review_Date = DateTime.Now,
                ID_Users = addUserRequest.ID_Users,
                ID_Destination = addUserRequest.ID_Destination,
            };
            await _dBContext.Review.AddAsync(review);
            await _dBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public async Task<IActionResult> ViewReviewPost(Review model)
        {
            var review = await _dBContext.Review.FindAsync(model.ID_Reviews);

            if (review != null)
            {
                review.ID_Reviews = model.ID_Reviews;
                review.Rating = model.Rating;
                review.Comment = model.Comment;
                review.Review_Date = model.Review_Date;
                review.ID_Users = model.ID_Users;
                review.ID_Destination = model.ID_Destination;

                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

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
