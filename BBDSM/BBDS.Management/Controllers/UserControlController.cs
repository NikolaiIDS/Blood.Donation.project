using Microsoft.AspNetCore.Mvc;
using BBDS.Management.Data;
using BBDS.Management.Models;
using Microsoft.AspNetCore.Identity;

namespace BBDS.Management.Controllers
{
    public class UserControlController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public UserControlController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<UserEditingViewModel> objRegisterList = _db.Users.Select(u => new UserEditingViewModel
            {
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Id = u.Id
            });
            return View(objRegisterList);
        }


        //GET
        [HttpGet]
        public IActionResult Edit(string id)
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
                Id = u.Id
            }).FirstOrDefault(u => u.Id == id);
            if (personFromDb == null)
            {
                return NotFound();
            }
            return View(personFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditingViewModel personFromDb)
        {

            if (personFromDb == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(personFromDb);
            }
             _db.Update(personFromDb);
            return RedirectToAction("Index");
        }

        //GET
        [HttpGet]
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
            }).FirstOrDefault(u => u.Id == u.Id);
            if (personFromDb == null)
            {
                return NotFound();
            }
            return View(personFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(UserDeleteViewModel obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            _db.Remove(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
