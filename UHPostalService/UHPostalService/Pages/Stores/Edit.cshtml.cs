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
using Microsoft.AspNetCore.Authorization;

namespace UHPostalService.Pages.Stores
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]

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
            public string PhoneNumber { get; set; }
            public int ? Supervisor { get; set; }

        }
        [BindProperty]
        public InputModel Store { get; set; }
        [BindProperty]
        public Address storeAddress { get; set; }
        //[BindProperty]
        //public Address Address { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editStore = await _context.Stores.AsNoTracking()
                .Include(s => s.Address)
                .Include(s => s.Supervisor).FirstOrDefaultAsync(m => m.Id == id);

            if (editStore == null)
            {
                return NotFound();
            }
            storeAddress = editStore.Address;
            ViewData["SupID"] = new SelectList(_context.Employees.Where(e => ((e.Role == Role.Supervisor || e.Role == Role.Admin) && e.Deleted == false)), "Id", "Name");
            Store = new InputModel { Id = editStore.Id, PhoneNumber = editStore.PhoneNumber, Supervisor = editStore.SupID };

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var addr = _context.Addresses.Where(f => (f.StreetAddress == storeAddress.StreetAddress
            && f.City == storeAddress.City
            && f.State == storeAddress.State
            && f.Zipcode == storeAddress.Zipcode)).FirstOrDefault();

            if (addr == null)
            {
                _context.Addresses.Add(storeAddress);
                await _context.SaveChangesAsync();
                addr = storeAddress;
            }
            else if (addr.Deleted == true)
            {
                addr.Deleted = false;
                await _context.SaveChangesAsync();


            }

            var editStore = _context.Stores.FirstOrDefault(s => s.Id == Store.Id);
            if (editStore == null)
                return NotFound();
            editStore.PhoneNumber = Store.PhoneNumber;
            editStore.SupID = Store.Supervisor;
            editStore.AddressID = addr.Id;

            _context.Update(editStore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(Store.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }
    }
}
