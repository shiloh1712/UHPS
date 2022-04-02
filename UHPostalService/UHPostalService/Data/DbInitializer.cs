using UHPostalService.Models;

namespace UHPostalService.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any students.
            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }

            var firstAddresses = new Address[]
            {
                new Address { StreetAddress = "4800 Calhoun Rd", City = "Houston", State = "TX", Zipcode = "77024" },
                new Address { StreetAddress = "4500 University Dr", City = "Houston", State = "TX", Zipcode = "77004" },
                new Address { StreetAddress = "4373 Cougar Village Dr", City = "Houston", State = "TX", Zipcode = "77204" },
                new Address { StreetAddress = "UH Entrance 14", City = "Houston", State = "TX", Zipcode = "77004" },
                new Address { StreetAddress = "4455 University Dr", City = "Houston", State = "TX", Zipcode = "77204" }
            };

            context.Addresses.AddRange(firstAddresses);
            context.SaveChanges();

            var firstCustomers = new Customer[]
            {
                new Customer { Name= "Bader", PhoneNumber ="1234567890", Email ="bader@email.com", Password ="Bader", AddressID =4/*to test if address will auto based on ID, Address =*/},
                new Customer { Name= "Morrison", PhoneNumber ="0987654321", Email ="morrison@email.com", Password ="Morrison", AddressID =5, Address = context.Addresses.Where(p=> (p.Id == 5)).FirstOrDefault()},
            };

            context.Customers.AddRange(firstCustomers);
            context.SaveChanges();
           
        }
    }
}
