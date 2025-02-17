using Azure.Core.GeoJson;
using BusPortal.DAL.Persistence;
using BusPortal.DAL.Persistence.Entities;
using BusPortal.DAL.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusPortal.DAL;

public static class Startup
{
    public static void RegisterDALServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DALDbContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("BusPortal"));
        });
        services.AddIdentity<IdentityUser, IdentityRole>()
           .AddEntityFrameworkStores<DALDbContext>()
           .AddDefaultTokenProviders();
        services.AddScoped<ILineRepository, LineRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<UserRepository>();

        //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}
