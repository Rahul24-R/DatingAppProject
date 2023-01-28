using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]     //requires an token authentication to access the endpoints of this controller
    public class UsersController:BaseApiController //when an api is called this class object is created
    {
        private readonly DataContext _context;

        public UsersController(DataContext context) //datacontext creates a session with the DB as a service as mentioned in the program.cs
        {
           this._context = context;
        }

        [HttpGet]
        [AllowAnonymous]        //allowa accesswithout token autherization
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){ //action result is used to handle API responses like 404 not found etc..
                //async to mke the code async and task<> where the task has a return value, if it doesnt it  async task getusers()
            var user =await _context.Users.ToListAsync();   //await to complete the task , then to proceed with next task in the method
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