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

        public UserControlController(ApplicationDbContext db, UserManager <IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserEditingViewModel> objRegisterList = _db.Users.Select(u => new UserEditingViewModel
            {
                UserName = u.UserName,
                Id = u.Id,
            }); 
            return View(objRegisterList);
        }
     

        //GET
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var personFromDb = _db.Users.FirstOrDefault(u => u.Id == id);
            if (personFromDb==null)
            {
                return NotFound();
            }
            return View(personFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditingViewModel obj)
        {
        
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            _userManager.Users.Where(u => u.Id == obj.Id).FirstOrDefault();
            var user = new IdentityUser 
            {
            UserName=obj.UserName,
            Email=obj.Email,
            PhoneNumber=obj.PhoneNumber,
            Id=obj.Id
            };
             
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        /*
        //GET
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var personFromDb = _db.RegisterViewModels.Find(id);
            if (personFromDb == null)
            {
                return NotFound();
            }
            return View(personFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var obj = _db.RegisterViewModels.Find(id);
            if (obj== null)
            {
                return NotFound();
            }
           
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            _db.RegisterViewModels.Remove(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }*/
    }
}
