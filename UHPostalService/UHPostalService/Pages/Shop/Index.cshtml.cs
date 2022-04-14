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

namespace UHPostalService.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public IndexModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public string PriceSort { get; set; }
        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public IList<Product> Product { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            // using System;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            PriceSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentFilter = searchString;
            IQueryable<Product> ProductIdent = from s in _context.Products
                                           select s;
            /*IQueryable<Store> AddressIdent = from s in _context.Addresses
                                           select s;*/

            if (!String.IsNullOrEmpty(searchString))
            {
                ProductIdent = ProductIdent.Where(s => s.Desc.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    ProductIdent = ProductIdent.OrderByDescending(s => s.Desc);
                    break;
                case "Date":
                    ProductIdent = ProductIdent.OrderBy(s => s.UnitCost);
                    break;
                case "date_desc":
                    ProductIdent = ProductIdent.OrderByDescending(s => s.UnitCost);
                    break;
                default:
                    ProductIdent = ProductIdent.OrderBy(s => s.Desc);
                    break;
            }

            Product = await ProductIdent.
                AsNoTracking().ToListAsync();
        }
    }
}
