using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISEPay.DAL.Persistence.Config
{
    internal class ImageConfig : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");

            // Primary Key
            builder.HasKey(i => i.Id);

            // Properties
            builder.Property(i => i.ImageName)
                   .IsRequired()
                   .HasMaxLength(255); // Maximum length for image name

            builder.Property(i => i.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(500); // Maximum length for image URL

            builder.Property(i => i.ImageType)
                   .IsRequired()
                   .HasMaxLength(50); // Maximum length for image type (e.g., jpg, png)

            // Foreign Key
            builder.HasOne(i => i.User)
                   .WithMany(u => u.Images)
                   .HasForeignKey(i => i.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // Ensures deleting a user deletes the images associated with them

            builder.Property(i => i.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()"); // Default value for CreatedAt (UTC time)

            builder.Property(i => i.UpdatedAt);

            // Indexes
            builder.HasIndex(i => i.ImageUrl)
                   .IsUnique(); // Enforcing unique image URL (optional, based on your needs)
        }
    }
}
