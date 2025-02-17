using Microsoft.EntityFrameworkCore;
using BusPortal.DAL.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace BusPortal.DAL.Persistence
{
    public class DALDbContext : IdentityDbContext<IdentityUser>
    {
        public DALDbContext(DbContextOptions<DALDbContext> options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);


                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Seat)
                    .IsRequired()
                    .HasMaxLength(10);


                entity.Property(e => e.DateTime)
               .IsRequired();

                entity.HasKey(e => e.Id);

             //   entity.HasOne(e => e.Line)
             //.WithMany()
             //.HasForeignKey(e => e.Line)
             //.OnDelete(DeleteBehavior.Restrict);


            //    entity.HasOne(b => b.Client)
            //.WithMany()  // Client will have a collection of Bookings
            //.HasForeignKey(b => b.Client)
            //.OnDelete(DeleteBehavior.Restrict);
            });
        }
    }

}
