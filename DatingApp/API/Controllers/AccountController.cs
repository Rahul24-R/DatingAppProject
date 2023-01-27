using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly DataContext context;

        public AccountController(DataContext context)
        {
            this.context = context;
            
        }
        // [HttpPost("register")]      //api/account/register?username=abcd&password=pwd
        // public async Task<ActionResult<AppUser>> Register(string username,string password){

        //     using var hmac = new HMACSHA512();      //hasing algo - randomly generated key
        //                                             //using is used to dispose memory of the class after it is used by garbage collector
        //     var user = new AppUser() {
        //         UserName=username,
        //         PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
        //         PasswordSalt=hmac.Key
        //     };

        //     context.Users.Add(user);
        //     await context.SaveChangesAsync();

        //     return user;
        // }
        [HttpPost("register")]      //api/account/register?username=abcd&password=pwd
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerdto){     //using objects -DTO (Data transfer Object)
            if(await userExist(registerdto.Username)){
                return BadRequest("User Exists");
            }

            using var hmac = new HMACSHA512();      //hasing algo - randomly generated key
                                                    //using is used to dispose memory of the class after it is used by garbage collector
            var user = new AppUser() {
                UserName=registerdto.Username.ToLower(),
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerdto.Password)),
                PasswordSalt=hmac.Key
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        [HttpPost("login")]
        public async Task <ActionResult<AppUser>> Login(LoginDto logindto){

            var user = await context.Users.SingleOrDefaultAsync(x => x.UserName==logindto.username);
            if(user == null){
                return Unauthorized("User not Found");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);     //we pass the key to use the same password hashing
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.password)); //computedHash is am array of bytes

            for(int i=0 ; i<computedHash.Length;i++){
                if(computedHash[i]!= user.PasswordHash[i]){
                    return Unauthorized("invalid password");
                }
            }
            return user;
        }

        private async Task<bool> userExist(string username){
            return await context.Users.AnyAsync(x=>x.UserName==username.ToLower());
        }
    }
}