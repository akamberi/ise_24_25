using CarRental.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.Persistence.Configurations;

internal class CarBrandConfiguration : IEntityTypeConfiguration<CarBrand>
{
    public void Configure(EntityTypeBuilder<CarBrand> builder)
    {
        builder.ToTable("CarBrands");
        builder.HasKey(x => x.Id);
        //builder.HasKey(x => new { x.Id, x.Name });
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(50);
        builder.Property(x => x.Logo)
               .IsRequired()
               .HasMaxLength(500);
        builder.HasIndex(x => x.Name)
               .IsUnique();
    }
}
