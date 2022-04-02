#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Account.Customers
{
    public class CreateModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public CreateModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        //public Address Address { get; set; }
        public string ReturnUrl { get; set; }
        
        
        public async Task OnGetAsync(string returnURL = null)
        {
            //customize address dropdown menu
            var AvailAddr = _context.Addresses.Select(x => new
            {
                Id = x.Id,
                FullAddress = x.StreetAddress + " " + x.City
            });
        ViewData["AddressID"] = new SelectList(AvailAddr, "Id", "FullAddress");
            ReturnUrl = returnURL;
            //return Page();
        }

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            var user = _context.Customers.Where(f => f.Email == Customer.Email).FirstOrDefault();
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, user.Email + " Alrready exists");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            else
            {

                _context.Customers.Add(Customer);
                await _context.SaveChangesAsync();
                
                HttpContext.Session.SetString("userID", Customer.Id.ToString());
                HttpContext.Session.SetString("role", "Customer");


                return RedirectToPage("RegisterConfirmation", new { email = Customer.Email });
            }
        }
    }
}
