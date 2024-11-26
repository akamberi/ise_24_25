using CarRental.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CarRental.DAL.Persistence;

internal class CarRentalDbContext : DbContext
{
    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    //public DbSet<Car> Cars { get; set; }
    //public DbSet<CarBrand> CarBrands { get; set; }
}
