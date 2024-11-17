
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Technico.Models;
using Technico.Services;
using Technico.Session;
using TechnicoWebApi.Dtos;
using TechnicoWebApi.Models;

namespace Technico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOwnerService _ownerService;

        public HomeController(ILogger<HomeController> logger, IOwnerService ownerService)
        {
            _logger = logger;
            _ownerService = ownerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Id,VAT,Name,Surname,Address,PhoneNumber,Email,Password,OwnerType")] OwnerRequestDto ownerRequestDto) 
        {
            if (!ModelState.IsValid)
            {
                return View(ownerRequestDto);
            }

            var newOwner = await _ownerService.CreateOwner(ownerRequestDto);
            if (newOwner != null)
            {
                SessionClass.ownerId = newOwner.Id;
                SessionClass.ownerType = newOwner.OwnerType;
                return RedirectToAction("Index", "User");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the repair.");
                return View(ownerRequestDto);
            }
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Id,VAT,Name,Surname,Address,PhoneNumber,Email,Password,OwnerType")] OwnerRequestDto ownerRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return View(ownerRequestDto);
            }

            var newOwner = await _ownerService.CreateOwner(ownerRequestDto);
            if (newOwner != null)
            {
                return RedirectToAction("Login", "Home", new { email = ownerRequestDto.Email, password = ownerRequestDto.Password });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the repair.");
                return View(ownerRequestDto);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        /*[HttpPost, Route("Home/Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(OwnerCredentialsDto ownerCredentials)
        {
            var loggedInOwner = await _ownerService.Login(ownerCredentials.Email, ownerCredentials.Password);
           
            if (loggedInOwner.OwnerType == 0)
            {
                SessionClass.ownerId = loggedInOwner.Id;
                SessionClass.ownerType = loggedInOwner.OwnerType;
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Index","Admin");
            }
        }*/

        [HttpPost, Route("Home/Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var loggedInOwner = await _ownerService.Login(email, password);

            if (loggedInOwner == null)
            {
                ModelState.AddModelError(string.Empty, "No such account exists.");
                return View();
            }

            SessionClass.ownerId = loggedInOwner.Id;
            SessionClass.ownerType = loggedInOwner.OwnerType;
            if (loggedInOwner.OwnerType == OwnerType.Owner)
            {
                
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
