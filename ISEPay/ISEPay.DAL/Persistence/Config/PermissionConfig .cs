using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISEPay.DAL.Persistence.Config
{
    internal class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Description)
                   .HasMaxLength(500);

            builder.Property(p => p.Resource)
                   .HasMaxLength(100);

            builder.Property(p => p.ActionType)
                   .HasMaxLength(50);

            builder.Property(p => p.IsActive)
                   .IsRequired();

            builder.Property(p => p.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.UpdatedDate);

            builder.Property(p => p.PermissionType)
                   .HasMaxLength(50);

            // Indexes
            builder.HasIndex(p => p.Name)
                   .IsUnique();
        }
    }
}
