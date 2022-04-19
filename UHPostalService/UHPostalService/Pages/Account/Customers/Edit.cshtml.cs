#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Account.Customers
{
    public class EditModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public EditModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public class InputModel
        {
            public int Id { get; set; }
            public string StreetAddress { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zipcode { get; set; }
        }
        [BindProperty]
        public InputModel modifiedPkg { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer editpkg = await _context.Customers.AsNoTracking()
               .Include(p => p.Address).FirstOrDefaultAsync(m => m.Id == id);

            if (editpkg == null)
            {
                return NotFound();
            }

            //ViewData["Address.City"] = new SelectList(_context.Addresses, "Id", "City");
            Address = _context.Addresses.Where(a => a.Id == editpkg.AddressID).FirstOrDefault();
            modifiedPkg = new InputModel { StreetAddress = editpkg.StreetAddress, City = editpkg.City, State = editpkg.State, Zipcode = editpkg.Zipcode};
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public Address Address { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Customer pkgToUpdate = _context.Customers
                .Include(p => p.AddressID).Where(p => p.Id == modifiedPkg.Id).FirstOrDefault();
            if (pkgToUpdate == null)
                return NotFound();

            _context.Attach(Customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            if (!Address.Equals(pkgToUpdate.AddressID))
            {
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
                pkgToUpdate.AddressID = addr.Id;
            }
            pkgToUpdate.Copy(modifiedPkg.StreetAddress, modifiedPkg.City, modifiedPkg.State, modifiedPkg.Zipcode);

            _context.Attach(pkgToUpdate).State = EntityState.Modified;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}