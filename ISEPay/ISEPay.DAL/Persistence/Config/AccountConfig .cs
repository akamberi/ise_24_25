using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

internal class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        // Primary Key
        builder.HasKey(x => x.Id);

        // Properties
        builder.Property(x => x.AccountNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(x => x.Balance)
               .HasColumnType("decimal(18, 2)")
               .IsRequired();

        builder.Property(x => x.Currency)
               .IsRequired()
               .HasMaxLength(3); // ISO currency code

        builder.Property(x => x.Status)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(x => x.Type)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(x => x.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.UpdatedAt)
               .IsRequired();

        // Relationships
        builder.HasOne(x => x.User)
               .WithMany(x => x.Accounts) // Assuming User has ICollection<Account>
               .HasForeignKey(x => x.UserId)
               .IsRequired();

        // Map IncomingTransactions
        builder.HasMany(x => x.IncomingTransactions)
               .WithOne(x => x.AccountIn)
               .HasForeignKey(x => x.AccountInId)
               .OnDelete(DeleteBehavior.Restrict);

        // Map OutgoingTransactions
        builder.HasMany(x => x.OutgoingTransactions)
               .WithOne(x => x.AccountOut)
               .HasForeignKey(x => x.AccountOutId)
               .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(x => x.AccountNumber)
               .IsUnique();

        builder.HasIndex(x => x.UserId);
    }
}
