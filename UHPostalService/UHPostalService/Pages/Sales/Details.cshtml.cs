#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Sales
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor")]
    public class DetailsModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public DetailsModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Sale Sale { get; set; }

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

            Sale = await _context.Sales.Include(s=>s.Buyer)
                .Include(s => s.Product).FirstOrDefaultAsync(m => m.ID == id);

            if (Sale == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
