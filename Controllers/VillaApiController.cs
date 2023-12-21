using Microsoft.AspNetCore.Mvc;
using VillaApi.Models;

namespace VillaApi.Controllers
{
    [ApiController]//defines it is a controller
    [Route("api/VillaApi")]
    public class VillaApiController : ControllerBase //controllerBase doesnot have support for views
    {
        [HttpGet]
        public IEnumerable<Villa> GetVillas()
        {
            return new List<Villa>()
            {
                new Villa(){Id=1, Name="Pool Villa"},
                new Villa(){Id=2, Name="Beach Villa"},

            };
        }
    }


}