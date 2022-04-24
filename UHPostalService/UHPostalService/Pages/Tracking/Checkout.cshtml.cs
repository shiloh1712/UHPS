#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class CheckoutModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public CheckoutModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        
        [BindProperty]
        public int nextstop { get; set; }
        public async Task<IActionResult> OnGetAsync(int? trnum)
        {
            if (trnum == null)
            {
                return NotFound();
            }
            int CurrentStore = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);

            var TrackingRecord =  await _context.TrackingRecords.Where(m => m.TrackNum == trnum && m.TimeOut == null && m.StoreId == CurrentStore)
                .Include(t => t.Address)
                .Include(t => t.Employee)
                .Include(t => t.Package)
                .Include(t => t.Store).FirstOrDefaultAsync();
            
            if (TrackingRecord == null)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            var pkg = _context.Packages.Include(p => p.Destination).FirstOrDefault(p => p.Id == trnum);
            SelectListItem destination = new SelectListItem() { Value = pkg.AddressID.ToString(), Text="Destination: " + pkg.Destination.ToString()};

            var NextStops = new SelectList(_context.Stores.Where(s => s.Id != CurrentStore).Include(s=>s.Address).Select(s => new
            {
                AddressID = s.AddressID,
                Text = $"Store #{s.Id}: {s.Address.ToString()}"
            }), "AddressID", "Text").Append(destination);

            ViewData["NextStops"] = NextStops;

            return Page();
        }
        
        
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? trnum)
        {
            if (!ModelState.IsValid)
            {
                //return Page();
            }
            //int num = Int32.Parse(TrNums);
            if (trnum == null)
                return NotFound();

            int num = (int)trnum;
            //var claims = ClaimsPrincipal.Current.Identities.FirstOrDefault().Claims.ToList();
            //string employeeid = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            //var principal = System.Security.Claims.ClaimsPrincipal.Current;
            //string fullname2 = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            int employee = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);

            TrackingRecord record = _context.TrackingRecords.Where(r=>r.TrackNum==num && r.Destination == null).FirstOrDefault();
            if (record == null)
                return NotFound();
            record.Destination = nextstop;
            record.TimeOut = DateTime.Now;

            _context.Attach(record).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.TrackingRecords.Where(r => r.TrackNum == num).FirstOrDefault() == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Shipments/Details", new {id = num });
        }
    }
}
