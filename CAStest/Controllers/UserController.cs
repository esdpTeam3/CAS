using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAStest.Data;
using CAStest.Models;
using CAStest.Models.ViewModels;
using CAStest.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CAStest.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public UserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<UserController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }
        public IActionResult UserCards(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var users = from c in _context.Users.Where(u => u.Fullname != "Admin")
                        select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = _context.Users.Where(s => s.Fullname.Contains(searchString) || s.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderBy(s => s.Fullname);
                    break;

            }
            //IEnumerable<ApplicationUser> users = _context.Users.ToList();
            return PartialView("CardsUser", users);
        }

        public async Task<IActionResult> Create()
        {
            return RedirectToAction("Register", "Account");
        }

        public async Task<IActionResult> EditUser()
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                ApplicationUserViewModel model = new ApplicationUserViewModel
                {
                    Fullname = user.Fullname,
                    Login = user.UserName,
                    Email = user.Email,
                    DatePassword = user.DatePassword
                };
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(ApplicationUserViewModel model)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                user.Fullname = model.Fullname;
                user.UserName = model.Login;
                user.Email = model.Email;
                user.DatePassword = model.DatePassword;

                // Сюда нужно будет добавить функционал по смене пароля (Настя)

                //_userManager.RemovePasswordAsync();
                //_userManager.ChangePasswordAsync()
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }

        public async Task<ActionResult> Index()
        {
            ApplicationUser user = await UserSearch();

           
            if (user != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<ActionResult> Details(string id)
        {

            ApplicationUser user = await _context.Users.FindAsync(id);
            ApplicationUserViewModel model = new ApplicationUserViewModel();
            if (user != null)
            {

                model.Fullname = user.Fullname;
                model.Login = user.UserName;
                model.Email = user.Email;
                model.DatePassword = user.DatePassword;
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        private async Task<ApplicationUser> UserSearch()
        {
            string name = User.Identity.Name;
            ApplicationUser user = new ApplicationUser();
            user = null;
            if (name != null)
            {
                user = await _userManager.FindByNameAsync(name);

            }
            return user;
        }

        public IActionResult UserList(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var users = from c in _context.Users.Where(u => u.Fullname != "Admin")
                        select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = _context.Users.Where(s => s.Fullname.Contains(searchString)|| s.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderBy(s => s.Fullname);
                    break;

            }
            //IEnumerable<ApplicationUser> users = _context.Users.ToList();
            return PartialView("ListUsers", users);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckName(string login)
        {
            ApplicationUser user = _context.Users.FirstOrDefault(u => u.UserName == login);
            return Json(user == null);
        }

        public virtual async Task<IdentityResult> LockUserAccount(ApplicationUser user)
        {
            IdentityResult result = await _userManager.SetLockoutEnabledAsync(user, true);
            if (result.Succeeded)
            {
                result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
                user.IsBlocked = true;
                _context.SaveChanges();
            }
            return result;
        }

        public virtual async Task<IdentityResult> UnlockUserAccount(ApplicationUser user)
        {
            IdentityResult result = await _userManager.SetLockoutEnabledAsync(user, false);
            if (result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                user.IsBlocked = false;
                _context.SaveChanges();
            }
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Block(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            await LockUserAccount(user);
            return RedirectToAction("UserList");
        }

        [HttpPost]
        public async Task<IActionResult> UnBlock(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            await UnlockUserAccount(user);
            return RedirectToAction("UserList");
        }
    }
}