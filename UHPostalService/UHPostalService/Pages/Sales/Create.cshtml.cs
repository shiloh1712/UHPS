#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Sales
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor")]
    public class CreateModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public class InputModel {
            public int Quantity { get; set; }
            public int ProductID { get; set; }
            public string? Name { get; set; }
            [DataType(DataType.EmailAddress)]
            public string ?Email { get; set; }

            };
        public CreateModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Desc");
            return Page();
        }

        [BindProperty]
        public InputModel Sale { get; set; }
        /*[BindProperty]
        public Customer Customer { get; set; }*/


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            if (!ModelState.IsValid )
            {
                return this.OnGet();

            }
            if (Sale.Email == null && Sale.Name != null)
            {
                ModelState.AddModelError(string.Empty, "Name can't be null if email is present");
                return OnGet();
            }
            DateTime temp = DateTime.Now;
            //Sale.PurchaseDate = temp;
            //Sale.Total = 0;
            var prod = _context.Products.Where(f => (f.Id == Sale.ProductID)).FirstOrDefault();
            Customer cust = null;
            //sale email field is not empty
            if (Sale.Email != null){
                cust = _context.Customers.Where(f => (f.Email == Sale.Email)).FirstOrDefault();
                if (cust == null)
                {
                    cust = new Customer { Name = Sale.Name, Email = Sale.Email };
                    _context.Customers.Add(cust);
                    await _context.SaveChangesAsync();
                }
            }
            if (Sale.Quantity > prod.Stock || Sale.Quantity <= 0)
            {
                ModelState.AddModelError(string.Empty, "Quantity invalid. There is " + prod.Stock + " of this item left");
                return OnGet();
            }
            //Sale.Product = prod;
            Sale newsale;
            if (cust != null)
                newsale = new Models.Sale { PurchaseDate = DateTime.Now, Product = prod, ProductID = Sale.ProductID, Quantity = Sale.Quantity, Total = 0, BuyerID = cust.Id };
            else
                newsale = new Models.Sale { PurchaseDate = DateTime.Now, Product = prod, ProductID = Sale.ProductID, Quantity = Sale.Quantity, Total = 0};


            _context.Sales.Add(newsale);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
