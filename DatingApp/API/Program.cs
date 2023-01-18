using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => {                                 //adding DataContxt as a service
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));  //reading the connection string from config file
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();
