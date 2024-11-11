// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.AspNetCore.Mvc;
using Technico.DTO;
using Technico.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechnicoWebApi.Controllers
{
    [Route("api/[controller]")] // api//owners
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<List<OwnerDTO>>> GetOwners()
        {
            var result = await _ownerService.GetAllOwners();
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerDTO>> GetOwnerById(int id)
        {
            var result = await _ownerService.GetOwner(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        // GET api/<ValuesController>/5
        [HttpGet, Route("ownersproperties/{id}")] //Task<Result<List<OwnerWithPropertiesDTO>>>
        public async Task<ActionResult<List<OwnerWithPropertiesDTO>>> GetOwnerPropertiesById([FromRoute] int id)
        {
            var result = await _ownerService.GetOwnerProperties(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        // GET api/<ValuesController>/5
        [HttpGet, Route("ownersrepairs/{id}")]
        public async Task<ActionResult<List<OwnerWithRepairsDTO>>> GetOwnerRepairsById([FromRoute] int id)
        {
            var result = await _ownerService.GetOwnerRepairs(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<OwnerDTO>> Post([FromBody] OwnerDTOCreate ownerDtoCreate)
        {
            var result = await _ownerService.CreateOwner(ownerDtoCreate);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OwnerDTO>> Put(int id, [FromBody] OwnerDTO ownerDto)
        {
            var result = await _ownerService.UpdateOwner(id, ownerDto);
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            var result = await _ownerService.DeleteOwner(id);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
