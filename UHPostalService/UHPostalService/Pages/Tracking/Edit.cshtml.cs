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

namespace UHPostalService.Pages.Tracking
{
    public class EditModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public EditModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["Destination"] = new SelectList(_context.Addresses, "Id", "Id");
           ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
           ViewData["TrackNum"] = new SelectList(_context.Packages, "Id", "Id");
           ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //return Page();
            }

            _context.Attach(TrackingRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackingRecordExists(TrackingRecord.Id))
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

        private bool TrackingRecordExists(int id)
        {
            return _context.TrackingRecords.Any(e => e.Id == id);
        }
    }
}
