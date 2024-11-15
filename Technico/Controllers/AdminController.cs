using Microsoft.AspNetCore.Mvc;
using Technico.Services;
using Technico.Session;
using TechnicoWebApi.Dtos;

namespace Technico.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOwnerService _ownerService;
        private readonly IPropertyService _propertyService;
        private readonly IRepairService _repairService;
        
        public AdminController(IRepairService repairService , IPropertyService propertyService, IOwnerService ownerService)
        {
            _ownerService = ownerService;
            _propertyService = propertyService;
            _repairService = repairService;
        }
        public async Task<IActionResult> Index()
        {
            var ongoingRepairs = await _repairService.GetOngoingRepairs();

            return View(ongoingRepairs);
        }

        public async Task<IActionResult> IndexRepairs()
        {
            return View(await _repairService.GetRepairs());
        }

        public IActionResult CreateRepair()
        {
            return View();
        }

        // POST: Repairs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRepair([Bind("Id,ScheduledRepair,RepairType,Description,Address,RepairStatus,Cost")] RepairDto repair, int ownerId)
        {
            if (!ModelState.IsValid)
            {
                return View(repair);
            }
            var id = SessionClass.ownerId;
            var newRepair = await _repairService.CreateRepair(repair, id);
            if (newRepair != null)
            {
                return RedirectToAction(nameof(IndexRepairs));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the repair.");
                return View(repair);
            }

        }

        // GET: Repairs/Edit/5
        public async Task<IActionResult> EditRepair(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var repairToEdit = await _repairService.GetRepairById(id);
            if (repairToEdit == null)
            {
                return NotFound();
            }
            return View(repairToEdit);
        }

        // POST: Repairs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRepair(int id, [Bind("Id,ScheduledRepair,RepairType,Description,Address,RepairStatus,Cost")] RepairDto repairdto)
        {
            if (!ModelState.IsValid)
            {
                return View(repairdto);
            }

            var updateRepair = await _repairService.UpdateRepair(repairdto, id);
            if (updateRepair != null)
            {
                return RedirectToAction(nameof(IndexRepairs));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the repair.");
                return View(repairdto);
            }

        }
        // GET: Repairs/Delete/5
        public async Task<IActionResult> DeleteRepair(int id)
        {
            var repair = await _repairService.GetRepairById(id);

            if (repair == null)
            {
                return NotFound();
            }
            else
            {
                return View(repair);
            }
        }

        // POST: Repairs/Delete/5
        [HttpPost, ActionName("DeleteRepair")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedRepair(int id)
        {
            var repairToDelete = await _repairService.DeleteRepair(id);
            if (repairToDelete != null)
            {
                return RedirectToAction(nameof(IndexRepairs));
            }
            else
            {
                return View("Error");
            }

        }

        public async Task<IActionResult> IndexProperties()
        {
            return View(await _propertyService.GetProperties());
        }

        public IActionResult CreateProperty()
        {
            return View();
        }

        // POST: PropertyItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProperty(
            [Bind("Id,IdentificationNumber,Address,ConstructionYear,PropertyType")]
            PropertyDto propertyDto)
        {
            var propertyCreated = _propertyService.CreateProperty(propertyDto, 1);
            return View(propertyDto);
        }

        // GET: PropertyItems/Edit/5
        public async Task<IActionResult> EditProperty(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var repairToEdit = await _propertyService.GetPropertyById(id);
            if (repairToEdit == null)
            {
                return NotFound();
            }

            return View(repairToEdit);
        }

        // POST: PropertyItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProperty(int id,
            [Bind("Id,IdentificationNumber,Address,ConstructionYear,PropertyType")]
            PropertyDto propertyItem)
        {

            if (!ModelState.IsValid)
            {
                return View(propertyItem);
            }

            var updateRepair = await _propertyService.UpdateProperty(propertyItem, id);
            if (updateRepair != null)
            {
                return RedirectToAction(nameof(IndexProperties));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the Property.");
                return View(propertyItem);
            }
        }

        public async Task<IActionResult> DeleteProperty(int id)
        {
            var property = await _propertyService.GetPropertyById(id);

            if (property == null)
            {
                return NotFound();
            }
            else
            {
                return View(property);
            }
        }

        [HttpPost, ActionName("DeleteProperty")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedProperty(int id)
        {
            var repairToDelete = await _propertyService.DeleteProperty(id);
            if (repairToDelete != null)
            {
                return RedirectToAction(nameof(IndexProperties));
            }
            else
            {
                return View("Error");
            }

        }

        public async Task<IActionResult> IndexOwners()
        {
            return View(await _ownerService.GetAllOwners());
        }

        // GET: Owners/Create
        public IActionResult CreateOwner()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOwner([Bind("Id,VAT,Name,Surname,Address,PhoneNumber,Email,Password,OwnerType")] OwnerRequestDto ownerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(ownerDto);
            }

            var newOwner = await _ownerService.CreateOwner(ownerDto);
            if (newOwner != null)
            {
                return RedirectToAction(nameof(IndexOwners));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the owner.");
                return View(ownerDto);
            }
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
                return RedirectToAction(nameof(IndexOwners));
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
                return RedirectToAction(nameof(IndexOwners));
            }
            else
            {
                return View("Error");
            }
        }
    }
}
