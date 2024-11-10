﻿// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.AspNetCore.Mvc;
using Technico.DTO;
using Technico.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechnicoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private IRepairService _repairService;
        public RepairController(IRepairService repairService)
        {
            _repairService = repairService;
        }
        // GET: api/<RepairController>
        [HttpGet]
        public async Task<ActionResult<List<RepairDTOEmployee>>> Get()
        {
            var result = await _repairService.GetAllRepairs();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else 
            {
                return NotFound(result.Error);
            }
        }

        [HttpGet, Route("Search")]
        public async Task<ActionResult<List<RepairDTOEmployee>>> Search(DateTime startDate, DateTime endDate, int id)
        {
            var result = await _repairService.SearchRepair(startDate, endDate, id);
            if (result.IsSuccess)
            {
                return Ok(result.Value.ToList());
            }
            else
            {
                return NotFound(result.Error);
            }
        }

        // GET api/<RepairController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RepairDTOEmployee>> Get(int id)
        {
            var result = await _repairService.GetRepair(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            } 
            else 
            {
               return BadRequest(result.Error);
            }
        }

        // POST api/<RepairController>
        [HttpPost]
        public async Task<ActionResult<RepairDTOEmployee>> Post([FromBody] RepairDTOEmployee repairDTO , int ownerId)
        {
            var result = await _repairService.CreateRepair(repairDTO,ownerId);
            if (result.IsSuccess) 
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        // PUT api/<RepairController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<RepairDTOEmployee>> Put(int id, [FromBody] RepairDTOEmployee repairDTO)
        {
            var result = await _repairService.UpdateRepair(id, repairDTO);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else 
            {
                return BadRequest(result.Error) ;
            }
        }

        // DELETE api/<RepairController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RepairDTOEmployee>> Delete(int id)
        {
            var result = await _repairService.DeleteRepair(id);
            if (result.IsSuccess)
            {
                return Ok();
            }
            else 
            {
                return BadRequest(result.Error) ;
            }
        }
    }
}
