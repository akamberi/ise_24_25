using Microsoft.EntityFrameworkCore;
using ISEPay.DAL.Persistence.Entities;
using System.Collections.Generic;
using System;
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
        public DbSet<Address> Addresses { get; set; } // Added Address DbSet
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
// User entity configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Id)
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("NEWID()");

                // Configure one-to-one relationship with Account
                entity.HasOne(u => u.Account)
                    .WithOne(a => a.User)
                    .HasForeignKey<Account>(a => a.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            // Role entity configuration
            modelBuilder.Entity<Role>()
                .Property(r => r.Id)
                .HasColumnType("uniqueidentifier") // SQL Server uniqueidentifier type
                .HasDefaultValueSql("NEWID()"); // SQL Server default value for UUID

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions) 
                .WithMany(p => p.Roles)
                .UsingEntity(j => j.ToTable("RolePermissions")); // Join table for many-to-many relationship between Role and Permission

            // Permission entity configuration
            modelBuilder.Entity<Permission>()
                .Property(p => p.Id)
                .HasColumnType("uniqueidentifier") // SQL Server uniqueidentifier type
                .HasDefaultValueSql("NEWID()"); // SQL Server default value for UUID

            // Address entity configuration (Apply configuration class)
            modelBuilder.ApplyConfiguration(new AddressConfig());
            
            
            // Account entity configuration
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(a => a.Id)
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("NEWID()");

                // Configure one-to-many relationship with Wallet
                entity.HasMany(a => a.Wallets)
                    .WithOne(w => w.Account)
                    .HasForeignKey(w => w.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            // Wallet entity configuration
            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.Property(w => w.Id)
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("NEWID()");

                entity.HasOne(w => w.Account)
                    .WithMany(a => a.Wallets)
                    .HasForeignKey(w => w.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Configure other Wallet properties
                entity.Property(w => w.Balance)
                    .HasColumnType("decimal(18,2)");

                entity.Property(w => w.Currency)
                    .HasMaxLength(3)
                    .IsRequired();

                entity.Property(w => w.AccountNumber)
                    .HasMaxLength(50)
                    .IsRequired();
            });
        }
    }
}
