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

namespace UHPostalService.Pages.Tracking
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor")]

    public class DetailsModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public DetailsModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public TrackingRecord TrackingRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TrackingRecord = await _context.TrackingRecords
                .Include(t => t.Address)
                .Include(t => t.Employee)
                .Include(t => t.Package)
                .Include(t => t.Store).FirstOrDefaultAsync(m => m.Id == id);

            if (TrackingRecord == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}