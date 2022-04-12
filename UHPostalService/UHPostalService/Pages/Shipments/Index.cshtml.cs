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

namespace UHPostalService.Pages.Shipments
{
    public class IndexModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public IndexModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public string StatusSort { get; set; }
        public List<Status> StatusOptions { get; set; }

        public IList<Package> Package { get;set; }
        public string StatusFilter { get; set; }

        public async Task OnGetAsync(string sortOrder, string statusFilter)
        {
            /*StatusOptions = 
            StatusFilterOptions = _context.Packages.Select(p =>
                                  new SelectListItem
                                  {
                                      Value = p.Status.ToString(),
                                      Text = p.Status.ToString()
                                  }).Distinct().ToList();
            ViewData["StatusOptions"] = StatusFilterOptions;*/

            StatusSort = String.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            IQueryable<Package> packageIQ = from s in _context.Packages
                                             select s;
            switch (sortOrder)
            {
                case "status_desc":
                    packageIQ = packageIQ.OrderByDescending(s => s.Status);
                    break;
                default:
                    packageIQ = packageIQ.OrderBy(s => s.Status);
                    break;
            }
            
            Package = await packageIQ.AsNoTracking().Include(p => p.Destination)
                .Include(p => p.Receiver)
                .Include(p => p.Sender).ToListAsync();
            /*Package = (IList<Package>)_context.Packages.Select(x => new
            {
                Sender = x.Sender,
                Receiver = x.Receiver,
                Destination = x.Destination.StreetAddress+x.Destination.City,
                Description = x.Description,
                Status = x.Status,
                Express = x.Express,
                ShipCost = x.ShipCost

            });*/
            /*.Include(p => p.Destination)
            .Include(p => p.Receiver)
            .Include(p => p.Sender).ToListAsync();*/
        }
    }
}
