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
            modelBuilder.Entity<Customer>().HasOne(a => a.Address).WithMany().HasForeignKey(a=>a.AddressID).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee>().HasOne(a => a.Address).WithMany().HasForeignKey(a => a.AddressID).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Employee>().HasOne(a => a.Store).WithMany().HasForeignKey(a => a.StoreID).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Employee>().Property(b => b.Role).HasDefaultValue(Role.Employee);
            modelBuilder.Entity<Store>().HasOne(a => a.Supervisor).WithMany().IsRequired(false).HasForeignKey(a => a.SupID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Store>().HasOne(a => a.Address).WithOne().IsRequired(false).HasForeignKey<Store>(a => a.AddressID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Package>().HasOne(a => a.Sender).WithMany().HasForeignKey(a => a.SenderID).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Package>().HasOne(a => a.Receiver).WithMany().HasForeignKey(a => a.ReceiverID).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Package>().HasOne(a => a.Destination).WithMany().HasForeignKey(a => a.AddressID).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Package>().HasOne(a => a.Type).WithMany().HasForeignKey(a => a.ClassID).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Sale>().HasOne(a => a.Product).WithMany().HasForeignKey(a => a.ProductID).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TrackingRecord>().HasOne(a => a.Employee).WithMany().IsRequired(false).HasForeignKey(a => a.EmployeeId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TrackingRecord>().HasOne(a => a.Package).WithMany().IsRequired(true).HasForeignKey(a => a.TrackNum).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TrackingRecord>().HasOne(a => a.Store).WithMany().IsRequired(true).HasForeignKey(a => a.StoreId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TrackingRecord>().HasOne(a => a.Address).WithMany().IsRequired(false).HasForeignKey(a => a.Destination).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Sale>().HasOne(a => a.Buyer).WithMany().IsRequired(false).HasForeignKey(a => a.BuyerID).OnDelete(DeleteBehavior.NoAction);

            


            modelBuilder.Entity<Customer>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(p => p.PhoneNumber).IsUnique();



        }
    }
}
