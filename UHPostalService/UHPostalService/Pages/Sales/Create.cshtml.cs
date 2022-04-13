#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Sales
{
    public class CreateModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public class InputModel {
            public int Quantity { get; set; }
            public int ProductID { get; set; }

            };
        public CreateModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Desc");
            return Page();
        }

        [BindProperty]
        public InputModel Sale { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            DateTime temp = DateTime.Now;
            //Sale.PurchaseDate = temp;
            //Sale.Total = 0;
            var prod = _context.Products.Where(f => (f.Id == Sale.ProductID)).FirstOrDefault();

            //Sale.Product = prod;
            Sale NewSale = new Models.Sale { PurchaseDate = DateTime.Now, Product = prod, ProductID = Sale.ProductID, Quantity = Sale.Quantity, Total = 0 };
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Sales.Add(NewSale);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
