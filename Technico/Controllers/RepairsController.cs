using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Technico.Data;
using Technico.Models;
using Technico.Services;
using TechnicoWebApi.Dtos;

namespace Technico.Controllers
{
    public class RepairsController : Controller
    {
        private readonly IRepairService _repairService;

        public RepairsController(IRepairService repairService)
        {
           _repairService = repairService;
        }

        // GET: Repairs
        public async Task<IActionResult> Index()
        {
            return View(await _repairService.GetRepairs());
        }

        //GET: Repairs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _repairService.GetRepairById(id));
        }

        // GET: Repairs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Repairs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ScheduledRepair,RepairType,Description,Address,RepairStatus,Cost")] RepairDto repair , int ownerId)
        {
            if (!ModelState.IsValid)
            {
                return View(repair);
            }

            var newRepair = await _repairService.CreateRepair(repair , ownerId);
            if(newRepair != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            { 
                 ModelState.AddModelError(string.Empty, "An error occurred while creating the repair.");
                 return View(repair);             
            }

        }

        //// GET: Repairs/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var repair = await _context.Repairs.FindAsync(id);
        //    if (repair == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(repair);
        //}

        //// POST: Repairs/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,ScheduledRepair,RepairType,Description,Address,RepairStatus,Cost")] Repair repair)
        //{
        //    if (id != repair.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(repair);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RepairExists(repair.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(repair);
        //}

        // GET: Repairs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var updatedRepairs = await _repairService.DeleteRepair(id);

            if (updatedRepairs!= null)
            {
                return View("Index", updatedRepairs);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: Repairs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var repair = await _context.Repairs.FindAsync(id);
        //    if (repair != null)
        //    {
        //        _context.Repairs.Remove(repair);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool RepairExists(int id)
        //{
        //    return _context.Repairs.Any(e => e.Id == id);
        //}
    }
}
