﻿
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Technico.Models;
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

        // GET
        public IActionResult CreateRepair()
        {
            var allOwners = _ownerService.GetAllOwners();
            return View(new CreateRepairDto
            {
                ownerList = allOwners.Result,
            });
        }

        // POST: Repairs/Create
        [HttpPost]
        public async Task<IActionResult> CreateRepair(RepairDto repairDto, int ownerId)
        {
            if (ModelState.IsValid)
            {
                await _repairService.CreateRepair(repairDto, ownerId);
                return RedirectToAction(nameof(IndexRepairs));
            }

            var model = new CreateRepairDto
            {
                repairDto = repairDto,
                ownerList = (await _ownerService.GetAllOwners()).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SearchRepair(DateTime? startDate, DateTime? endDate ,int ownerId)
        {
            if (!startDate.HasValue || !endDate.HasValue || ownerId == 0)
            {
                ModelState.AddModelError(string.Empty, "Please provide valid search parameters.");
                return View();
            }

            var repairs = await _repairService.SearchRepairsByDateRange(startDate.Value, endDate.Value , ownerId);

            if (repairs == null)
            {
                ModelState.AddModelError(string.Empty, "No repairs found for the given criteria.");
                return View();
            }

            return View("IndexRepairs", repairs);
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

        // GET
        public IActionResult CreateProperty()
        {
            var allOwners = _ownerService.GetAllOwners();
            return View(new CreatePropertyDto
            {
                ownerList = allOwners.Result,
            });
        }

        // POST: PropertyItems/Create
        [HttpPost]
        public async Task<IActionResult> CreateProperty(PropertyDto propertyDto, int ownerId)
        {
            if (ModelState.IsValid)
            {
                // Pass both the property data and owner ID to your service
                await _propertyService.CreateProperty(propertyDto, ownerId);
                return RedirectToAction(nameof(IndexProperties));
            }
    
            // If we got this far, something failed, redisplay form
            var model = new CreatePropertyDto
            {
                propertyDto = propertyDto,
                ownerList = (await _ownerService.GetAllOwners()).ToList()
            };
            return View(model);
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

        [HttpPost]
        public async Task<IActionResult> SearchProperty(int propertyId, string vatNumber)
        {
            if (propertyId == 0 && string.IsNullOrEmpty(vatNumber))
            {
                ModelState.AddModelError(string.Empty, "Please provide at least one search parameter.");
                return View();
            }

            var properties = await _propertyService.SearchPropertiesByOwnerOrVatAsync(propertyId, vatNumber);

            if (properties == null || !properties.Any())
            {
                ModelState.AddModelError(string.Empty, "No properties found for the given criteria.");
                return View();
            }

            return View("IndexProperties", properties);
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

        [HttpPost]
        public async Task<IActionResult> SearchOwner(string vat, string email)
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


            return View("IndexOwners", owner);
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
