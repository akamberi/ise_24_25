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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User entity configuration
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasColumnType("uniqueidentifier") // SQL Server uniqueidentifier type
                .HasDefaultValueSql("NEWID()"); // SQL Server default value for UUID

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
        }
    }
}
