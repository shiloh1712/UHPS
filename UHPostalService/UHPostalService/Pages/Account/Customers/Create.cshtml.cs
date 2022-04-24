#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public class InputModel
        {
            [Required]
            public string Name { get; set; }
            public string? PhoneNumber { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        };
        [BindProperty]
        public InputModel Customer { get; set; }
        [BindProperty]
        public Address Address { get; set; }
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            returnUrl ??= Url.Content("~/");
            var user = _context.Customers.Where(f => f.Email == Customer.Email).FirstOrDefault();
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, user.Email + " alrready exists");
                return Page();
            }
            if (Customer.PhoneNumber != null) {
                var user2 = _context.Customers.Where(f => f.PhoneNumber == Customer.PhoneNumber).FirstOrDefault();
                if (user2 != null)
                {
                    ModelState.AddModelError(string.Empty, user2.PhoneNumber + " already exists");
                    return Page();
                }
            }
            var addr = _context.Addresses.Where(f => (f.StreetAddress == Address.StreetAddress 
            && f.City == Address.City
            && f.State == Address.State
            && f.Zipcode == Address.Zipcode)).FirstOrDefault();
            
            if(addr == null)
            {
                _context.Addresses.Add(Address);
                await _context.SaveChangesAsync();
                addr = Address;

            }
            else if (addr.Deleted == true)
            {
                addr.Deleted = false;
                await _context.SaveChangesAsync();

            }
            Customer newcust = new Models.Customer { Address = addr , AddressID = addr.Id, Email = Customer.Email, Name = Customer.Name, Password = Customer.Password};
            if(Customer.PhoneNumber != null)
            {
                newcust.PhoneNumber = Customer.PhoneNumber;
            }

            var check = _context.Customers.Where(f => (f.AddressID == newcust.AddressID
            && f.Email == newcust.Email
            && f.Deleted == true)).FirstOrDefault();
            if (check != null && check.Deleted == true)
            {
                newcust = check;
                newcust.Deleted = false;

            }
            else
            {
                _context.Customers.Add(newcust);
            }
            await _context.SaveChangesAsync();
                
            HttpContext.Session.SetString("userID", newcust.Id.ToString());
            HttpContext.Session.SetString("role", "Customer");


            return RedirectToPage("../RegisterConfirmation", new { email = Customer.Email });
            
        }
    }
}
