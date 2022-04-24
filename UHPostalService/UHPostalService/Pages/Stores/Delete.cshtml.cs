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

namespace UHPostalService.Pages.Stores
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]

    public class DeleteModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public DeleteModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Store Store { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Store = await _context.Stores
                .Include(s => s.Address)
                .Include(s => s.Supervisor).FirstOrDefaultAsync(m => m.Id == id);

            if (Store == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Store = await _context.Stores.FindAsync(id);

            if (Store != null)
            {
                //_context.Stores.Remove(Store);
                Store.Deleted = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
