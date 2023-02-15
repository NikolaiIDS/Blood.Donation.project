using Microsoft.AspNetCore.Mvc;
using BBDS.Management.Data;
using BBDS.Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BBDS.Management.Controllers
{
    public class UserControlController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserControlController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [Authorize]


        public async Task<IActionResult> Index()
        {
            List<UserEditingViewModel> users = new List<UserEditingViewModel>();
            users = GetUsers();

            return View(users);
        }

        public PartialViewResult SearchUsers(string searchText)
        {
            List<UserEditingViewModel> model = GetUsers();

            var result = model.Where(a => a.UserName.ToLower().Contains(searchText.ToLower()) ||
            a.Email.ToLower().Contains(searchText.ToLower()) ||
            a.PhoneNumber.ToLower().Contains(searchText.ToLower()) ||
            a.EGN.ToLower().Contains(searchText.ToLower()) ||
            a.FirstName.ToLower().Contains(searchText.ToLower()) ||
            a.LastName.ToLower().Contains(searchText.ToLower()) ||
            a.BloodTypeName.ToLower().Contains(searchText.ToLower()) ||
            a.CityName.ToLower().Contains(searchText.ToLower())).ToList();

            return PartialView("_userTable", result);
        }

        public List<UserEditingViewModel> GetUsers()
        {
            List<UserEditingViewModel> objRegisterList = _db.Users
                .Select(u => new UserEditingViewModel
                {
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Id = u.Id,
                    EGN = u.EGN,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    BloodTypeName = GetBloodTypeName(u.BloodTypeId),
                    CityName = GetCityName(u.CityId)
                }).ToList();

            return objRegisterList;
        }

        public static string GetBloodTypeName(int BloodId)
        {
            if (BloodId == 1)
                return "A +";
            if (BloodId == 2)
                return "A -";
            if (BloodId == 3)
                return "B +";
            if (BloodId == 4)
                return "B -";
            if (BloodId == 5)
                return "AB +";
            if (BloodId == 6)
                return "AB -";
            if (BloodId == 7)
                return "0 +";
            if (BloodId == 8)
                return "0 -";
            else
                return null;
        }

        public static string GetCityName(Guid CityId)
        {
            if (CityId == Guid.Parse("F40854CE-7D63-46A4-BB60-07DDE1A4705E"))
                return "Пловдив";
            if (CityId == Guid.Parse("72316B2D-31A5-4BA8-8609-13719F7CCE98"))
                return "Велиоко Търново";
            if (CityId == Guid.Parse("3A4DD904-D3B8-4ADB-B6A6-4E42267C9683"))
                return "София";
            if (CityId == Guid.Parse("D0E60734-F384-4CCB-B472-61E8E9629E48"))
                return "Враца";
            if (CityId == Guid.Parse("00182E26-FD34-436A-988A-69A8CE6CEB16"))
                return "Горна Оряховица";
            if (CityId == Guid.Parse("56393F6B-D016-4583-B536-726AD925ED8A"))
                return "Левски";
            if (CityId == Guid.Parse("7AAC1F3B-EF3E-461E-8AAB-857846F67FA4"))
                return "Дряново";
            if (CityId == Guid.Parse("65078407-8AE3-4872-A807-CE3484306A99"))
                return "Варна";
            else
                return null;
        }

        //GET
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //   var user = _db.Users.FirstOrDefault(u => u.Id == id);
            var personFromDb = _db.Users.Select(u => new UserEditingViewModel
            {
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Id = u.Id,
                EGN = u.EGN,
                FirstName = u.FirstName,
                LastName = u.LastName,
                CityId = u.CityId,
                BloodId = u.BloodTypeId
            }).FirstOrDefault(u => u.Id == id);
            personFromDb.Cities = await _db.Cities.Select(c => new CityViewModel
            {
                Name = c.CityName,
                Id = c.Id
            }).ToListAsync();
            if (personFromDb == null)
            {
                return NotFound();
            }
            TempData["warning"] = "Внимавай с данните на потребителите!";
            return View(personFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(UserEditingViewModel personFromDb)
        {
            if (personFromDb == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(personFromDb.Id);
            user.UserName = personFromDb.UserName;
            user.NormalizedUserName = personFromDb.UserName.ToUpper();
            user.PhoneNumber = personFromDb.PhoneNumber;
            user.Email = personFromDb.Email;
            user.NormalizedEmail = personFromDb.Email.ToUpper();
            user.FirstName = personFromDb.FirstName;
            user.LastName = personFromDb.LastName;
            user.CityId = personFromDb.CityId;
            user.EGN = personFromDb.EGN;
            user.BloodTypeId = personFromDb.BloodId;
            user.GenderId = personFromDb.GenderId;


            if (user == null)
            {
                return NotFound();
            }

            _db.Update(user);
            await _db.SaveChangesAsync();
            TempData["success"] = "Потребителският профил бе обновен успешно!";
            return RedirectToAction("Index");
        }

        //GET
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var personFromDb = _db.Users.Select(u => new UserDeleteViewModel
            {
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Id = u.Id
            }).FirstOrDefault(u => u.Id == id);

            if (personFromDb == null)
            {
                return NotFound();
            }
            return View(personFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePOST(UserDeleteViewModel obj)
        {
            var user = await _userManager.FindByIdAsync(obj.Id);
            if (user == null)
            {
                return NotFound();
            }
            if (obj == null)
            {
                return NotFound();
            }

            //if (!ModelState.IsValid)
            //{
            //    return View(obj);
            //}
            _db.Remove(user);
            await _db.SaveChangesAsync();
            TempData["error"] = "Потребителят бе изтрит успешно!";
            return RedirectToAction("Index");
        }
    }
}
