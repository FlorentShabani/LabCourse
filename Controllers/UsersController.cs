using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Data;
using Travista.Data;
using Travista.Models;
using Travista.Models.Domain;

namespace Travista.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly DBContext MVCDBContext;

        public UsersController(DBContext MVCDBContext)
        {
            this.MVCDBContext = MVCDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await MVCDBContext.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel addUserRequest)
        {
            var User = new User()
            {
                ID_Users = 0,
                Fullname = addUserRequest.Fullname,
                Username = addUserRequest.Username,
                Email = addUserRequest.Email,
                Birthdate = addUserRequest.Birthdate,
                Password = addUserRequest.Password,
                role = addUserRequest.role
            };

            await MVCDBContext.Users.AddAsync(User);
            await MVCDBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(int ID_Users)
        {
            var user = await MVCDBContext.Users.FirstOrDefaultAsync(x => x.ID_Users == ID_Users);

            if(user != null) 
            {
                var viewModel = new UpdateUserViewModel()
                {
                    ID_Users = user.ID_Users,
                    Fullname = user.Fullname,
                    Username = user.Username,
                    Email = user.Email,
                    Birthdate = user.Birthdate,
                    role = user.role
                };

                return View("View",viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateUserViewModel model)
        {
            var user = await MVCDBContext.Users.FindAsync(model.ID_Users);

            if(user != null)
            {
                user.Fullname = model.Fullname;
                user.Username = model.Username;
                user.Email = model.Email;
                user.Birthdate = model.Birthdate;
                user.Password = model.Password;
                user.role = model.role;

                await MVCDBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateUserViewModel model)
        {
            var user = await MVCDBContext.Users.FindAsync(model.ID_Users);

            if(user != null) 
            {
                MVCDBContext.Users.Remove(user);
                await MVCDBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
