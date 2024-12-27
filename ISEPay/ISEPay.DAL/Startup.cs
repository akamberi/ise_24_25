using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ISEPay.DAL.Persistence;
using ISEPay.DAL.Persistence.Repositories;

public static class Startup
{
    public static void RegisterDALServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ISEPayDBContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("ISEPay"));
        });
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
       // services.AddScoped<IRolesRepository, RolesRepository>();
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Application started!");
        // Add your application logic here
    }
}