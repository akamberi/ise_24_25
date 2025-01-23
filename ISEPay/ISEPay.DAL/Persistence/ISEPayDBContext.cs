using Microsoft.EntityFrameworkCore;
using ISEPay.DAL.Persistence.Entities;
using ISEPay.DAL.Persistence.Config;

namespace ISEPay.DAL.Persistence
{
    public class ISEPayDBContext : DbContext
    {
        public ISEPayDBContext(DbContextOptions<ISEPayDBContext> options) : base(options) { }

        // Define DbSet for each entity
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Image> Images { get; set; } // Added DbSet for Images


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User entity configuration
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWID()");

            // Role entity configuration
            modelBuilder.Entity<Role>()
                .Property(r => r.Id)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity(j => j.ToTable("RolePermissions"));

            // Permission entity configuration
            modelBuilder.Entity<Permission>()
                .Property(p => p.Id)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWID()");

            // Address entity configuration
            modelBuilder.ApplyConfiguration(new AddressConfig());

            // Account entity configuration
            modelBuilder.Entity<Account>()
                .Property(a => a.Id)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Account>()
                .HasMany(a => a.IncomingTransactions)
                .WithOne(t => t.AccountIn)
                .HasForeignKey(t => t.AccountInId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.OutgoingTransactions)
                .WithOne(t => t.AccountOut)
                .HasForeignKey(t => t.AccountOutId)
                .OnDelete(DeleteBehavior.Restrict);

            // Transaction entity configuration
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Id)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.AccountInId);

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.AccountOutId);

            // Image entity configuration
            modelBuilder.Entity<Image>()
                .Property(i => i.Id)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Image>()
                .HasOne(i => i.User)
                .WithMany(u => u.Images)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
