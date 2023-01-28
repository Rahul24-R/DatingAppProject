using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;     //key used to encrypt and decrypt the data 
                                                        //there is asymmetrickey servr needs to encrypt and client needs to decrypt
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])); //getting the token key from the cofig file and converting to byte arraay
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Name,user.UserName) //information that the user claims
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor{
                Subject= new ClaimsIdentity(claims),        // data
                Expires=DateTime.Now.AddDays(7),            //expiry of token
                SigningCredentials=creds                    //token signature
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescription); // create token

            return tokenhandler.WriteToken(token);
        }
    }
}