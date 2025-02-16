using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISEPay.DAL.Persistence.Config
{
    internal class CurrencyConfig : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Code)
                   .IsRequired()
                   .HasMaxLength(3); // ISO 4217 currency code

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100); // Maximum length for currency name

            builder.Property(x => x.Symbol)
                   .HasMaxLength(10); // Maximum length for currency symbol

            builder.Property(x => x.IsActive)
                   .IsRequired();

            builder.Property(x => x.Country)
                   .HasMaxLength(100); // Maximum length for country name

            builder.Property(x => x.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(x => x.Code)
                   .IsUnique(); // Ensure unique currency codes
        }
    }
}
