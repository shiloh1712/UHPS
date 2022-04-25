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
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor")]
    public class DetailsModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public DetailsModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int cookieEmployee = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var store = User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value;
            int cookieStore = String.IsNullOrEmpty(store)? 0:Int32.Parse(store);
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
    }
}
