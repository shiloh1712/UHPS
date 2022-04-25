#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Shipments
{
    public class DetailsModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public DetailsModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Package Package { get; set; }
        public IList<TrackingRecord> TrackingRecord { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Package = await _context.Packages
                .Include(p => p.Destination)
                .Include(p => p.Receiver)
                .Include(p => p.Sender)
                .Include(p=>p.Type).FirstOrDefaultAsync(m => m.Id == id);

            if (Package == null)
            {
                return NotFound();
            }
            if (id != null)
            {
                TrackingRecord = await _context.TrackingRecords
                .Include(t => t.Address)
                .Include(t => t.Employee)
                .Include(t => t.Package)
                .Include(t => t.Store).Where(f => f.TrackNum == id).ToListAsync();
            }
            else
            {
                TrackingRecord = await _context.TrackingRecords
                    .Include(t => t.Address)
                    .Include(t => t.Employee)
                    .Include(t => t.Package)
                    .Include(t => t.Store).ToListAsync();
            }

            return Page();
        }
    }
}
