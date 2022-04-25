#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UHPostalService.Data;
using UHPostalService.Models;
using Microsoft.AspNetCore.Authorization;
namespace UHPostalService.Pages.Shipments
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor")]

    public class CreateModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public CreateModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public class InputModel
        {
            public string? Description { get; set; }
            [DefaultValue(Status.InStore)]
            public Status Status { get; set; }
            public float Weight { get; set; }
            [DefaultValue(false)]
            public bool Express { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
            public float Depth { get; set; }
        }
        /*public IActionResult OnGet()
        {
        ViewData["AddressID"] = new SelectList(_context.Addresses, "Id", "Id");
        ViewData["ReceiverID"] = new SelectList(_context.Customers, "Id", "Id");
        ViewData["SenderID"] = new SelectList(_context.Customers, "Id", "Id");
            return Page();
        }*/

        [BindProperty]
        public InputModel Package { get; set; }
        [BindProperty]
        public Address Address { get; set; }
        [BindProperty]
        public Customer From { get; set; }
        [BindProperty]
        public Customer To { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            var addr = _context.Addresses.Where(f => (f.StreetAddress == Address.StreetAddress
            && f.City == Address.City
            && f.State == Address.State
            && f.Zipcode == Address.Zipcode)).FirstOrDefault();

            if (addr == null)
            {
                _context.Addresses.Add(Address);
                await _context.SaveChangesAsync();
                addr = Address;
            }
            else if (addr.Deleted == true)
            {
                addr.Deleted = false;
            }

            var cust = _context.Customers.Where(f => (f.Name == To.Name
            && f.PhoneNumber == To.PhoneNumber
            && f.Email == To.Email)).FirstOrDefault();

            if (cust == null)
            {
                _context.Customers.Add(To);
                await _context.SaveChangesAsync();
                cust = To;
            }
            else if(cust.Deleted == true)
            {
                cust.Deleted = false;
            }

            var cust2 = _context.Customers.Where(f => (f.Name == From.Name
            && f.PhoneNumber == From.PhoneNumber
            && f.Email == From.Email)).FirstOrDefault();

            if (cust2 == null)
            {
                _context.Customers.Add(From);
                await _context.SaveChangesAsync();
                cust2 = From;
            }
            else if(cust2.Deleted == true)
            {
                cust2.Deleted = false;
            }
            Package newPack = new Models.Package { SenderID = cust2.Id, ReceiverID = cust.Id, AddressID = addr.Id, Description = Package.Description, Weight = Package.Weight, Express = Package.Express, ShipCost = 0, Height = Package.Height, Width = Package.Width, Depth = Package.Depth, ClassID=1};
            if (!ModelState.IsValid)
            {
                //return Page();
            }
            //int trnum = _context.Packages.Select(p => p.Id).Max()+1;
            //return RedirectToPage("/Tracking/CheckIn", new { urltrnum = trnum });

            _context.Packages.Add(newPack);
            //int employee = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            

            //await _context.SaveChangesAsync();
            /*TrackingRecord trackingRecord = new TrackingRecord { EmployeeId = employee, StoreId = store, TrackNum = trnum };
            _context.TrackingRecords.Add(trackingRecord);
            */
            await _context.SaveChangesAsync();

            return RedirectToPage("/Tracking/CheckIn", new { urltrnum = newPack.Id });

            //return RedirectToPage("./Index");
        }
    }
}
