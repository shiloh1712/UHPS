#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.temp
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Employee,Admin,Supervisor")]
    public class EditModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public EditModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public class InputModel
        {
            public int ID { get; set; }
            public int Quantity { get; set; }
            public int? ProductID { get; set; }
            public string? Email { get; set; }
            public string? Name  { get; set; }
        }

        [BindProperty]
        public InputModel Sale { get; set; }
        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            if (id == null)
            {
                return NotFound();
            }
            var editsale = await _context.Sales.AsNoTracking().Include(m=>m.Buyer).FirstOrDefaultAsync(m => m.ID == id);

            /*Sale = await _context.Sales
                .Include(s => s.Buyer)
                .Include(s => s.Product).FirstOrDefaultAsync(m => m.ID == id);
            */
            if (editsale == null)
            {
                return NotFound();
            }
            ViewData["BuyerID"] = new SelectList(_context.Customers, "Id", "Email");
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Desc");
            //Buyer = _context.Customers.FirstOrDefault(a => a.Id == editsale.BuyerID);
            Sale = new InputModel { ID = editsale.ID, ProductID = editsale.ProductID, Quantity = editsale.Quantity};
            if(editsale.Buyer != null)
            {
                Sale.Name = editsale.Buyer.Name;
                Sale.Email = editsale.Buyer.Email;
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            /*Buyer.Name = Sale.Name;
            Buyer.Email = Sale.Email;
            if (!ModelState.IsValid)
            {
                return Page();
            }*/
            /*if (Sale.Email != null && Sale.Name != null)
            {
                Buyer = new Customer { Name = Sale.Name, Email = Sale.Email };
            }*/
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }

            if ((Sale.Email == null && Sale.Name != null) || (Sale.Email != null && Sale.Name == null))
            {
                ModelState.AddModelError(string.Empty, "Please fill out both or neither of the Buyer input fields");
                return Page();
            }
            
            var upSale = _context.Sales.Include(p => p.Product).Include(p => p.Buyer).Where(p => p.ID == Sale.ID).FirstOrDefault();
            // check validity of the inputs and decide what to do
            var prod = _context.Products.Where(p => p.Id == Sale.ProductID).FirstOrDefault();

            if (upSale == null)
            {
                return NotFound();
            }
            if(upSale.ProductID != Sale.ProductID)
            {
                if(prod.Stock < Sale.Quantity)
                {
                    ModelState.AddModelError(string.Empty, "Only " + prod.Stock + " " + prod.Desc + "s left in stock!");
                    return Page();
                }
                
            }
            else
            {
                if((prod.Stock+upSale.Quantity) < Sale.Quantity)
                {
                    ModelState.AddModelError(string.Empty, "Only " + prod.Stock + " " + prod.Desc + "s left in stock!");
                    return Page();
                }
            }
            if (Sale.Quantity == 0)
            {
                upSale.Deleted = true;
            }
            upSale.Quantity = Sale.Quantity;
            upSale.ProductID = Sale.ProductID;
            //var prod = _context.Products.Where(p=>p.Id == Sale.ProductID).FirstOrDefault();

            
            if(Sale.Name != null && Sale.Email != null)
            {
                var exist = _context.Customers.Where(s => s.Email == Sale.Email).FirstOrDefault();
                if(exist != null)
                {
                    if(Sale.Name != null)
                    {
                        exist.Name = Sale.Name;
                    }
                    upSale.BuyerID = exist.Id;
                    upSale.Buyer = exist;

                }
                if(exist == null)
                {
                    var newB = new Customer { Email = Sale.Email, Name = Sale.Name };
                    _context.Customers.Add(newB);
                    await _context.SaveChangesAsync();
                    upSale.BuyerID = newB.Id;
                    upSale.Buyer = newB;
                }
            }
            /*upSale.Buyer = Buyer;
            if(upSale.Buyer.Email != Buyer.Email)
            {
                _context.Customers.Add(Buyer);
                await _context.SaveChangesAsync();
                upSale.BuyerID = Buyer.Id;
            }*/
            

            _context.Attach(upSale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(upSale.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.ID == id);
        }
    }
}
