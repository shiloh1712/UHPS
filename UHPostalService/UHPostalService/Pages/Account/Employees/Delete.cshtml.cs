#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UHPostalService.Data;
using UHPostalService.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UHPostalService.Pages.Account.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public DeleteModel(UHPostalService.Data.ApplicationDbContext context)
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
            int cookieEmployee = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            int cookieStore = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            string cookieRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            Employee = await _context.Employees
                            .Include(e => e.Address)
                            .Include(e => e.Store)
                            .FirstOrDefaultAsync(m => m.Id == id);
            if (Employee == null)
            {
                return NotFound();
            }
            if (cookieRole == "Admin" ||
                (cookieRole == "Supervisor" && (Employee.StoreID == null || Employee.StoreID == cookieStore) && Employee.Role == Role.Employee) ||
                cookieEmployee == (int)id)
            {
                return Page();
            }
            return RedirectToPage("/Account/AccessDenied");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await _context.Employees.FindAsync(id);

            if (Employee != null)
            {
                //_context.Employees.Remove(Employee);
                Employee.Deleted = true;
                _context.Update(Employee).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
