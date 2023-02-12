using BBDS.Management.Data;
using BBDS.Management.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Security.Claims;

namespace BBDS.Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User != null && User.Identity != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
                var user = await _userManager.FindByIdAsync(userId);
                IEnumerable<RequestViewModel> objRegisterList = _db.Requests
                   .Where(w => w.BloodTypeId == user.BloodTypeId)
                   .Where(c => c.CityId == user.CityId)
                   .Select(u => new RequestViewModel
                   {
                       BloodTypeName = u.BloodTypeName,
                       CityId = u.CityId,
                       BloodId = u.BloodTypeId,
                       PeopleToView = u.CountOfRequestedUsers,
                       Id = u.Id
                   }); // selects requests with same BloodType and City as the user's

                return View(objRegisterList);
            }
            else return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            TempData["error"] = "OOPSIE WOOPSIE WE MADE A FUCKIE WUCKIE UwU";
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}