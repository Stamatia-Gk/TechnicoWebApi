// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.AspNetCore.Mvc;
using Technico.Services.Interfaces;
using TechnicoWebApi.Dtos;
using TechnicoWebApi.Services.Implementations;

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
        public async Task<ActionResult> GetOwners()
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
        public async Task<ActionResult> GetOwnerById(int id)
        {
            var result = await _ownerService.GetOwnerById(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet, Route("searchowner")]
        public async Task<ActionResult> Search(string? vat = null , string? email = null)
        {
            var result = await _ownerService.SearchOwner(vat, email);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OwnerRequestDto ownerRequestDto)
        {
            var result = await _ownerService.CreateOwner(ownerRequestDto);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] OwnerResponseDto OwnerResponseDto)
        {
            var result = await _ownerService.UpdateOwner(id, OwnerResponseDto);
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var result = await _ownerService.DeleteOwner(id);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        // POST api/<ValuesController>
        [HttpPost("{email},{password}")]
        public async Task<ActionResult> Login([FromRoute] string email, [FromRoute] string password)
        {
            var result = await _ownerService.Login(email, password);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
