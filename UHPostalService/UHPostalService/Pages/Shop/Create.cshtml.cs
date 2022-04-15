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
using Microsoft.AspNetCore.Authorization;
namespace UHPostalService.Pages.Shop
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor")]

    public class CreateModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public CreateModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            var prod = _context.Products.Where(f => (f.Desc == Product.Desc)).FirstOrDefault();
            if (prod == null)
            {
                _context.Products.Add(Product);
                await _context.SaveChangesAsync();
                
            }
            else if(prod.Deleted == true)
            {
                prod.Deleted = false;
                prod.Stock = Product.Stock;
                await _context.SaveChangesAsync();
            }
            else
            {
                prod.Stock += Product.Stock;
                await _context.SaveChangesAsync();

            }



            if (!ModelState.IsValid)
            {
                return Page();
            }

            /*_context.Products.Add(Product);
            await _context.SaveChangesAsync();*/

            return RedirectToPage("./Index");
        }
    }
}
