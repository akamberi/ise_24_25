using CarRental.BLL.Services.Scoped;
using CarRental.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.BLL.Services;

public static class Startup
{
    public static void RegisterBLLServices(this IServiceCollection services, IConfiguration config)
    {
        services.RegisterDALServices(config);
        services.AddScoped<ICarBrandService, CarBrandService>();
    }
}
