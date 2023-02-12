using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BBDS.Management.Data;
using BBDS.Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BBDS.Management.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestController(ApplicationDbContext _db, UserManager<ApplicationUser> _userManager)
        {
            this._db = _db;
            this._userManager = _userManager;
        }

        [HttpGet]
        public async Task<IActionResult> SendRequestAsync()
        {
            var model = new RequestViewModel();
            model.Cities = await _db.Cities.Select(c => new CityViewModel
            {
                Name = c.CityName,
                Id = c.Id
            }).ToListAsync();
            model.BloodTypes = await _db.BloodTypes.Select(c => new BloodTypeViewModel
            {
                Name = c.TypeName,
                Id = c.Id
            }).ToListAsync();
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> SendRequestAsyncPost(RequestViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }
            var request = _db.Set<Request>();

            model.BloodTypes = await _db.BloodTypes.Select(c => new BloodTypeViewModel
            {
                Name = c.TypeName,
                Id = c.Id
            }).ToListAsync();

            model.Cities = await _db.Cities.Select(c => new CityViewModel
            {
                Name = c.CityName,
                Id = c.Id
            }).ToListAsync();

            foreach (var item in model.BloodTypes)
            {
                if (item.Id == model.BloodId)
                {
                    model.BloodTypeName = item.Name;
                    break;
                }
            }

            foreach (var item in model.Cities)
            {
                if (item.Id == model.CityId)
                {
                    model.CityName = item.Name;
                    break;
                }
            }

            request.Add(new Request { BloodTypeId = model.BloodId,CityName = model.CityName, CityId = model.CityId, BloodTypeName = model.BloodTypeName, CountOfRequestedUsers = model.PeopleToView });;
            TempData["success"] = "Заявката бе успешно създадена!";
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Medic")] 

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var requestFromDb =  _db.Requests.Select(u => new RequestViewModel
            {
                BloodId = u.BloodTypeId,
                BloodTypeName = u.BloodTypeName,
                CityName = u.CityName,
                PeopleToView = u.CountOfRequestedUsers,
                Id = u.Id
            }).FirstOrDefault(u => u.Id == id);

            if (requestFromDb == null)
            {
                return NotFound();
            }
            return View(requestFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Medic")]

        public async Task<IActionResult> Delete(RequestViewModel obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            var request = _db.Requests.First(c => c.Id == obj.Id);
            if (request == null)
            {
                return NotFound();
            }

            _db.Remove(request);
            await _db.SaveChangesAsync();
            TempData["error"] = "Заявката бе изтрита успешно!";
            return RedirectToAction("Index","Home");
        }
    }
}
