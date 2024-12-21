using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ISEPay.DAL.Persistence.Entities;

namespace ISEPay.DAL.Persistence.Config
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("Users");

            // Set primary key
            builder.HasKey(x => x.Id);

            // Configure properties
            builder.Property(x => x.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Gender)
                   .HasMaxLength(10);

            builder.Property(x => x.Nationality)
                   .HasMaxLength(50);

            builder.Property(x => x.Birthday)
                   .HasColumnType("date");

            builder.Property(x => x.BirthCity)
                   .HasMaxLength(50);

            builder.Property(x => x.PhoneNumber)
                   .HasMaxLength(15); // Adjust max length as needed for phone numbers

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Password)
                   .IsRequired()
                   .HasMaxLength(255); // Adjust for password hashing

            builder.Property(x => x.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdatedAt)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .HasConversion<string>() // Store enum as string
                   .IsRequired();

            // Configure unique index on Email
            builder.HasIndex(x => x.Email)
                   .IsUnique();

            // Additional indexes if required
            builder.HasIndex(x => x.PhoneNumber)
                   .IsUnique(false); // Allows duplicates, change to true if unique is needed
        }
    }
}
