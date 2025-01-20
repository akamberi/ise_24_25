using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ISEPay.DAL.Persistence.Entities;

namespace ISEPay.DAL.Persistence.Config
{
    internal class ExchangeRateConfig : IEntityTypeConfiguration<ExchangeRate>
    {
        public void Configure(EntityTypeBuilder<ExchangeRate> builder)
        {
            builder.ToTable("ExchangeRates");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Rate)
                   .HasColumnType("decimal(18, 6)")
                   .IsRequired();

            builder.Property(x => x.EffectiveDate)
                   .IsRequired();

            // Relationships
            builder.HasOne(x => x.FromCurrency)
                   .WithMany(c => c.FromExchangeRates) // Navigation property on Currency
                   .HasForeignKey(x => x.FromCurrencyId)
                   .OnDelete(DeleteBehavior.Restrict);  // Prevents deletion if related ExchangeRates exist

            builder.HasOne(x => x.ToCurrency)
                   .WithMany(c => c.ToExchangeRates) // Navigation property on Currency
                   .HasForeignKey(x => x.ToCurrencyId)
                   .OnDelete(DeleteBehavior.Restrict);  // Prevents deletion if related ExchangeRates exist

            // Indices
            builder.HasIndex(x => x.FromCurrencyId);
            builder.HasIndex(x => x.ToCurrencyId);
            builder.HasIndex(x => x.EffectiveDate);
        }
    }
}