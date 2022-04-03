using Microsoft.EntityFrameworkCore;
using UHPostalService.Models;

namespace UHPostalService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ShipmentClass> ShipmentClasses { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<TrackingRecord> TrackingRecords { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasOne(a => a.Store).WithMany(c=>c.Employees).HasForeignKey(a => a.StoreID).IsRequired(false);
            modelBuilder.Entity<Store>().HasOne(a => a.Supervisor).WithOne(b => b.Supervised)
                .HasForeignKey<Store>(a => a.SupID).OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Customer>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(p => p.PhoneNumber).IsUnique();



        }
    }
}
