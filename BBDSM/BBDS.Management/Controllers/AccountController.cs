using System.Security.Claims;

using BBDS.Management.Data;
using BBDS.Management.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBDS.Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager, ApplicationDbContext _Db)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            _db = _Db;
        }

        [HttpGet]
        public async Task<IActionResult> Inspect()
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var personFromDb = await _db.Users.Select(u => new UserEditingViewModel
            {
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                FirstName = u.FirstName,
                LastName = u.LastName,
                EGN = u.EGN,
                BloodId = u.BloodTypeId,
                Id = u.Id,
                CityId = u.CityId,
                GenderId = u.GenderId
            }).FirstOrDefaultAsync(u => u.Id == userId);

            if (personFromDb == null)
            {
                return NotFound();
            }

            personFromDb.Cities = await _db.Cities.Select(c => new CityViewModel
            {
                Id = c.Id,
                Name = c.CityName
            }).ToListAsync();

            return View(personFromDb);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var personFromDb = _db.Users.Select(u => new UserEditingViewModel
            {
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                FirstName = u.FirstName,
                LastName = u.LastName,
                EGN = u.EGN,
                BloodId = u.BloodTypeId,
                CityId = u.CityId,
                Id = u.Id,
                GenderId = u.GenderId
            }).FirstOrDefault(u => u.Id == userId);

            if (personFromDb == null)
            {
                return NotFound();
            }

            personFromDb.Cities = await _db.Cities.Select(c => new CityViewModel
            {
                Name = c.CityName,
                Id = c.Id
            }).ToListAsync();
            TempData["warning"] = "Провери преди да обновиш!";
            return View(personFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(UserEditingViewModel personFromDb)
        {

            if (personFromDb == null)
            {
                return NotFound();
            }

            personFromDb.Cities = await _db.Cities.Select(c => new CityViewModel
            {
                Name = c.CityName,
                Id = c.Id
            }).ToListAsync();

            var user = await userManager.FindByEmailAsync(personFromDb.Email);
            user.UserName = personFromDb.UserName;
            user.NormalizedUserName = personFromDb.UserName.ToUpper();
            user.PhoneNumber = personFromDb.PhoneNumber;
            user.Email = personFromDb.Email;
            user.NormalizedEmail = personFromDb.Email.ToUpper();
            user.FirstName = personFromDb.FirstName;
            user.LastName = personFromDb.LastName;
            user.CityId = personFromDb.CityId;

            if (user == null)
            {
                return NotFound();
            }

            _db.Update(user);
            TempData["success"] = "Акаунтът бе успешно подновен!";
            await _db.SaveChangesAsync();
            return RedirectToAction("Edit");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                EGN = model.EGN,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BloodTypeId = model.BloodId,
                CityId = model.CityId,
                GenderId = model.GenderId
            };


            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            TempData["success"] = "Регистрирахте се успешно!";
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                TempData["success"] = "Влязохте успешно!";
                if (result.Succeeded)
                {

                    return RedirectToAction(nameof(HomeController.Index), "Home");

                }
            }

            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["success"] = "Излязохте успешно!";

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }

}
