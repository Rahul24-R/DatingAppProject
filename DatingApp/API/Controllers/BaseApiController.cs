
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]                 //indicate that this is used for HTTP api response
    [Route("api/[controller]")]      // localhost/api/user - where [controller] is a place holer and will be like usercontroller
    public class BaseApiController:ControllerBase
    {
        
    }
}