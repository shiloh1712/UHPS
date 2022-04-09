#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Tracking
{
    public class CreateModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public CreateModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Destination"] = new SelectList(_context.Addresses, "Id", "Id");
        //ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
        ViewData["TrackNum"] = new SelectList(_context.Packages, "Id", "Id");
        ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public TrackingRecord TrackingRecord { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //return Page();
            }
            TrackingRecord.Destination = null; 

            _context.TrackingRecords.Add(TrackingRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
