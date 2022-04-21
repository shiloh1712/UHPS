using Microsoft.EntityFrameworkCore;
using UHPostalService.Models;

namespace UHPostalService.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {

            // Look for any customer./*
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
                new Address { StreetAddress = "4166 Yorkie Lane", City = "Cypress", State = "TX", Zipcode = "77433" },
                new Address { StreetAddress = "5516 Tori Lane", City = "Ogden", State = "UT", Zipcode = "84401" },
                new Address { StreetAddress = "4392 Isaacs Creek Road", City = "Decatur", State = "IL", Zipcode = "62522" },
                new Address { StreetAddress = "2411 Brown Avenue", City = "Cross Hill", State = "SC", Zipcode = "29332" },
                new Address { StreetAddress = "4519 Rinehart Road", City = "Sunrise", State = "FL", Zipcode = "33323" },
                new Address { StreetAddress = "4748 Eastland Avenue", City = "Ridgeland", State = "MS", Zipcode = "39157" },
                new Address { StreetAddress = "1110 Tea Berry Lane", City = "Crivitz", State = "WI", Zipcode = "54114" },
                new Address { StreetAddress = "176 Lochmere Lane", City = "Manchester", State = "CT", Zipcode = "06040" },
                new Address { StreetAddress = "369 Armbrester Drive", City = "El Segundo", State = "CA", Zipcode = "90245" },
                new Address { StreetAddress = "2581 Hiney Road", City = "Las Vegas", State = "NV", Zipcode = "89102" },
                new Address { StreetAddress = "1155 Sycamore Circle", City = "Dallas", State = "TX", Zipcode = "75204" },
                new Address { StreetAddress = "337 Hide A Way Road", City = "Destin", State = "FL", Zipcode = "32789" },
                new Address { StreetAddress = "1740 Pyramid Valley Road", City = "Fort Madison", State = "IA", Zipcode = "52627" },
                new Address { StreetAddress = "1216 Poplar Avenue", City = "San Diego", State = "CA", Zipcode = "92105" },
                new Address { StreetAddress = "2218 Seneca Drive", City = "Salem", State = "OR", Zipcode = "97301" },
                new Address { StreetAddress = "1438 Kenwood Place", City = "Tamarac", State = "FL", Zipcode = "33321" },
                new Address { StreetAddress = "1588 Whiteman Street", City = "Somers Point", State = "NJ", Zipcode = "08244" },
                new Address { StreetAddress = "1752 Hickory Heights Drive", City = "Linthicum", State = "MD", Zipcode = "21090" },


            };
            context.Addresses.AddRange(addresses);
            context.SaveChanges();

            var custs = new Customer[]
            {
                new Customer { Name= "Bader", PhoneNumber ="1234567890", Email ="bader@email.com", Password ="Bader", AddressID=4},
                new Customer { Name= "Morrison", PhoneNumber ="0987654321", Email ="morrison@email.com", Password ="Morrison"},
                new Customer { Name= "Maverick", PhoneNumber ="5554562148", Email ="helloyes@email.com", Password ="Mav", AddressID=3},
                new Customer { Name= "Boris", PhoneNumber ="4567891345", Email ="jimbo@email.com", Password ="bori", AddressID=2},
                new Customer { Name= "Jon", PhoneNumber ="4531687169", Email ="yaboy@email.com", Password ="jonny", AddressID=4},
                new Customer { Name= "Susan", PhoneNumber ="1984561023", Email ="sus@email.com", Password ="susamongus"},
                new Customer { Name= "Jenny", PhoneNumber ="4561238951", Email ="jay12@email.com", Password ="jay"},
                new Customer { Name= "Stephen", PhoneNumber ="4678923541", Email ="scargat@email.com", Password ="scar", AddressID=6},
                new Customer { Name= "Emily", PhoneNumber ="9745612587", Email ="emem@email.com", Password ="emem", AddressID=7},
            };
            context.Customers.AddRange(custs);
            context.SaveChanges();

            var emps = new Employee[]
            {
                new Employee { Name= "Son", PhoneNumber ="1234567890", Email ="son@uhps.com", Password ="son", AddressID =4, StoreID = 1},
                new Employee { Name= "Josh", PhoneNumber ="2345678901", Email ="josh@uhps.com", Password ="josh", AddressID =5, StoreID = 1, Role=Role.Supervisor},
                new Employee { Name= "Danny", PhoneNumber ="4561239856", Email ="dan@uhps.com", Password ="dan", AddressID =6},
                new Employee { Name= "Dahlia", PhoneNumber ="5687451209", Email ="dah@uhps.com", Password ="dah", AddressID =7},
                new Employee { Name= "Gia", PhoneNumber ="6124578912", Email ="gia@uhps.com", Password ="gia", AddressID =8},
                new Employee { Name= "Abdullah", PhoneNumber ="7895216489", Email ="abd@uhps.com", Password ="abd", AddressID =9},

            };
            context.Employees.AddRange(emps);
            context.SaveChanges();

            var ships = new ShipmentClass[]
            {
                new ShipmentClass { Desc= "Letter", MaxLength =11.50f, MaxWidth =6.20f, MaxHeight= 0.25f, GroundCost =1.50f, ExpressCost =9.00f},
                new ShipmentClass { Desc= "Large Envelope", MaxLength =15.00f, MaxWidth =12.00f, MaxHeight= 0.75f, GroundCost =2.00f, ExpressCost =10.00f},
                new ShipmentClass { Desc= "Box", MaxLength =90.00f, MaxWidth =90.00f, MaxHeight= 90.00f, GroundCost =4.00f, ExpressCost =15.00f},
            };
            context.ShipmentClasses.AddRange(ships);
            context.SaveChanges();

            var packs = new Package[]
            {
                new Package { SenderID= 1, ReceiverID =2, Description="luxury", AddressID =3, ShipCost =51F, Width=1.2F, Depth=2.3F, Height=4.5F, Weight=3.4F, ClassID=3, Express=true},
                new Package { SenderID= 2, ReceiverID =1,Description="Pillow", AddressID =4, ShipCost =2.25F, Width=4.5f, Depth=2.3F, Height=0.15F, Weight=1.5f, ClassID=1},
                new Package { SenderID= 3, ReceiverID =1,Description="wedding ring", AddressID =5, ShipCost =27.0F, Width=12.2F, Depth=2.3F, Height=0.5F, Weight=3.0f, ClassID=2, Express=true},
                new Package { SenderID= 4, ReceiverID =3,Description="Empty Box", AddressID =6, ShipCost =2.8F, Width=1.1F, Depth=2.3F, Height=0.4F, Weight=1.4f, ClassID=2},
                new Package { SenderID= 5, ReceiverID =4,Description="chocolate", AddressID =7, ShipCost =49.5F, Width=1.1F, Depth=2.2F, Height=4.4F, Weight=3.3F, ClassID=3, Express=true},
                new Package { SenderID= 6, ReceiverID =5,Description="cash", AddressID =8, ShipCost =5.4F, Width=1.3F, Depth=2.4F, Height=.15F, Weight=3.6F, ClassID=1},
                new Package { SenderID= 7, ReceiverID =6,Description="painting", AddressID =9, ShipCost =3.0F, Width=1.2F, Depth=2.4F, Height=0.5F, Weight=1.5f, ClassID=2},
            };
            context.Packages.AddRange(packs);
            context.SaveChanges();

            var prods = new Product[]
            {
                new Product { Desc= "Stamp", UnitCost =5.99F, Stock =100},
                new Product { Desc= "Envelope", UnitCost =10.99F, Stock =50},
                new Product { Desc= "Box", UnitCost =4.99F, Stock =150},
                new Product { Desc= "Tape", UnitCost =3.99F, Stock =65},
                new Product { Desc= "Pens", UnitCost =12.99F, Stock =45},
            };
            context.Products.AddRange(prods);
            context.SaveChanges();

            var sales = new Sale[]
            {
                new Sale { ProductID= 1, Quantity =5, PurchaseDate =DateTime.Parse("2010-09-01"), Total = 29.95f},
                new Sale { ProductID= 2, Quantity =10, PurchaseDate =DateTime.Parse("2015-07-24"), BuyerID=1, Total = 109.9f},
                new Sale { ProductID= 2, Quantity =4, PurchaseDate =DateTime.Parse("2011-08-02"), Total = 43.96f},
                new Sale { ProductID= 1, Quantity =2, PurchaseDate =DateTime.Parse("2016-09-15"), BuyerID=4,Total = 11.98f},
                new Sale { ProductID= 1, Quantity =3, PurchaseDate =DateTime.Parse("2020-11-01"), BuyerID=6, Total = 17.97f},
                new Sale { ProductID= 1, Quantity =5, PurchaseDate =DateTime.Parse("2009-09-09"), BuyerID=5,Total = 29.95f},
                new Sale { ProductID= 2, Quantity =10, PurchaseDate =DateTime.Parse("2015-07-24"), BuyerID=1, Total = 109.90f},
            };
            context.Sales.AddRange(sales);
            context.SaveChanges();

            
             var stores = new Store[]
            {
                new Store { SupID= 3, PhoneNumber ="135794680", AddressID =12},
                new Store { SupID= 2, PhoneNumber ="246813579", AddressID =13},
                new Store { SupID= 4, PhoneNumber ="9456123658", AddressID =14},
                new Store { SupID= 5, PhoneNumber ="8964512359", AddressID =15},
            };
            context.Stores.AddRange(stores);
            context.SaveChanges();

             var tracks = new TrackingRecord[]
            {
                new TrackingRecord { EmployeeId= 4, TrackNum =1, StoreId =1, TimeIn= DateTime.Parse("2012-04-12"), TimeOut =DateTime.Parse("2012-04-18"), Destination =2},
                new TrackingRecord { EmployeeId= 2, TrackNum =1, StoreId =2, TimeIn= DateTime.Parse("2016-03-22")},
                new TrackingRecord { EmployeeId= 3, TrackNum =3, StoreId =1, TimeIn= DateTime.Parse("2011-05-11"), TimeOut =DateTime.Parse("2011-06-19"), Destination =2},
                new TrackingRecord { EmployeeId= 4, TrackNum =4, StoreId =1, TimeIn= DateTime.Parse("2017-04-21")},
                new TrackingRecord { EmployeeId= 5, TrackNum =5, StoreId =1, TimeIn= DateTime.Parse("2020-07-20")},
            };
            context.TrackingRecords.AddRange(tracks);
            context.SaveChanges();
            
            
        }



    }
}
