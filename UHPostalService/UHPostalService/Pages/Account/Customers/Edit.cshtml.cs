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
            public string Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            //store working at: initially not assigned a store
        };
        [BindProperty]
        public InputModel Customer { get; set; }
        [BindProperty]
        public Address cusAddress { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            if (id == null)
            {
                return NotFound();
            }

            var editCustomer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (editCustomer == null)
            {
                return NotFound();
            }

            cusAddress = _context.Addresses.FirstOrDefault(a=>a.Id==editCustomer.AddressID);
            Customer = new InputModel { Id = editCustomer.Id, Name=editCustomer.Name, PhoneNumber=editCustomer.PhoneNumber, Email=editCustomer.Email, Password=""};


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public Address Address { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var editCustomer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == Customer.Id);
            if (editCustomer == null)
                return NotFound();

            var existedEmail = _context.Customers.Where(c => c.Email == Customer.Email).FirstOrDefault();
            if (existedEmail != null && existedEmail.Id != editCustomer.Id)
            {
                ModelState.AddModelError(String.Empty, existedEmail.Email + " belongs to another customer");
                return Page();
            }
            var addr = _context.Addresses.Where(f => (f.StreetAddress == cusAddress.StreetAddress
            && f.City == cusAddress.City
            && f.State == cusAddress.State
            && f.Zipcode == cusAddress.Zipcode)).FirstOrDefault();



            if (addr == null)
            {
                _context.Addresses.Add(cusAddress);
                await _context.SaveChangesAsync();
                addr = cusAddress;

            }
            else if (addr.Deleted == true)
            {
                addr.Deleted = false;
                await _context.SaveChangesAsync();

            }

            editCustomer.Name = Customer.Name;
            editCustomer.PhoneNumber = Customer.PhoneNumber;
            editCustomer.Email = Customer.Email;
            editCustomer.Password = Customer.Password;
            editCustomer.AddressID = addr.Id;

            _context.Attach(editCustomer).State = EntityState.Modified;          


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(editCustomer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Account/Customers/Details", new {id = editCustomer.Id });
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}