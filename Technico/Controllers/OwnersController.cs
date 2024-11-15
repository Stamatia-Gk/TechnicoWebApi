using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Technico.Data;
using Technico.Models;
using Technico.Services;
using TechnicoWebApi.Dtos;
using TechnicoWebApi.Models;

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

        // GET: PropertyItems/Create
        public IActionResult Create()
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
