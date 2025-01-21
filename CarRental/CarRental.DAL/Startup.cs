using CarRental.DAL.Persistence;
using CarRental.DAL.Persistence.Entities;
using CarRental.DAL.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CarRental.DAL;

public static class Startup
{
    public static void RegisterDALServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CarRentalDbContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("CarRental"));
        });

        services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CarRentalDbContext>();

        services.AddScoped<ICarBrandsRepository, CarBrandsRepository>();
    }
}
