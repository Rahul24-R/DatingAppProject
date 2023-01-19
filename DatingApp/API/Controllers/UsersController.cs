using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]                 //indicate that this is used for HTTP api response
    [Route("api/[controller]")]                 // localhost/api/user - where [controller] is a place holer and will be like usercontroller
    public class UsersController:ControllerBase //when an api is called this class object is created
    {
        private readonly DataContext _context;

        public UsersController(DataContext context) //datacontext creates a session with the DB as a service as mentioned in the program.cs
        {
           this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){ //action result is used to handle API responses like 404 not found etc..

            var user =await _context.Users.ToListAsync();
            if(user==null){
                return NotFound();
            }
            return user;
        }
        [HttpGet("{id}")]       //specific user
        public async Task<ActionResult<AppUser>> GetUser(int id){
            return await _context.Users.FindAsync(id);
        }
    }
}