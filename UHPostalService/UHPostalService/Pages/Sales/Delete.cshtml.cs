#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Sales
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor")]

    public class DeleteModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public DeleteModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sale Sale { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sale = await _context.Sales
                .Include(s => s.Product).FirstOrDefaultAsync(m => m.ID == id);

            if (Sale == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }

            Sale thisSale = _context.Sales.Where(s=>s.ID ==id).Include(s => s.Product).FirstOrDefault();

            if (thisSale != null)
            {
                //_context.Sales.Remove(Sale);
                thisSale.Deleted = true;
                thisSale.Product.Stock += Sale.Quantity;
                _context.Attach(thisSale.Product).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Products.Where(p=>p.Id == thisSale.ProductID).FirstOrDefault() == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                //await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
