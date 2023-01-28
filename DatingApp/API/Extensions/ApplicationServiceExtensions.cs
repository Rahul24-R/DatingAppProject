using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)    //we return service and extend the servicce class by (this iservicecollection)
        {
            services.AddDbContext<DataContext>(opt => {                                 //adding DataContxt as a service
             opt.UseSqlite(config.GetConnectionString("DefaultConnection"));  //reading the connection string from config file
            });
            services.AddCors();  
            services.AddScoped<ITokenService,TokenService>();
            return services;
        }
    }
}