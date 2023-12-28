using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaApi.Data;
using VillaApi.Models;
using VillaApi.Models.Dto;

namespace VillaApi.Controllers
{
    [ApiController]//defines it is a controller
    [Route("api/VillaApi")]
    // [Route("api/[controller]")] --> can use the controller instead of explicity defining the controller name
    public class VillaApiController : ControllerBase //controllerBase doesnot have support for views
    {
        private readonly ILogger<VillaApiController> _logger;

        public VillaApiController(ILogger<VillaApiController> logger)
        {
            _logger = logger;
        }
        // Get list of villas

        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Getting all Villas"); //logging
            return Ok(VillaStore.villaList);
        }

        //Get Villa By Id

        [HttpGet("id", Name = "GetVilla")]
        //[HttpGet("{id:int}")] if id is not written here it will throw an error that the request matched multiple points
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // [ProducesResponseType(200)]
        // [ProducesResponseType(400)]      can also be written like this so the response type will not be undocumented
        // [ProducesResponseType(404)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                // logging
                _logger.LogInformation("Get Villa Error with Id "+id );
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            if (villa is null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        // Get Villa By Name

        [HttpGet("name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(string name)
        {
            if (name is null)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(item => item.Name == name);

            if (villa is null)
            {
                return NotFound();
            }
            return Ok(villa);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto VillaDto)
        {
            if (VillaDto == null)
            {
                return BadRequest();
            }
            if (VillaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (VillaStore.villaList.FirstOrDefault(item => item.Name.ToLower() == VillaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", $"{VillaDto.Name} Already Exists");
                return BadRequest(ModelState);
            }
            VillaDto.Id = VillaStore.villaList.OrderByDescending(item => item.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(VillaDto);
            return CreatedAtRoute("GetVilla", new { id = VillaDto.Id }, VillaDto);
        }

        [HttpDelete("id", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]


        public ActionResult<VillaDto> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(item => item.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("id", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<VillaDto> UpdateVilla(int id, [FromBody] VillaDto VillaDto)
        {
            if (VillaDto == null || id != VillaDto.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(item => item.Id == id);
            villa.Name = VillaDto.Name;
            villa.Sqft = VillaDto.Sqft;
            villa.Occupancy = VillaDto.Occupancy;
            
            return NoContent();

        }

        // https://www.youtube.com/watch?v=_uZYOgzYheU&t=4723s (1:18-1:24)
        // refer above video to download required nuget packages
        // dotnet add package Microsoft.AspNetCore.JsonPatch --version 7.0.0
        // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 7.0.0-preview.5.22303.8

        [HttpPatch("id",Name ="Update Partial Villa")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<VillaDto> UpdatePartialVilla(int id,JsonPatchDocument<VillaDto> pathDto)
        {
            if(pathDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(item => item.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            pathDto.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }


}

