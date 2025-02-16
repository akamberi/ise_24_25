using ISEPay.BLL.Services.Scoped;
using ISEPay.DAL;
using ISEPay.DAL.Persistence.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ISEPay.BLL.Services;

public static class Startup
{

    static void Main(string[] args)
    {
        Console.WriteLine("Test project starting...");
    }
    public static void RegisterBLLServices(this IServiceCollection services, IConfiguration config)
    {
        services.RegisterDALServices(config);
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IOtpService,OTPService>(); 
        services.AddScoped<IAccountService,AccountService>();
        // Register IHttpContextAccessor correctly
        services.AddHttpContextAccessor();
        // services.AddScoped<IAddressService, AddressService>(); 
        services.AddScoped<ITransferService,TransferService>(); 
        services.AddScoped<IAccountService,AccountService>(); 
       // services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<ICurrencyService,CurrencyService>(); 
        services.AddScoped<IExchangeRateService,ExchangeRateService>(); 
        //services.AddScoped<IExchangeRateService,ExchangeRateService>(); 
       // services.AddScoped<IAddressService, AddressService>(); 
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<FeeService>(); 

    }
}
