using Microsoft.AspNetCore.Mvc;
using Technico.Services;
using Technico.Session;
using TechnicoWebApi.Dtos;

namespace Technico.Controllers
{
    public class UserController : Controller
    {
        private readonly IOwnerService _ownerService;
        private readonly IPropertyService _propertyService;
        private readonly IRepairService _repairService;

        public UserController(IRepairService repairService, IPropertyService propertyService, IOwnerService ownerService)
        {
            _ownerService = ownerService;
            _propertyService = propertyService;
            _repairService = repairService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var ownerId = SessionClass.ownerId;
            if (ownerId == null)
            {
                return NotFound();
            }

            return View(await _repairService.OwnerRepairs(ownerId));
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ownerId = SessionClass.ownerId;
            var ownerDto = await _ownerService.GetOwnerById(ownerId);
            if (ownerDto == null)
            {
                return NotFound();
            }

            return View(ownerDto);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> EditOwner(int id)
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
        public async Task<IActionResult> EditOwner([Bind("Id,VAT,Name,Surname,Address,PhoneNumber,Email,Password,OwnerType")] OwnerResponseDto ownerDto, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(ownerDto);
            }

            var updateOwner = await _ownerService.UpdateOwner(id, ownerDto);
            if (updateOwner != null)
            {
                return RedirectToAction(nameof(Details));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the owner.");
                return View(ownerDto);
            }
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> DeleteOwner(int id)
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
        [HttpPost, ActionName("DeleteOwner")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedOwner(int id)
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
    }
}
