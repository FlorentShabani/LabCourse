using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travista.Data;
using Travista.Areas.Identity.Data;
using Microsoft.Extensions.Options;
using System.Security.Claims;


namespace Travista.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly UserManager<TravistaUser> _userManager;
        private readonly TravistaContext _dbContext;

        public UsersController(UserManager<TravistaUser> userManager, TravistaContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> ViewUsers(string Id)
        {
            var travistauser = await _userManager.FindByIdAsync(Id);

            if (travistauser != null)
            {
                var viewModel = new TravistaUser()
                {
                    Id = travistauser.Id,
                    Fullname = travistauser.Fullname,
                    UserName = travistauser.UserName,
                    Email = travistauser.Email,
                };

                return View("ViewUser", viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewUserPost(TravistaUser model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user != null)
            {
                user.Id = model.Id;
                user.Fullname = model.Fullname;
                user.UserName = model.UserName;
                user.Email = model.Email;

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                // User not found, handle the error (e.g., display an error message)
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                // User deleted successfully, redirect or display a success message
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
    }
    }
}