using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISEPay.DAL.Persistence.Config
{
    internal class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            // Primary Key
            builder.HasKey(r => r.Id);

            // Properties
            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(r => r.Description)
                   .HasMaxLength(500);

            builder.Property(r => r.IsActive)
                   .IsRequired();

            builder.Property(r => r.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(r => r.UpdatedDate);

            // Relationships (many-to-many with Permission)
            builder.HasMany(r => r.Permissions)
                   .WithMany(p => p.Roles) // Many-to-many relationship
                   .UsingEntity<Dictionary<string, object>>(
                       "RolePermission", // Join table
                       join => join.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                       join => join.HasOne<Role>().WithMany().HasForeignKey("RoleId"));

            // Indexes
            builder.HasIndex(r => r.Name)
                   .IsUnique();
        }
    }
}
