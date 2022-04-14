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

namespace UHPostalService.Pages.Sales
{
    public class IndexModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public IndexModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public string DateSort { get; set; }
        public string TotalSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public IList<Sale> Sale { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System;
            DateSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            TotalSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentFilter = searchString;
            IQueryable<Sale> SaleIdent = from s in _context.Sales
                                               select s;
            /*IQueryable<Store> AddressIdent = from s in _context.Addresses
                                           select s;*/

            if (!String.IsNullOrEmpty(searchString))
            {
                SaleIdent = SaleIdent.Where(s => s.Buyer.Name.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    SaleIdent = SaleIdent.OrderByDescending(s => s.PurchaseDate);
                    break;
                case "Date":
                    SaleIdent = SaleIdent.OrderBy(s => s.Total);
                    break;
                case "date_desc":
                    SaleIdent = SaleIdent.OrderByDescending(s => s.Total);
                    break;
                default:
                    SaleIdent = SaleIdent.OrderBy(s => s.PurchaseDate);
                    break;
            }
            Sale = await SaleIdent
                .Include(s => s.Product).Include(s=> s.Buyer).ToListAsync();
        }
    }
}
