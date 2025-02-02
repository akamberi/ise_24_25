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
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<Fee> Fees { get; set; }

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
                    modelBuilder.Entity<ExchangeRate>()
            .HasOne(x => x.FromCurrency)
            .WithMany(c => c.FromExchangeRates)
            .HasForeignKey(x => x.FromCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);  // Ensure the delete behavior is applied here

            modelBuilder.Entity<ExchangeRate>()
                .HasOne(x => x.ToCurrency)
                .WithMany(c => c.ToExchangeRates)
                .HasForeignKey(x => x.ToCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.AccountOutId);

            // Apply Currency and ExchangeRate configurations
            modelBuilder.ApplyConfiguration(new CurrencyConfig());
            modelBuilder.ApplyConfiguration(new ExchangeRateConfig());
            
            

            modelBuilder.Entity<Fee>()
                .Property(f => f.Id)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Fee>()
                .Property(f => f.TransactionType)
                .HasConversion<string>() 
                .IsRequired();

            modelBuilder.Entity<Fee>()
                .Property(f => f.FeeValue)
                .HasColumnType("decimal(18, 2)") 
                .IsRequired();
        }
    }
}
