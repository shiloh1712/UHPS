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

        public DbSet<Customer> Customer { get; set; }
    }
}
