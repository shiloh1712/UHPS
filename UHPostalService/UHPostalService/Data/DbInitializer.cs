using UHPostalService.Models;

namespace UHPostalService.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any students./*
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            var addresses = new Address[]
            {
                new Address { StreetAddress = "4500 University Dr", City = "Houston", State = "TX", Zipcode = "77004" },
                
                new Address { StreetAddress = "4373 Cougar Village Dr", City = "Houston", State = "TX", Zipcode = "77204" },
                new Address { StreetAddress = "UH Entrance 14", City = "Houston", State = "TX", Zipcode = "77004" },
                new Address { StreetAddress = "4455 University Dr", City = "Houston", State = "TX", Zipcode = "77204" },
                new Address { StreetAddress = "another address", City = "Cypress", State = "TX", Zipcode = "77433" },
                new Address { StreetAddress = "one other address", City = "Austin", State = "TX", Zipcode = "73301" },


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
                new Employee { Name= "Son", PhoneNumber ="1234567890", Email ="son@uhps.com", Password ="son", AddressID =1, StoreID = 1, Role = Role.Employee},
                new Employee { Name= "Josh", PhoneNumber ="2345678901", Email ="josh@uhps.com", Password ="josh", AddressID =2, StoreID = 1},
            };
            context.Employees.AddRange(emps);
            context.SaveChanges();

            var packs = new Package[]
            {
                new Package { SenderID= 1, ReceiverID =1, AddressID =1, ShipCost =2.89F},
                new Package { SenderID= 2, ReceiverID =2, AddressID =2, ShipCost =3.50F},
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
                new Sale { ProductID= 1, Quantity =75, PurchaseDate =DateTime.Parse("2010-09-01")},
                new Sale { ProductID= 2, Quantity =30, PurchaseDate =DateTime.Parse("2015-07-24")},
            };
            context.Sales.AddRange(sales);
            context.SaveChanges();

            var ships = new ShipmentClass[]
            {
                new ShipmentClass { Desc= "Letter", MaxLength =11.50f, MaxHeight =6.20f, MaxWidth= 0.25f, GroundCost =1.50f, ExpressCost =9.00f},
                new ShipmentClass { Desc= "Large Envelope", MaxLength =15.00f, MaxHeight =12.00f, MaxWidth= 0.75f, GroundCost =2.00f, ExpressCost =10.00f},
                new ShipmentClass { Desc= "Box", MaxLength =90.00f, MaxHeight =90.00f, MaxWidth= 90.00f, GroundCost =4.00f, ExpressCost =15.00f},
            };
            context.ShipmentClasses.AddRange(ships);
            context.SaveChanges();

             var stores = new Store[]
            {
                new Store { SupID= 3, PhoneNumber ="135794680", AddressID =3},
                new Store { SupID= 2, PhoneNumber ="246813579", AddressID =2},
            };
            context.Stores.AddRange(stores);
            context.SaveChanges();

             var tracks = new TrackingRecord[]
            {
                new TrackingRecord { EmployeeId= 1, TrackNum =1, StoreId =1, TimeIn= DateTime.Parse("2012-04-12"), TimeOut =DateTime.Parse("2012-04-18"), Destination =1},
                new TrackingRecord { EmployeeId= 2, TrackNum =1, StoreId =2, TimeIn= DateTime.Parse("2016-03-22"), TimeOut =DateTime.Parse("2016-03-28"), Destination =2},
            };
            context.TrackingRecords.AddRange(tracks);
            context.SaveChanges();
            
            
        }



    }
}
