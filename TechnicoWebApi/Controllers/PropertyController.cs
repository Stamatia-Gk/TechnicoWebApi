// Team Project | European Dynamics | Code.Hub Project 2024
using Microsoft.AspNetCore.Mvc;
using Technico.DTO;
using Technico.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechnicoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult>Get()
        {
            var result = await _propertyService.GetAllProperties();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("{ownerId}/Properties")]
        public async Task<ActionResult> GetPropertiesByOwnerId(int ownerId)
        {
            var result = await _propertyService.GetAllPropertiesOfAnOwner(ownerId);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _propertyService.GetPropertyById(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PropertyDTO propertyDto,[FromQuery] int ownerId)
        {
            var result = await _propertyService.CreateProperty(propertyDto, ownerId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromQuery] int oldPropertyId, [FromBody] PropertyDTO propertyDto)
        {
            var result = await _propertyService.UpdateProperty(oldPropertyId, propertyDto);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _propertyService.DeleteProperty(id);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
