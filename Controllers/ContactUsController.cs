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
    public class ContactUsController : Controller
    {
        private readonly TravistaContext _dBContext;
        private readonly UserManager<TravistaUser> _userManager;

        public ContactUsController(TravistaContext _dBContext, UserManager<TravistaUser> _userManager)
        {
            this._dBContext = _dBContext;
            this._userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            var contactus = await _dBContext.ContactUs.ToListAsync();
            return View(contactus);
        }

        public IActionResult AddContactUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddContactUs(ContactUs addUserRequest)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var contactus = new ContactUs()
            {
                ID_ContactUs = 0,
                Name = addUserRequest.Name,
                Subject = addUserRequest.Subject,
                Message = addUserRequest.Message,
                Email = addUserRequest.Email,
                ID_Users = currentUser.Id
            };

            await _dBContext.ContactUs.AddAsync(contactus);
            await _dBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewContactUs(int ID_ContactUs)
        {
            var contactus = await _dBContext.ContactUs.FirstOrDefaultAsync(x => x.ID_ContactUs == ID_ContactUs);

            if (contactus != null)
            {
                var viewModel = new ContactUs()
                {
                    ID_ContactUs = contactus.ID_ContactUs,
                    Name = contactus.Name,
                    Subject = contactus.Subject,
                    Message = contactus.Message,
                    Email = contactus.Email,
                    ID_Users = contactus.ID_Users
                };

                return View("ViewContactUs", viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewContactUsPost(ContactUs addUserRequest)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var contactus = await _dBContext.ContactUs.FindAsync(addUserRequest.ID_ContactUs);

            if (contactus != null)
            {
                contactus.ID_ContactUs = addUserRequest.ID_ContactUs;
                contactus.Name = addUserRequest.Name;
                contactus.Subject = addUserRequest.Subject;
                contactus.Message = addUserRequest.Message;
                contactus.Email = addUserRequest.Email;
                contactus.ID_Users = currentUser.Id;

                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteContactUs(ContactUs model)
        {
            var contactus = await _dBContext.ContactUs.FindAsync(model.ID_ContactUs);

            if (contactus != null)
            {
                _dBContext.ContactUs.Remove(contactus);
                await _dBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
