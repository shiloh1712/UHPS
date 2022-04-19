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

namespace UHPostalService.Pages.temp
{
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
            public int ProductID { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }

        }
        [BindProperty]
        public InputModel ModSale { get; set; }
        
        public Customer Buyer   { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sale editSale = await _context.Sales
                .Include(s => s.Buyer)
                .Include(s => s.Product).FirstOrDefaultAsync(m => m.ID == id);

            if (editSale == null)
            {
                return NotFound();
            }
            //ViewData["BuyerID"] = new SelectList(_context.Customers, "Id", "Email");
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Desc");
            Buyer = _context.Customers.Where(s => s.Id == editSale.BuyerID).FirstOrDefault();
            ModSale = new InputModel { Quantity = editSale.Quantity, ID = editSale.ID, ProductID = (int)editSale.ProductID };
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                //return Page();
            }*/
            bool empty = false;
            if(ModSale.Email == null && ModSale.Name == null)
            {
                empty = true;
            }
            
            if(ModSale.Email != null && ModSale.Name != null)
            {   
                Buyer = _context.Customers.Where(c=>c.Email == ModSale.Email).FirstOrDefault();
                if(Buyer == null)
                {
                    Buyer = new Customer { Email = ModSale.Email, Name = ModSale.Name };
                }
                /*Buyer.Email = ModSale.Email;
                Buyer.Name = ModSale.Name;*/
            }
            if((ModSale.Email == null && ModSale.Name != null) || (ModSale.Name == null && ModSale.Email != null))
            {
                ModelState.AddModelError(string.Empty, "Please fill out both customer fields");
                return Page();
            }
            
            
            
            Sale saleUpdate = _context.Sales
                .Include(p => p.Buyer)
                .Include(p => p.Product)
                .Where(p => p.ID == ModSale.ID).FirstOrDefault();
            if(saleUpdate == null)
            {
                return NotFound();
            }
            //check is sale info has changed
            if(ModSale.Quantity > 0)
            {
                saleUpdate.Quantity = ModSale.Quantity;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Quantity must be greater than 0");
                return Page();
            }
            saleUpdate.ProductID = ModSale.ProductID;
            if(saleUpdate.Buyer != null)
            {
                if(Buyer.Email == saleUpdate.Buyer.Email && Buyer.Name != saleUpdate.Buyer.Name && Buyer.Name != null && Buyer.Email != null)
                {
                    var temp = _context.Customers.Where(c => c.Email == Buyer.Email).FirstOrDefault();
                    temp.Name = Buyer.Name;
                }
                if (Buyer.Email == saleUpdate.Buyer.Email && Buyer.Name == saleUpdate.Buyer.Name && Buyer.Name != null && Buyer.Email != null)
                {
                    var temp = _context.Customers.Where(c => c.Email == Buyer.Email).FirstOrDefault();

                    saleUpdate.Buyer = temp;
                    saleUpdate.BuyerID = temp.Id;
                    saleUpdate.Buyer.Email = temp.Email;
                    saleUpdate.Buyer.Name = temp.Name;
                }
                if (Buyer.Email != saleUpdate.Buyer.Email && Buyer.Name == saleUpdate.Buyer.Name && Buyer.Name != null && Buyer.Email != null)
                {

                    _context.Customers.Add(Buyer);
                    await _context.SaveChangesAsync();
                    saleUpdate.BuyerID = Buyer.Id;

                }


            }
            else if (!empty)
            {
                var temp = _context.Customers.Where(c => c.Email == Buyer.Email).FirstOrDefault();
                if(temp == null)
                {
                    _context.Customers.Add(Buyer);
                    await _context.SaveChangesAsync();
                    saleUpdate.BuyerID = Buyer.Id;
                }
                else
                {
                    saleUpdate.Buyer = temp;
                    saleUpdate.BuyerID = temp.Id;
                    saleUpdate.Buyer.Email = temp.Email;
                    saleUpdate.Buyer.Name = temp.Name;
                }
            }
            _context.Attach(saleUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(saleUpdate.ID))
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
