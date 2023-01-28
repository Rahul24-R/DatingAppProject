using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => {                                 //adding DataContxt as a service
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));  //reading the connection string from config file
});
builder.Services.AddCors();  
builder.Services.AddScoped<ITokenService,TokenService>();
//install package microsoft.aspnetcore.authenticatio.jwtbearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters=new TokenValidationParameters{ //parameters for token validation
        ValidateIssuerSigningKey=true,          //the jwt token requires a signed issuer
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),   //getting the signature key
        ValidateIssuer=false,       // no need to validate the issues
        ValidateAudience=false
    };
});
   //adding cors service
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseCors(builder=>builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));      //allowing alltype of header and methods
app.UseAuthentication(); //do u have  valid token -middleware
app.UseAuthorization(); //what u are allowed to do-middleware
app.MapControllers();

app.Run();
