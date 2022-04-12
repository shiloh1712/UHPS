#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Stores
{
    public class CreateModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public CreateModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public class InputModel
        {
            public string PhoneNumber { get; set; }
            public int SupID { get; set; }

        };
        public IActionResult OnGet()
        {
        ViewData["AddressID"] = new SelectList(_context.Addresses, "Id", "Id");
        ViewData["SupID"] = new SelectList(_context.Employees.Where(e=>(e.Role == Role.Supervisor || e.Role == Role.Admin)), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public InputModel Store { get; set; }
        [BindProperty]
        public Address Address { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            

            var addr = _context.Addresses.Where(f => (f.StreetAddress == Address.StreetAddress
            && f.City == Address.City
            && f.State == Address.State
            && f.Zipcode == Address.Zipcode)).FirstOrDefault();

            var pnum = _context.Stores.Where(f => (f.PhoneNumber == Store.PhoneNumber)).FirstOrDefault();
            if(pnum != null)
            {
                ModelState.AddModelError(string.Empty, pnum.PhoneNumber + " already exists");
                return Page();
            }

            if (addr == null)
            {
                _context.Addresses.Add(Address);
                await _context.SaveChangesAsync();
                addr = Address;
                
            }

            Store newstore = new Models.Store { PhoneNumber = Store.PhoneNumber, AddressID = addr.Id, Address=addr, SupID = Store.SupID };
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                _context.Stores.Add(newstore);
                await _context.SaveChangesAsync();
            
                return RedirectToPage("./Index");
            }
        }
    }
}
