// Team Project | European Dynamics | Code.Hub Project 2024
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
        public async Task<ActionResult<List<RepairDTO>>> Get()
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

        // GET api/<RepairController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RepairDTO>> Get(int id)
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
        public async Task<ActionResult<RepairDTO>> Post([FromBody] RepairDTO repairDTO , int ownerId)
        {
            var result = await _repairService.CreateRepair(repairDTO,ownerId);
            if (result.IsSuccess) 
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        // PUT api/<RepairController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<RepairDTO>> Put(int id, [FromBody] RepairDTO repairDTO)
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
        public async Task<ActionResult<RepairDTO>> Delete(int id)
        {
            var result = await _repairService.DeleteRepair(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else 
            {
                return BadRequest(result.Error) ;
            }
        }
    }
}
