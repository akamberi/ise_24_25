using ISEPay.BLL.Services.Scoped;
using ISEPay.DAL;
using ISEPay.DAL.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ISEPay.BLL.Services;

public static class Startup
{

    static void Main(string[] args)
    {
        Console.WriteLine("Test project starting...");
        // You can add additional logic to run tests manually or handle setup here.
    }
    public static void RegisterBLLServices(this IServiceCollection services, IConfiguration config)
    {
        services.RegisterDALServices(config);
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        //services.AddScoped<IAD, RoleService>();

        /*   services.AddIdentityCore<User>()
              .AddRoles<Role>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();*/

        // Alternatively, explicitly register IPasswordHasher
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

    }
}
