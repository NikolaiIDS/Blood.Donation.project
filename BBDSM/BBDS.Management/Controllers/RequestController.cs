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
        public IActionResult Index()
        {
            return View();
        }

       // [HttpGet]
       // public Task<IActionResult>  SendRequest()
       // {
       //     var requestFromDb = _db.Requests
       // }
    }
}
