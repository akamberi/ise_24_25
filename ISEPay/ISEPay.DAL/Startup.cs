using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ISEPay.DAL.Persistence;
using ISEPay.DAL.Persistence.Repositories;
using static ISEPay.DAL.Persistence.Repositories.IAccountRepository;

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
         services.AddScoped<IAccountRepository, AccountsRepository>();
         services.AddScoped<ITransactionsRepository, TransactionsRepository>();
         services.AddScoped<_ImagesRepository, ImagesRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Application started!");
        // Add your application logic here
    }

}