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
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas()
        {
            return VillaStore.villaList;
            
        }
       [HttpGet("id")]
        public VillaDto GetVilla(int id)
        {
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            return villa;
        } 
    }


}