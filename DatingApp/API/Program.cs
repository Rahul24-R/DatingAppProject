using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);     //extension method to add services to services -application services
builder.Services.AddIdentityServices(builder.Configuration);        //extension method for identity services - JWT token service
   //adding cors service
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseCors(builder=>builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));      //allowing alltype of header and methods
app.UseAuthentication(); //do u have  valid token -middleware
app.UseAuthorization(); //what u are allowed to do-middleware
app.MapControllers();

app.Run();
