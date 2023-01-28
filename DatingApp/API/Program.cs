using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => {                                 //adding DataContxt as a service
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));  //reading the connection string from config file
});
builder.Services.AddCors();  
builder.Services.AddScoped<ITokenService,TokenService>();
   //adding cors service
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseCors(builder=>builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));      //allowing alltype of header and methods
app.MapControllers();

app.Run();
