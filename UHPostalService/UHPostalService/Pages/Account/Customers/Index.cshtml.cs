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


namespace UHPostalService.Pages.Account.Customers
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor")]

    public class IndexModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public IndexModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string EmailSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Customer> Customer { get;set; }
        public int test { get; set; }

        public async Task<IActionResult> OnGetAsync(string sortOrder, string searchString, int filterby)
        {
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            // using System;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            EmailSort = sortOrder == "Email" ? "email_desc" : "Email";
            test = filterby;
            CurrentFilter = searchString;
            IQueryable<Customer> CustomerIdent = from s in _context.Customers
                                               select s;
            /*IQueryable<Store> AddressIdent = from s in _context.Addresses
                                           select s;*/

            if (!String.IsNullOrEmpty(searchString) && test == 2)
            {
                CustomerIdent = CustomerIdent.Where(s => s.Email.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }
            if (!String.IsNullOrEmpty(searchString) && test == 1)
            {
                CustomerIdent = CustomerIdent.Where(s => s.Name.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }
            if (!String.IsNullOrEmpty(searchString) && test == 3)
            {
                CustomerIdent = CustomerIdent.Where(s => s.PhoneNumber.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    CustomerIdent = CustomerIdent.OrderByDescending(s => s.Name);
                    break;
                case "Email":
                    CustomerIdent = CustomerIdent.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    CustomerIdent = CustomerIdent.OrderByDescending(s => s.Email);
                    break;
                default:
                    CustomerIdent = CustomerIdent.OrderBy(s => s.Name);
                    break;
            }

            Customer = await CustomerIdent.
                Include(s=>s.Address).ToListAsync();
            return Page();
        }
    }
}
