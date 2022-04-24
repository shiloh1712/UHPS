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
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Tracking
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor")]
    public class CheckInModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;
        public CheckInModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string TrNums { get; set; }
        /*public IActionResult OnGet()
        {
            ViewData["Destination"] = new SelectList(_context.Addresses, "Id", "Id");
            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["TrackNum"] = new SelectList(_context.Packages, "Id", "Id");
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id");
            return Page();
        }
        */
        /*[BindProperty]
        public TrackingRecord TrackingRecord { get; set; }
        */
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnGetAsync(int? urltrnum)
        {
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            if (urltrnum == null)
                return Page();
            int num = (int)urltrnum;
            int employee = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var PkgExist = _context.Packages.FirstOrDefault(p=>p.Id == num);
            if (PkgExist == null)
                return NotFound();
            TrackingRecord newrecord = new TrackingRecord
            {
                TrackNum = num,
                EmployeeId = employee,
                StoreId = store,
                TimeIn = DateTime.Now
            };

            _context.TrackingRecords.Add(newrecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Shipments/Details", new { id = num });
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //return Page();
            }
            if(TrNums == null)
            {
                return Page();
            }
            int num = Int32.Parse(TrNums);
            //var claims = ClaimsPrincipal.Current.Identities.FirstOrDefault().Claims.ToList();
            //string employeeid = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            //var principal = System.Security.Claims.ClaimsPrincipal.Current;
            //string fullname2 = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await OnGetAsync(num);
            
            
            
            /*int employee = Int32.Parse(User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier).Value);
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);

            Package pkg = _context.Packages.FirstOrDefault(p=>p.Id== urltrnum);
            if (pkg == null)
                return NotFound();
            TrackingRecord newrecord = new TrackingRecord {
                TrackNum = num, EmployeeId = employee, StoreId = store
            };

            _context.TrackingRecords.Add(newrecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Shipments/Index");*/
        }

    }
}
