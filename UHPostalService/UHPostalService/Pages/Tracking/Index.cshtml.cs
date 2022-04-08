﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Tracking
{
    public class IndexModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public IndexModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public int SearchTracking { get; set; }
        public IList<TrackingRecord> TrackingRecord { get;set; }

        public async Task OnGetAsync(int? SearchTracking)
        {
            if (SearchTracking != null)
            {
                TrackingRecord = await _context.TrackingRecords
                .Include(t => t.Address)
                .Include(t => t.Employee)
                .Include(t => t.Package)
                .Include(t => t.Store).Where(f => f.TrackNum == SearchTracking).ToListAsync();
            }
            else
            {
                TrackingRecord = await _context.TrackingRecords
                    .Include(t => t.Address)
                    .Include(t => t.Employee)
                    .Include(t => t.Package)
                    .Include(t => t.Store).ToListAsync();
            }
        }
    }
}