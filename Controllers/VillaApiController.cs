using Microsoft.AspNetCore.Mvc;

namespace VillaApi.Controllers
{
    [ApiController]//defines it is a controller
    public class VillaApiController : ControllerBase //controllerBase doesnot have support for views
    {
        public VillaApiController()
        {
        }
    }

}