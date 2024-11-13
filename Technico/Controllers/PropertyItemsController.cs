using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Technico.Data;
using TechnicoWebApi.Models;

namespace Technico.Controllers
{
    public class PropertyItemsController : Controller
    {
        private readonly TechnicoDbContext _context;

        public PropertyItemsController(TechnicoDbContext context)
        {
            _context = context;
        }

        // GET: PropertyItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.Properties.ToListAsync());
        }

        // GET: PropertyItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyItem = await _context.Properties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyItem == null)
            {
                return NotFound();
            }

            return View(propertyItem);
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
        public async Task<IActionResult> Create([Bind("Id,IdentificationNumber,Address,ConstructionYear,PropertyType")] PropertyItem propertyItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyItem);
        }

        // GET: PropertyItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyItem = await _context.Properties.FindAsync(id);
            if (propertyItem == null)
            {
                return NotFound();
            }
            return View(propertyItem);
        }

        // POST: PropertyItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdentificationNumber,Address,ConstructionYear,PropertyType")] PropertyItem propertyItem)
        {
            if (id != propertyItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyItemExists(propertyItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(propertyItem);
        }

        // GET: PropertyItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyItem = await _context.Properties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyItem == null)
            {
                return NotFound();
            }

            return View(propertyItem);
        }

        // POST: PropertyItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyItem = await _context.Properties.FindAsync(id);
            if (propertyItem != null)
            {
                _context.Properties.Remove(propertyItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyItemExists(int id)
        {
            return _context.Properties.Any(e => e.Id == id);
        }
    }
}
