#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Stores
{
    public class IndexModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public IndexModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public string StateSort { get; set; }
        public string ZipSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public IList<Store> Store { get;set; }
        
        public IList<Address> Address { get; set; }
        public int test { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString, int filterby)
        {
            // using System;
            StateSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ZipSort = sortOrder == "Date" ? "date_desc" : "Date";
            test = filterby;
            CurrentFilter = searchString;
            IQueryable<Store> StoreIdent = from s in _context.Stores
                                             select s;
            /*IQueryable<Store> AddressIdent = from s in _context.Addresses
                                           select s;*/
            if (test == 1)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    StoreIdent = StoreIdent.Where(s => s.Address.Zipcode.Contains(searchString)

                                           /*|| s.FirstMidName.Contains(searchString)*/);
                }
            }
            if (test == 2)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    StoreIdent = StoreIdent.Where(s => s.Address.State.Contains(searchString)

                                           /*|| s.FirstMidName.Contains(searchString)*/);
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    StoreIdent = StoreIdent.OrderByDescending(s => s.Address.State);
                    break;
                case "Date":
                    StoreIdent = StoreIdent.OrderBy(s => s.Address.Zipcode);
                    break;
                case "date_desc":
                    StoreIdent = StoreIdent.OrderByDescending(s => s.Address.Zipcode);
                    break;
                default:
                    StoreIdent = StoreIdent.OrderBy(s => s.Address.State);
                    break;
            }

            Store = await StoreIdent.OrderBy(s=>s.Id).Where(s=>!s.Deleted)
                .Include(s => s.Address)
                .Include(s => s.Supervisor).ToListAsync();
            //Address = await AddressIdent.AsNoTracking().ToListAsync();
                
            
            /*Store = await _context.Stores
                .Include(s => s.Address)
                .Include(s => s.Supervisor).ToListAsync();*/
        }
    }
}
