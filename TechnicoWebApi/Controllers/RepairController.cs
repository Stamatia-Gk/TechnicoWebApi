﻿// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.AspNetCore.Mvc;
using Technico.Services.Interfaces;
using TechnicoWebApi.Dtos;

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
        public async Task<ActionResult> Get()
        {
            var result = await _repairService.GetAllRepairs();
            if (result.IsFailure)
            {
                return NotFound(result.Error); 
            }

            return Ok(result.Value);
        }

        [HttpGet("ongoing")]

        public async Task<ActionResult> GetAllOnGoing()
        {
            var result = await _repairService.GetOngoingRepairs();
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        // GET api/<RepairController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _repairService.GetRepairById(id);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet, Route("ownerrepairs/{id}")]
        public async Task<ActionResult> GetRepairsByOwnerId(int id)
        {
            var result = await _repairService.GetAllRepairsOfAnOwner(id);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet, Route("searchrepairs")]
        public async Task<ActionResult> Search(DateTime startDate, DateTime endDate, int id)
        {
            var result = await _repairService.SearchRepair(startDate, endDate, id);
            if (result.IsFailure)
            {
                return NotFound(result.Error); 
            }

            return Ok(result.Value.ToList());
        }

        // POST api/<RepairController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RepairDto repairDTO , int ownerId)
        {
            var result = await _repairService.CreateRepair(repairDTO,ownerId);
            if (result.IsFailure) 
            {
                return BadRequest(result.Error); 
            }

            return Ok(result.Value);
        }

        // PUT api/<RepairController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] RepairDto repairDTO)
        {
            var result = await _repairService.UpdateRepair(id, repairDTO);
            if (result.IsFailure)
            {
                return BadRequest(result.Error); 
            }

            return Ok(result.Value);
        }

        // DELETE api/<RepairController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repairService.DeleteRepair(id);
            if (result.IsFailure)
            {
                return BadRequest(result.Error); 
            }

            return Ok();
        }
    }
}
