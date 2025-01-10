using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.

            // Here you could define additional customizations related to email confirmation
            // and other business logic (e.g., add constraints or tables for roles if needed).

            // Example: Configure the default schema for Identity tables
            builder.Entity<IdentityUser>()
                .ToTable("Users");

            builder.Entity<IdentityRole>()
                .ToTable("Roles");

            builder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles");

            builder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims");

            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins");

            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims");

            builder.Entity<IdentityUserToken<string>>()
                .ToTable("UserTokens");

            // Example: Adding a custom configuration for email confirmation
            // (Though it's typically handled outside the DbContext, adding a custom User property
            // to track email confirmation states is possible.)

            // Uncomment and customize if you wish to add custom user properties related to email confirmation.
            // builder.Entity<IdentityUser>().Property<string>("EmailConfirmationStatus");

            // You can also seed default roles or other entities here if necessary.
        }
    }
}
