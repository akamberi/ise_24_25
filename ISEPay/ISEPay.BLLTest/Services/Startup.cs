using ISEPay.BLL.Services.Scoped;
using ISEPay.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ISEPay.BLL.Services;

public static class Startup
{
    public static void RegisterBLLServices(this IServiceCollection services, IConfiguration config)
    {
        services.RegisterDALServices(config);
        services.AddScoped<IUserService, UserService>();
    }
}
