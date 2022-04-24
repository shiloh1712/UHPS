#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UHPostalService.Data;
using UHPostalService.Models;
using Microsoft.AspNetCore.Authorization;


namespace UHPostalService.Pages.Account.Employees
{    public class CreateModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;
        public class InputModel
        {
            [Required]
            public string Name { get; set; }
            [DataType(DataType.PhoneNumber)]
            [Required]
            public string PhoneNumber { get; set; }
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [DataType(DataType.Password)]
            [Required]
            public string Password { get; set; }
            //store working at: initially not assigned a store
        };
        /*
        [BindProperty]
        public Employee Employee { get; set; }
        */
        [BindProperty]
        public Address Address { get; set; }
        [BindProperty]
        public InputModel Employee { get; set; }
        public string ReturnUrl { get; set; }
        public CreateModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
            //Employee.Address = _context.Addresses.FirstOrDefault();
        }

        public IActionResult OnGet()
        {
            ViewData["AddressID"] = new SelectList(_context.Addresses, "Id", "Id");
            ViewData["StoreID"] = new SelectList(_context.Stores, "Id", "Id");
            return Page();
        }

       


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            ReturnUrl ??= Url.Content("~/");
            var user = _context.Employees.Where(f => f.Email == Employee.Email).FirstOrDefault();
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, user.Email + " already exists");
                return Page();
            }
            var user2 = _context.Employees.Where(f => f.PhoneNumber == Employee.PhoneNumber).FirstOrDefault();
            if (user2 != null)
            {
                ModelState.AddModelError(string.Empty, user2.PhoneNumber + " already exists");
                return Page();
            }

            var addr = _context.Addresses.Where(f => (f.StreetAddress == Address.StreetAddress
            && f.City == Address.City
            && f.State == Address.State
            && f.Zipcode == Address.Zipcode)).FirstOrDefault();

            if (addr == null)
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
            Employee newemp = new Models.Employee { Name = Employee.Name, PhoneNumber = Employee.PhoneNumber, Email = Employee.Email, Password = Employee.Password, AddressID = addr.Id, Role = Role.Employee };
            //Employee.AddressID = Address.Id;
            var check = _context.Employees.Where(f => (f.AddressID == newemp.AddressID
            && f.Email == newemp.Email
            && f.PhoneNumber == newemp.PhoneNumber
            && f.Deleted == true)).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                if (check != null)
                {
                    check.Deleted = false;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Employees.Add(newemp);
                    await _context.SaveChangesAsync();
                }

                HttpContext.Session.SetString("userID", newemp.Id.ToString());
                HttpContext.Session.SetString("role", "Employee");


                return RedirectToPage("../RegisterConfirmation", new { email = Employee.Email });

                //return RedirectToPage("./Index");
            }
        }
    }
}

