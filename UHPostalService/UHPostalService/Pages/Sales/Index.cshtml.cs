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
using Microsoft.AspNetCore.Authorization;
namespace UHPostalService.Pages.Sales
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor")]

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
        [BindProperty]

        public DateTime From { get; set; }
        [BindProperty]
        public DateTime To { get; set; }

        public DateTime Default;

        public decimal? CurrTotal;
        public async Task<IActionResult> OnGetAsync(string sortOrder, string searchString, DateTime start, DateTime end)
        {
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            // using System;
            DateSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            TotalSort = sortOrder == "Date" ? "date_desc" : "Date";

            CurrentFilter = searchString;
            From = start;
            To = end;
            
            IQueryable<Sale> SaleIdent = from s in _context.Sales
                                               select s;
            /*IQueryable<Store> AddressIdent = from s in _context.Addresses
                                           select s;*/

            if (!String.IsNullOrEmpty(searchString))
            {
                SaleIdent = SaleIdent.Where(s => s.Buyer.Name.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }
            
                
                if (From != Default && To != Default)
            {
                SaleIdent = SaleIdent.Where(s => s.PurchaseDate >= From && s.PurchaseDate <= To);
            }

            CurrTotal = (decimal?)SaleIdent.Sum(s => s.Total);
            if (CurrTotal == null)
                CurrTotal = 0;
            else
                CurrTotal = decimal.Round((decimal)CurrTotal, 2);
                //CurrTotal = Math.Round(CurrTotal, 2);
            
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
            return Page();
        }
    }
}
