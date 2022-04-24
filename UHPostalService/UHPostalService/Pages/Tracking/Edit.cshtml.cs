#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Tracking
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor")]

    public class EditModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public EditModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public int nextstop { get; set; }
        [BindProperty]
        public int recordId { get; set; }

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

            var TrackingRecord = await _context.TrackingRecords
                .Include(t => t.Address)
                .Include(t => t.Employee)
                .Include(t => t.Package)
                .Include(t => t.Store).FirstOrDefaultAsync(m => m.Id == id);

            if (TrackingRecord == null)
            {
                return NotFound();
            }
            recordId = (int)id;
            int CurrentStore = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);

            var pkg = _context.Packages.Include(p => p.Destination).FirstOrDefault(p => p.Id == TrackingRecord.TrackNum);
            SelectListItem destination = new SelectListItem() { Value = pkg.AddressID.ToString(), Text = "Destination: " + pkg.Destination.ToString() };

            var NextStops = new SelectList(_context.Stores.Where(s => s.Id != CurrentStore).Include(s => s.Address).Select(s => new
            {
                AddressID = s.AddressID,
                Text = $"Store #{s.Id}: {s.Address.ToString()}"
            }), "AddressID", "Text").Append(destination);

            ViewData["NextStops"] = NextStops;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            if (!ModelState.IsValid)
            {
                //return Page();
            }
            var TrackingRecord = await _context.TrackingRecords.FirstOrDefaultAsync(r => r.Id == recordId);
            TrackingRecord.Destination = nextstop;
            TrackingRecord.TimeOut = DateTime.Now;
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

            return RedirectToPage("/Shipments/Details", new {id=TrackingRecord.TrackNum});
        }

        private bool TrackingRecordExists(int id)
        {
            return _context.TrackingRecords.Any(e => e.Id == id);
        }
    }
}
