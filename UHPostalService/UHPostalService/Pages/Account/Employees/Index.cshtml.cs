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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace UHPostalService.Pages.Account.Employees
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
        public string StoreSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Employee> Employee { get;set; }
        public int test { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString, int filterby)
        {
            // using System;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            EmailSort = sortOrder == "Date" ? "date_desc" : "Date";
            StoreSort = sortOrder == "Store" ? "store_desc" : "Store";
            test = filterby;
            CurrentFilter = searchString;
            IQueryable<Employee> EmployeeIdent = from s in _context.Employees
                                                 select s;
            /*IQueryable<Store> AddressIdent = from s in _context.Addresses
                                           select s;*/

            if (!String.IsNullOrEmpty(searchString) && test == 2)
            {
                EmployeeIdent = EmployeeIdent.Where(s => s.Email.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }
            if (!String.IsNullOrEmpty(searchString) && test == 1)
            {
                EmployeeIdent = EmployeeIdent.Where(s => s.Name.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }
            if (!String.IsNullOrEmpty(searchString) && test == 3)
            {
                EmployeeIdent = EmployeeIdent.Where(s => s.PhoneNumber.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }
            /*if (!String.IsNullOrEmpty(searchString) && test == 4)
            {
                EmployeeIdent = EmployeeIdent.Where(s => s.StoreID.Equals((int)searchString)

                                       );
            }*/

            //if (!String.IsNullOrEmpty(searchString) && test == 4)
            //{
            //    EmployeeIdent = EmployeeIdent.Where(s => s.StoreID.Contains(searchString)

            //                           /*|| s.FirstMidName.Contains(searchString)*/);
            //}

            //if (!String.IsNullOrEmpty(searchString) && test == 5)
            //{
            //    EmployeeIdent = EmployeeIdent.Where(s => s.Role.Contains(searchString)

            //                           /*|| s.FirstMidName.Contains(searchString)*/);
            //}

            switch (sortOrder)
            {
                case "name_desc":
                    EmployeeIdent = EmployeeIdent.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    EmployeeIdent = EmployeeIdent.OrderBy(s => s.Email);
                    break;
                case "Store":
                    EmployeeIdent = EmployeeIdent.OrderBy(s => s.StoreID);
                    break;
                case "store_desc":
                    EmployeeIdent = EmployeeIdent.OrderByDescending(s => s.StoreID);
                    break;
                case "date_desc":
                    EmployeeIdent = EmployeeIdent.OrderByDescending(s => s.Email);
                    break;
                default:
                    EmployeeIdent = EmployeeIdent.OrderBy(s => s.Name);
                    break;
            }
            //int? store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            //int ident = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Id")).Value);
            //var nme = HttpContext.User.Identity.Name;
            var flop = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Employee use = _context.Employees.Where(w => w.Id == flop).FirstOrDefault();
            var currstore = use.StoreID;

            if(User.IsInRole("Supervisor") && currstore !=null)
            {
                EmployeeIdent = EmployeeIdent.Where(s => s.StoreID == currstore || s.StoreID==null);
            }
            else if (currstore != null && User.IsInRole("Employee"))
            {
                EmployeeIdent = EmployeeIdent.Where(s=>s.StoreID == currstore);
            }
            else if(currstore == null && !User.IsInRole("Admin"))
            {
                EmployeeIdent = EmployeeIdent.Where(s => s.Id == flop);
            }

            Employee = await EmployeeIdent
                .Include(s=>s.Address)
                .Include(s=>s.Store)
                .ToListAsync();
        }
    }
}
