using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly DataContext context;

        public AccountController(DataContext context)
        {
            this.context = context;
            
        }
        [HttpPost("register")]      //api/account/register?username=abcd&password=pwd
        public async Task<ActionResult<AppUser>> Register(string username,string password){

            using var hmac = new HMACSHA512();      //hasing algo - randomly generated key
                                                    //using is used to dispose memory of the class after it is used by garbage collector
            var user = new AppUser() {
                UserName=username,
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt=hmac.Key
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }
    }
}