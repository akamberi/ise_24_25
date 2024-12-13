using CarRental.DAL.Persistence;
using CarRental.DAL.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.DAL;

public static class Startup
{
    public static void RegisterDALServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CarRentalDbContext>(opt=>
        {
            opt.UseSqlServer(config.GetConnectionString("CarRental"));
        });
        services.AddScoped<ICarBrandsRepository, CarBrandsRepository>();
    }
}
