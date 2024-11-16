
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Technico.Models;
using Technico.Services;
using Technico.Session;
using TechnicoWebApi.Dtos;

namespace Technico.Controllers
{
    public class PropertyItemsController : Controller
    {
        private readonly IPropertyService _propertyService;

        public PropertyItemsController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        // GET: PropertyItems
        public async Task<IActionResult> Index()
        {
            return View(await _propertyService.GetProperties());
        }

        public async Task<IActionResult> PropertiesOfAnOwner()
        {
            return View(await _propertyService.GetPropertiesByOwnerId(SessionClass.ownerId));
        }
        
        // GET: PropertyItems/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var propertyDto = await _propertyService.GetPropertyById(id);
            if (propertyDto == null)
            {
                return NotFound();
            }

            return View(propertyDto);
        }

        // GET: PropertyItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PropertyItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,IdentificationNumber,Address,ConstructionYear,PropertyType")]
            PropertyDto propertyDto)
        {
            var propertyCreated = _propertyService.CreateProperty(propertyDto, SessionClass.ownerId);
            return View(propertyDto);
        }

        // GET: PropertyItems/Edit/5
        public async Task<IActionResult> Edit(int id)
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
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
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the Property.");
                return View(propertyItem);
            }
        }
        
        public async Task<IActionResult> Delete(int id)
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
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repairToDelete = await _propertyService.DeleteProperty(id);
            if (repairToDelete != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SearchPropertyByIdOrVat(int ownerId, string vatNumber)
        {
            if (ownerId == 0 && string.IsNullOrEmpty(vatNumber))
            {
                ModelState.AddModelError(string.Empty, "Please provide at least one search parameter.");
                return View();
            }

            var properties = await _propertyService.SearchPropertiesByOwnerOrVatAsync(ownerId, vatNumber);

            if (properties == null || !properties.Any())
            {
                ModelState.AddModelError(string.Empty, "No properties found for the given criteria.");
                return View();
            }

            return View("IndexProperties",properties);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
