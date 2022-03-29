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
        public DbSet<Postage> Postages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ShipmentClass> ShipmentClasses { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<TrackingRecord> TrackingRecords { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
