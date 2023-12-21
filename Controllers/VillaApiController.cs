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

//      Get Villa By Id

        [HttpGet("id")]
//     [HttpGet("{id:int}")] if id is not written here it will throw an error that the request matched multiple points
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
    }


}