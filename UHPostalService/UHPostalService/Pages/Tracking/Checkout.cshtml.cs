#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Tracking
{
    public class CheckoutModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public CheckoutModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string TrNums { get; set; }
        [BindProperty]
        public int nextstop { get; set; }
        public IActionResult OnGet()
        {
            string query = "SELECT Addresses.ID as ID, StreetAddress + City as Text  FROM Addresses, Stores WHERE Stores.AddressID = Addresses.ID";
            ViewData["Destination"] = new SelectList(_context.Addresses.FromSqlRaw(query).ToList(), "Id", "Text");
            var testlist = new SelectList(_context.Employees.Where(e => (e.Role == Role.Supervisor || e.Role == Role.Admin)), "Id", "Name");
            
            return Page();
        }
        
        /*[BindProperty]
        public TrackingRecord TrackingRecord { get; set; }
        */
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //return Page();
            }
            int num = Int32.Parse(TrNums);
            //var claims = ClaimsPrincipal.Current.Identities.FirstOrDefault().Claims.ToList();
            //string employeeid = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
            //var principal = System.Security.Claims.ClaimsPrincipal.Current;
            //string fullname2 = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            int employee = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);

            TrackingRecord newrecord = new TrackingRecord
            {
                TrackNum = num,
                EmployeeId = employee,
                StoreId = store
            };

            _context.TrackingRecords.Add(newrecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
