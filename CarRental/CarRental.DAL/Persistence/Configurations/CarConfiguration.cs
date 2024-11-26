using CarRental.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.Persistence.Configurations;

internal class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);
        builder.Property(x => x.VIN)
               .IsRequired()
               .HasMaxLength(100);
        builder.HasIndex(x => x.VIN)
               .IsUnique();
        builder.HasIndex(x => x.Name)
               .IsUnique();
        builder.HasOne(x => x.Brand)
               .WithMany()
               .HasForeignKey(x => x.BrandId);
    }
}
