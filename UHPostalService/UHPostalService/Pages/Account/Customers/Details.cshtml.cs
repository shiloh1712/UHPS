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

namespace UHPostalService.Pages.Account.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public DetailsModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; }

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

            Customer = await _context.Customers
                .Include(c => c.Address).FirstOrDefaultAsync(m => m.Id == id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
