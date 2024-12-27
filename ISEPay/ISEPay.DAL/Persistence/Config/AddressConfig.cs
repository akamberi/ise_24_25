using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ISEPay.DAL.Persistence.Entities;

namespace ISEPay.DAL.Persistence.Config
{
    internal class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Country)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.City)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Street)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.Zipcode)
                   .IsRequired(false); // Zipcode can be nullable

            // Indexes (if needed, for example, you can index City for better performance in queries)
            builder.HasIndex(x => x.City)
                   .IsUnique(false); // Index on City, not unique
        }
    }
}
