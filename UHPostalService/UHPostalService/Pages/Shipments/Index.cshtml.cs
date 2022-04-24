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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;



namespace UHPostalService.Pages.Shipments
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor, Customer")]
    public class IndexModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public IndexModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Status> StatusOptions { get; set; }

        public IList<Package> Package { get;set; }
        public string StatusSort { get; set; }
        public string NameSort { get; set; }
        public string ExpressSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public int test { get; set; }
        public bool expr { get; set; }

        public async Task<IActionResult> OnGetAsync(string sortOrder, string searchString, int filterby, bool exp)
        {
            if (User.IsInRole("Employee"))
            {
                int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
                if (store == 0)
                {
                    return RedirectToPage("/Account/AccessDenied");
                }
            }
            /*StatusOptions = 
            StatusFilterOptions = _context.Packages.Select(p =>
                                  new SelectListItem
                                  {
                                      Value = p.Status.ToString(),
                                      Text = p.Status.ToString()
                                  }).Distinct().ToList();
            ViewData["StatusOptions"] = StatusFilterOptions;*/
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            StatusSort = sortOrder == "Date" ? "date_desc" : "Date";
            ExpressSort = sortOrder == "Express" ? "express_desc" : "Express";

            CurrentFilter = searchString;
            //expr = exp;
            IQueryable<Package> PackageIdent = from s in _context.Packages
                                               select s;
            int stat =0;
            test = filterby;
            /*if(test == 6)
            {
                switch (searchString)
                {
                    case "In Store":
                        stat = 0;
                        break;
                    case "In Transit":
                        stat = 1;
                        break;
                    case "Out for Delivery":
                        stat = 2;
                        break;
                    case "Delivered":
                        stat = 3;
                        break;
                    case "Returned":
                        stat = 4;
                        break;
                    case "Lost":
                        stat = 5;
                        break;
                    default:
                        searchString = "";
                        ModelState.AddModelError(string.Empty, "Invalid Status");

                        break;

                }
            }*/
            int tempo = stat;
            if (!String.IsNullOrEmpty(searchString) && test == 2)
            {
                PackageIdent = PackageIdent.Where(s => s.Destination.State.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }
            if (!String.IsNullOrEmpty(searchString) && test == 1)
            {
                PackageIdent = PackageIdent.Where(s => s.Destination.Zipcode.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }
            if (!String.IsNullOrEmpty(searchString) && test == 3)
            {
                PackageIdent = PackageIdent.Where(s => s.Destination.City.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }
            if (!String.IsNullOrEmpty(searchString) && test == 4)
            {
                PackageIdent = PackageIdent.Where(s => s.Sender.Name.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }
            if (!String.IsNullOrEmpty(searchString) && test == 5)
            {
                PackageIdent = PackageIdent.Where(s => s.Receiver.Name.Contains(searchString)

                                       /*|| s.FirstMidName.Contains(searchString)*/);
            }
            /*if (!String.IsNullOrEmpty(searchString) && test == 6)
            {
                PackageIdent = PackageIdent.Where(s => s.Status.Equals(searchString));
                                       
            }*/
            if (exp == true)
            {
                PackageIdent = PackageIdent.Where(s => s.Express == true);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    PackageIdent = PackageIdent.OrderByDescending(s => s.Status);
                    break;
                case "Date":
                    PackageIdent = PackageIdent.OrderBy(s => s.Sender.Name);
                    break;
                case "date_desc":
                    PackageIdent = PackageIdent.OrderByDescending(s => s.Sender.Name);
                    break;
                case "Express":
                    PackageIdent = PackageIdent.OrderBy(s => s.Express);
                    break;
                case "express_desc":
                    PackageIdent = PackageIdent.OrderByDescending(s => s.Express);
                    break;
                default:
                    PackageIdent = PackageIdent.OrderBy(s => s.Status);
                    break;
            }
            int ident = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (User.IsInRole("Customer"))
            {
                PackageIdent = PackageIdent.Where(s => s.SenderID == ident || s.ReceiverID == ident);
            }

            Package = await PackageIdent
                .Include(s => s.Sender).Include(s => s.Receiver).Include(s=>s.Destination).ToListAsync();
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
            return Page();
        }
    }
}
