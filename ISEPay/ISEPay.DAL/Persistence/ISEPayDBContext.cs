using Microsoft.EntityFrameworkCore;
using ISEPay.DAL.Persistence.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ISEPay.DAL.Persistence
{
    public class ISEPayDBContext : DbContext
    {
        public ISEPayDBContext(DbContextOptions<ISEPayDBContext> options) : base(options) { }

        // Define DbSet for each entity
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // You can add configurations for your entities here
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasColumnType("uniqueidentifier") // SQL Server uniqueidentifier type
                .HasDefaultValueSql("NEWID()"); // SQL Server default value for UUID
        }
    }
}
