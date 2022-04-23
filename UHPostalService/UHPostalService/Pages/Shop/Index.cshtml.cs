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
        public string StockSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public float from { get; set; }
        public float to { get; set; }
        public float def { get; set; }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString, float fr, float mx)
        {
            // using System;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            PriceSort = sortOrder == "Date" ? "date_desc" : "Date";
            StockSort = sortOrder == "Stock" ? "stock_desc" : "Stock";

            CurrentFilter = searchString;
            from = fr;
            to = mx;

            IQueryable<Product> ProductIdent = from s in _context.Products
                                           select s;
            /*IQueryable<Store> AddressIdent = from s in _context.Addresses
                                           select s;*/

            if (!String.IsNullOrEmpty(searchString))
            {
                ProductIdent = ProductIdent.Where(s => s.Desc.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }
            if(to != def)
            {
                ProductIdent = ProductIdent.Where(s => s.UnitCost >= from && s.UnitCost <= to);
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
                case "Stock":
                    ProductIdent = ProductIdent.OrderBy(s => s.Stock);
                    break;
                case "stock_desc":
                    ProductIdent= ProductIdent.OrderByDescending(s => s.Stock);
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
