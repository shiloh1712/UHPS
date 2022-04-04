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

            /*var packs = new Package[]
            {
                new Package { SenderID= 1, ReceiverID =1, AddrToID =1, ShipCost =2.89F},
                new Package { SenderID= 2, ReceiverID =2, AddrToID =2, ShipCost =3.50F},
            };
            context.Packages.AddRange(packs);
            context.SaveChanges();

            var prods = new Product[]
            {
                new Product { Desc= "Item description", UnitCost =5.99F, Stock =100},
                new Product { Desc= "New item description", UnitCost =10.99F, Stock =50},
            };
            context.Products.AddRange(prods);
            context.SaveChanges();

            var sales = new Sale[]
            {
                new Sale { ItemID= 1, Quantity =75, PurchaseDate =DateTime.Parse("2010-09-01")},
                new Sale { ItemID= 2, Quantity =30, PurchaseDate =DateTime.Parse("2015-07-24")},
            };
            context.Sales.AddRange(sales);
            context.SaveChanges();

            var ships = new ShipmentClass[]
            {
                new ShipmentClass { DESCR= "Shipment Class Description", Length =4.50, Height =1.00, Width= 7.00, GroundCost =1.50, ExpressCost =12.00},
                new ShipmentClass { Descr= "New Shipment Class Description", Length =5.00, Height =2.00, Width= 7.50, GroundCost =2.00, ExpressCost =13.00},
            };
            context.ShipmentClass.AddRange(ships);
            context.SaveChanges();

             var stores = new Store[]
            {
                new Store { SupID= 1, PhoneNumber ="1357924680", AddressID =1},
                new Store { SupID= 2, PhoneNumber ="2468013579", AddressID =2},
            };
            context.ShipmentClass.AddRange(stores);
            context.SaveChanges();

             var tracks = new TrackingRecord[]
            {
                new TrackingRecord { EmployeeId= 1, TrackNum ="123456", StoreId =1, TimeIn= DateTime.Parse("2012-04-12"), TimeOut =DateTime.Parse("2012-04-18"), Destination =1},
                new TrackingRecord { EmployeeId= 2, TrackNum ="789012", StoreId =2, TimeIn= DateTime.Parse("2016-03-22"), TimeOut =DateTime.Parse("2016-03-28"), Destination =2},
            };
            context.ShipmentClass.AddRange(tracks);
            context.SaveChanges();
            */
            
        }



    }
}
