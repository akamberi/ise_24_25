using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DAL.Persistence.Entities; // Adjust this if needed

namespace DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
        public DbSet<CourseModule> CourseModules { get; set; }
        public DbSet<LessonFile> LessonFiles { get; set; }
        public DbSet<Payment> Payments { get; set; }

        // Configure model relationships and other settings
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Identity-related customizations
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

            // Configure relationships and constraints for custom models
            builder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany()
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Course>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);  // Ensures proper storage in SQL Server

            builder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);  // Ensures proper storage in SQL Server


            builder.Entity<CourseEnrollment>()
                .HasOne(ce => ce.Course)
                .WithMany(c => c.CourseEnrollments)
                .HasForeignKey(ce => ce.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AssignmentSubmission>()
                .HasOne(asg => asg.Assignment)
                .WithMany()
                .HasForeignKey(asg => asg.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AssignmentSubmission>()
                .HasOne(asg => asg.User)
                .WithMany()
                .HasForeignKey(asg => asg.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CourseModule>()
                .HasOne(cm => cm.Course)
                .WithMany(c => c.CourseModules)
                .HasForeignKey(cm => cm.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<LessonFile>()
                .HasOne(lf => lf.CourseModule)
                .WithMany(cm => cm.LessonFiles)
                .HasForeignKey(lf => lf.CourseModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
