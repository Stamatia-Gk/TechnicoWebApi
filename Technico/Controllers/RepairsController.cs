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
using TechnicoLibrary.Models;

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

        // GET: Repairs/Edit/5
        public async Task<IActionResult> Edit(int id)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ScheduledRepair,RepairType,Description,Address,RepairStatus,Cost")] RepairDto repairdto)
        {
            if (!ModelState.IsValid)
            {
                return View(repairdto);
            }

            var updateRepair = await _repairService.UpdateRepair(repairdto, id);
            if (updateRepair != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the repair.");
                return View(repairdto);
            }

        }
    

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
