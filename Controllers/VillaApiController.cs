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

        // Get list of villas
        
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        //Get Villa By Id

        [HttpGet("id",Name="GetVilla")]
        //[HttpGet("{id:int}")] if id is not written here it will throw an error that the request matched multiple points
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // [ProducesResponseType(200)]
        // [ProducesResponseType(400)]      can also be written like this so the response type will not be undocumented
        // [ProducesResponseType(404)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            
            if(villa is null)
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
        public ActionResult<VillaDto>CreateVilla([FromBody] VillaDto VillaDto)
        {
            if (VillaDto == null)
            {
                return BadRequest();
            }
            if (VillaDto.Id>0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            VillaDto.Id=VillaStore.villaList.OrderByDescending(item=>item.Id).FirstOrDefault().Id+1;
            VillaStore.villaList.Add(VillaDto);
            return CreatedAtRoute("GetVilla",new {id = VillaDto.Id},VillaDto);
        }

    }


}

