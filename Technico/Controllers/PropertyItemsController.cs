using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Technico.Data;
using Technico.Services;
using TechnicoWebApi.Dtos;
using TechnicoLibrary.Models;

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

        // GET: PropertyItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var propertyDto = await _propertyService.GetPropertyById(1);
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
            [Bind("Id,IdentificationNumber,Address,ConstructionYear,PropertyType")] PropertyDto propertyDto)
        {
            var propertyCreated = _propertyService.CreateProperty(propertyDto, 1);
            return View(propertyDto);
        }

        // GET: PropertyItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            return View();
        }

        // POST: PropertyItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,IdentificationNumber,Address,ConstructionYear,PropertyType")] PropertyItem propertyItem)
        {

            return View(propertyItem);
        }

        // GET: PropertyItems/Delete/5
       /* public async Task<IActionResult> Delete(int id)
        {   
            _propertyService.DeleteProperty(id);
            return View();
        }*/ //inconsistent 

        // POST: PropertyItems/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyItemExists(int id)
        {
            return true;
        }
    }*/
    }
}
