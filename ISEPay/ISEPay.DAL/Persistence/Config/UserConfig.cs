using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        // Primary Key
        builder.HasKey(x => x.Id);

        // Properties
        builder.Property(x => x.FullName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Gender)
               .HasMaxLength(10);

        builder.Property(x => x.CardID)
               .HasMaxLength(50);

        builder.Property(x => x.Nationality)
               .HasMaxLength(50);

        builder.Property(x => x.Birthday)
               .HasColumnType("date");

        builder.Property(x => x.BirthCity)
               .HasMaxLength(50);

        builder.Property(x => x.PhoneNumber)
               .HasMaxLength(15);

        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Password)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.UpdatedAt)
               .IsRequired();

        builder.Property(x => x.Status)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(x => x.AdressID)
               .IsRequired();

        builder.Property(x => x.RoleID)
               .IsRequired();

        // Relationships
        builder.HasOne(x => x.Address)
               .WithMany() // Adjust if Address has a navigation property
               .HasForeignKey(x => x.AdressID)
               .IsRequired();

        builder.HasOne(x => x.Role)
               .WithMany() // Adjust if Role has a navigation property
               .HasForeignKey(x => x.RoleID)
               .IsRequired();

        // Configuration for Accounts
        builder.HasMany(x => x.Accounts)
               .WithOne(x => x.User) // Navigation property in Account pointing to User
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(x => x.Email)
               .IsUnique();

        builder.HasIndex(x => x.PhoneNumber)
               .IsUnique(false);
    }
}
