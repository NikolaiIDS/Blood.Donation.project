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
        [Authorize(Roles = "Admin,Medic")]
        public async Task<IActionResult> Index()
        {

            CombinedViewModel forView = new CombinedViewModel();
            forView.Cities = await _db.Cities.Select(c => new CityViewModel
            {
                Name = c.CityName,
                Id = c.Id
            }).ToListAsync();
            forView.Requests = await _db.Requests.Select(u => new RequestViewModel
            {
                Id = u.Id,
                CityId = u.CityId,
                CityName = u.CityName,
                BloodId = u.BloodTypeId,
                BloodTypeName = u.BloodTypeName,
                PeopleToView = u.CountOfRequestedUsers
            }).ToListAsync();


            return View(forView);
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

            // model.BloodTypes = await _db.BloodTypes.Select(c => new BloodTypeViewModel
            // {
            //     Name = c.TypeName,
            //     Id = c.Id
            // }).ToListAsync();

            model.Cities = await _db.Cities.Select(c => new CityViewModel
            {
                Name = c.CityName,
                Id = c.Id
            }).ToListAsync();

            // foreach (var item in model.BloodTypes)
            // {
            switch (model.BloodId)
            {
                case 1:
                    model.BloodTypeName = "7,1,8,2";
                    break;
                case 2:
                    model.BloodTypeName = "8,2";
                    break;
                case 3:
                    model.BloodTypeName = "7,3,8,4";
                    break;
                case 4:
                    model.BloodTypeName = "8,4";
                    break;
                case 5:
                    model.BloodTypeName = "7,1,3,5,8,2,4,6";
                    break;
                case 6:
                    model.BloodTypeName = "8,2,4,6";
                    break;
                case 7:
                    model.BloodTypeName = "7,8";
                    break;
                case 8:
                    model.BloodTypeName = "8";
                    break;
            }

            foreach (var item in model.Cities)
            {
                if (item.Id == model.CityId)
                {
                    model.CityName = item.Name;
                    break;
                }
            }

            request.Add(new Request { BloodTypeId = model.BloodId, CityName = model.CityName, CityId = model.CityId, BloodTypeName = model.BloodTypeName, CountOfRequestedUsers = model.PeopleToView });
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
            var requestFromDb = _db.Requests.Select(u => new RequestViewModel
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
            List<UsersAcceptedRequests> usersAccepted = _db.UsersAcceptedRequests
                .Where(u => u.RequestId == obj.Id)
                .Select(s => new UsersAcceptedRequests
                {
                    RequestId = s.RequestId,
                    UserId = s.UserId
                }).ToList();

            foreach (var item in usersAccepted)
            {
                _db.Remove(item);
            }

            _db.Remove(request);
            await _db.SaveChangesAsync();
            TempData["error"] = "Заявката бе изтрита успешно!";
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> UserAcceptRequest(Guid Id)
        {
            string getUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var user = _db.Set<UsersAcceptedRequests>(); //create variable
            user.Add(new UsersAcceptedRequests { RequestId = Id, UserId = getUserId }); //map avariable properties
            if (user == null)
            {
                return NotFound();
            }

            var acceptedRequestDb = _db.Requests
                .Where(b => b.Id == Id)
                .Select(u => new Request
                {
                    Id = u.Id,
                    CityId = u.CityId,
                    CityName = u.CityName,
                    BloodTypeId = u.BloodTypeId,
                    BloodTypeName = u.BloodTypeName,
                    CountOfRequestedUsers = u.CountOfRequestedUsers
                }).First();

            if (acceptedRequestDb.CountOfRequestedUsers == 0)
            {
                return NotFound();
            }
            else acceptedRequestDb.CountOfRequestedUsers--;



            _db.Update(acceptedRequestDb);
            TempData["success"] = "Заявката бе успешно приета!";
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
