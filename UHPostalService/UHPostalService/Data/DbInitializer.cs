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

            var addresses = new Address[]
            {
                new Address { StreetAddress = "4800 Calhoun Rd", City = "Houston", State = "TX", Zipcode = "77024" },
                new Address { StreetAddress = "4500 University Dr", City = "Houston", State = "TX", Zipcode = "77004" },
                /*
                new Address { StreetAddress = "4373 Cougar Village Dr", City = "Houston", State = "TX", Zipcode = "77204" },
                new Address { StreetAddress = "UH Entrance 14", City = "Houston", State = "TX", Zipcode = "77004" },
                new Address { StreetAddress = "4455 University Dr", City = "Houston", State = "TX", Zipcode = "77204" },
                new Address { StreetAddress = "another address", City = "Cypress", State = "TX", Zipcode = "77433" },
                new Address { StreetAddress = "one other address", City = "Austin", State = "TX", Zipcode = "73301" },*/


            };
            context.Addresses.AddRange(addresses);
            context.SaveChanges();

            var custs = new Customer[]
            {
                new Customer { Name= "Bader", PhoneNumber ="1234567890", Email ="bader@email.com", Password ="Bader"},
                new Customer { Name= "Morrison", PhoneNumber ="0987654321", Email ="morrison@email.com", Password ="Morrison"},
            };
            context.Customers.AddRange(custs);
            context.SaveChanges();

            var emps = new Employee[]
            {
                new Employee { Name= "Son", PhoneNumber ="1234567890", Email ="son@uhps.com", Password ="son", AddressID =1, StoreID = 1},
                new Employee { Name= "Josh", PhoneNumber ="2345678901", Email ="josh@uhps.com", Password ="josh", AddressID =2, StoreID = 2},
            };
            context.Employees.AddRange(emps);
            context.SaveChanges();
            
        }
    }
}
