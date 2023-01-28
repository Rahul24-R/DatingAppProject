using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration config){

            //install package microsoft.aspnetcore.authenticatio.jwtbearer
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters=new TokenValidationParameters{ //parameters for token validation
                            ValidateIssuerSigningKey=true,          //the jwt token requires a signed issuer
                            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),   //getting the signature key
                            ValidateIssuer=false,       // no need to validate the issues
                            ValidateAudience=false
                    };
            });

            return services;
        }
    }
}