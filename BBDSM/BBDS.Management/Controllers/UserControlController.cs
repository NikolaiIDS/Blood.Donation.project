using Microsoft.AspNetCore.Mvc;
using BBDS.Management.Data;
using BBDS.Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


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
            IEnumerable<UserEditingViewModel> objRegisterList =  _db.Users.Select(u => new UserEditingViewModel
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(UserEditingViewModel personFromDb)
        {

            if (personFromDb == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(personFromDb.Id);
            user.UserName = personFromDb.UserName;
            

            if (user == null)
            {
                return NotFound();
            }

            _db.Update(user);
            await _db.SaveChangesAsync();
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
        [Authorize (Roles = "Admin")]
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
            return RedirectToAction("Index");
        }
    }
}
