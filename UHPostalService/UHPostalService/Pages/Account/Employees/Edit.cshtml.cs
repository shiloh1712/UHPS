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
namespace UHPostalService.Pages.Account.Employees
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin,Supervisor")]

    public class EditModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public EditModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await _context.Employees
                .Include(e => e.Address)
                .Include(e => e.Store).FirstOrDefaultAsync(m => m.Id == id);

            if (Employee == null)
            {
                return NotFound();
            }
            ViewData["AddressID"] = new SelectList(_context.Addresses, "Id", "StreetAddress");
            ViewData["StoreID"] = new SelectList(_context.Stores, "Id", "Address.StreetAddress");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Employee.Address = _context.Addresses.Where(e => e.Id == Employee.AddressID).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return Page();
            }


            _context.Attach(Employee).State = EntityState.Modified;

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

