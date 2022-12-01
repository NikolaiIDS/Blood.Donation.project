using Microsoft.AspNetCore.Mvc;
using BBDS.Management.Data;
using BBDS.Management.Models;

namespace BBDS.Management.Controllers
{
    public class UserControlController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserControlController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<RegisterViewModel> objRegisterList = _db.RegisterViewModels;
            return View(objRegisterList);
        }
        //GET
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            await _db.RegisterViewModels.AddAsync(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //GET
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id==null||id==0)
            {
                return NotFound();
            }
            var personFromDb = _db.RegisterViewModels.Find(id);
            if (personFromDb==null)
            {
                return NotFound();
            }
            return View(personFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegisterViewModel obj)
        {
        
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

             _db.RegisterViewModels.Update(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
        }
    }
}
