using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Technico.Models;
using Technico.Services;
using TechnicoWebApi.Dtos;
using TechnicoWebApi.Models;

namespace Technico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOwnerService _ownerService;

        public HomeController(ILogger<HomeController> logger , IOwnerService ownerService)
        {
            _logger = logger;
            _ownerService = ownerService;
        }

        public  IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Id,VAT,Name,Surname,Address,PhoneNumber,Email,Password,OwnerType")] OwnerRequestDto requestDto ) 
        {
            if (!ModelState.IsValid)
            {
                return View(requestDto);
            }

            var newRepair = await _ownerService.CreateOwner(requestDto);
            if (newRepair != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the repair.");
                return View(requestDto);
            }
            
        }

        public IActionResult Login()
        {
            return View();
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
