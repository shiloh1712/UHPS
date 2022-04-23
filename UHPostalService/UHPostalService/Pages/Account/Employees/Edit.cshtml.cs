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

namespace UHPostalService.Pages.Employees
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
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            //store working at: initially not assigned a store
            public int StoreID { get; set; }
        };

        [BindProperty]
        public InputModel Employee { get; set; }
        [BindProperty]
        public Address Address { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var editEmployee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            /*Employee = await _context.Employees
                .Include(e => e.Address)
                .Include(e => e.Store).FirstOrDefaultAsync(m => m.Id == id);
            */
            if (editEmployee == null)
            {
                return NotFound();
            }

            ViewData["StoreID"] = new SelectList(_context.Stores, "Id", "Id");

            Address = _context.Addresses.FirstOrDefault(a => a.Id == editEmployee.AddressID);
            Employee = new InputModel { Id = editEmployee.Id, Email = editEmployee.Email, Name = editEmployee.Name, Password = "", PhoneNumber = editEmployee.PhoneNumber };
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
            var editEmployee = await _context.Employees.FirstOrDefaultAsync(m => m.Id == Employee.Id);
            if (editEmployee == null)
            {
                return NotFound();
            }
            var existedEmail = _context.Employees.Where(c => c.Email == Employee.Email).FirstOrDefault();
            if (existedEmail != null && existedEmail.Id != editEmployee.Id)
            {
                ModelState.AddModelError(String.Empty, existedEmail.Email + " belongs to another employee");
                return Page();
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
            editEmployee.Name = Employee.Name;
            editEmployee.PhoneNumber = Employee.PhoneNumber;
            editEmployee.Email = Employee.Email;
            editEmployee.Password = Employee.Password;
            editEmployee.AddressID = addr.Id;
            editEmployee.StoreID = Employee.StoreID;
            _context.Attach(editEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.Id))
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

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
