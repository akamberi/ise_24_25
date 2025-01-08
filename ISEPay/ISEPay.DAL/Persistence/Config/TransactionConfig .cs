using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ISEPay.DAL.Persistence.Entities;

namespace ISEPay.DAL.Persistence.Config
{
    internal class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Type)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.Amount)
                   .HasColumnType("decimal(18, 2)")
                   .IsRequired();

            builder.Property(x => x.FeeValue)
                   .HasColumnType("decimal(18, 2)")
                   .HasDefaultValue(0);

            builder.Property(x => x.Timestamp)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(500); // Adjust length as needed

            // Relationships
            builder.HasOne(x => x.AccountIn)
                   .WithMany(a => a.IncomingTransactions) // Updated navigation property
                   .HasForeignKey(x => x.AccountInId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AccountOut)
                   .WithMany(a => a.OutgoingTransactions) // Updated navigation property
                   .HasForeignKey(x => x.AccountOutId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Optional Relationships (Uncomment and adjust if needed)
            /*
            builder.HasOne(x => x.Agent)
                   .WithMany() // Adjust if Agent has navigation properties
                   .HasForeignKey(x => x.AgentId)
                   .OnDelete(DeleteBehavior.Restrict);
            */

            // Indices
            builder.HasIndex(x => x.AccountInId);
            builder.HasIndex(x => x.AccountOutId);
            builder.HasIndex(x => x.Timestamp);
        }
    }
}
