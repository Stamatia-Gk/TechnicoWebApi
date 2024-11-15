
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Technico.Models;
using Technico.Services;
using TechnicoWebApi.Dtos;

namespace Technico.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET: Owners
        public async Task<IActionResult> Index()
        {
            return View(await _ownerService.GetAllOwners());
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ownerDto = await _ownerService.GetOwnerById(id);
            if (ownerDto == null)
            {
                return NotFound();
            }

            return View(ownerDto);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VAT,Name,Surname,Address,PhoneNumber,Email,Password,OwnerType")] OwnerRequestDto ownerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(ownerDto);
            }

            var newOwner = await _ownerService.CreateOwner(ownerDto);
            if (newOwner != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the owner.");
                return View(ownerDto);
            }
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerToEdit = await _ownerService.GetOwnerById(id);
            if (ownerToEdit == null)
            {
                return NotFound();
            }
            return View(ownerToEdit);
        }

        // POST: Owners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,VAT,Name,Surname,Address,PhoneNumber,Email,Password,OwnerType")] OwnerResponseDto ownerDto, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(ownerDto);
            }

            var updateOwner = await _ownerService.UpdateOwner(id, ownerDto);
            if (updateOwner != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the owner.");
                return View(ownerDto);
            }
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var owner = await _ownerService.GetOwnerById(id);

            if (owner == null)
            {
                return NotFound();
            }
            else
            {
                return View(owner);
            }
        }

        // POST: Repairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ownerToDelete = await _ownerService.DeleteOwner(id);
            if (ownerToDelete != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SearchPropertyByIdOrVat(string vat, string email)
        {
            if (string.IsNullOrEmpty(vat) && string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError(string.Empty, "Please provide at least one search parameter.");
                return View();
            }

            var owner = await _ownerService.SearchOwner(vat, email);

            if (owner == null)
            {
                ModelState.AddModelError(string.Empty, "No properties found for the given criteria.");
                return View();
            }

            return View("Index", owner);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
