#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Shipments
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
            /*public InputModel( float weight, float width, float height, float depth, bool expr, int id, string desc = null)
            {
                Id = id;
                Description = desc;
                Weight = weight;
                Width = width;
                Height = height;
                Depth = depth;
                Express = expr;
            }*/
            public int Id { get; set; }
            public string? Description { get; set; }
            [DefaultValue(Status.InStore)]
            public Status Status { get; set; }
            public float Weight { get; set; }
            [DefaultValue(false)]
            public bool Express { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
            public float Depth { get; set; }
        }
        [BindProperty]
        public InputModel modifiedPkg { get; set; }
        [BindProperty]
        public Address Address { get; set; }
        [BindProperty]
        public Customer From { get; set; }
        [BindProperty]
        public Customer To { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Package editpkg = await _context.Packages.AsNoTracking()
                .Include(p => p.Destination)
                .Include(p => p.Receiver)
                .Include(p => p.Sender).FirstOrDefaultAsync(m => m.Id == id);


            if (editpkg == null)
            {
                return NotFound();
            }
            /*ViewData["AddressID"] = new SelectList(_context.Addresses, "Id", "Id");
            ViewData["ReceiverID"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["SenderID"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["ClassID"] = new SelectList(_context.ShipmentClasses, "Id", "Id");*/
            From = _context.Customers.Where(s => s.Id == editpkg.SenderID).FirstOrDefault();
            To = _context.Customers.Where(s => s.Id == editpkg.ReceiverID).FirstOrDefault();
            Address = _context.Addresses.Where(a => a.Id == editpkg.AddressID).FirstOrDefault();
            modifiedPkg = new InputModel { Weight = editpkg.Weight, Width = editpkg.Width, Height = editpkg.Height, Depth = editpkg.Depth, Express = editpkg.Express, Id = editpkg.Id, Description = editpkg.Description };
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            Package pkgToUpdate = _context.Packages
                .Include(p=>p.Sender)
                .Include(p=>p.Receiver)
                .Include(p=>p.Destination)
                .Where(p => p.Id == modifiedPkg.Id).FirstOrDefault();
            if(pkgToUpdate == null)
                return NotFound();
            if (!From.Equals(pkgToUpdate.Sender))
            {
                var cust2 = _context.Customers.Where(f => (f.Name == From.Name
                && f.PhoneNumber == From.PhoneNumber
                && f.Email == From.Email)).FirstOrDefault();

                if (cust2 == null)
                {
                    _context.Customers.Add(From);
                    await _context.SaveChangesAsync();
                    cust2 = From;
                }
                pkgToUpdate.SenderID = cust2.Id;
            }
            if (!To.Equals(pkgToUpdate.Receiver))
            {
                var cust = _context.Customers.Where(f => (f.Name == To.Name
                && f.PhoneNumber == To.PhoneNumber
                && f.Email == To.Email)).FirstOrDefault();

                if (cust == null)
                {
                    _context.Customers.Add(From);
                    await _context.SaveChangesAsync();
                    cust = To;
                }
                pkgToUpdate.ReceiverID = To.Id;
            }
            if (!Address.Equals(pkgToUpdate.Destination))
            {
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
                pkgToUpdate.AddressID = addr.Id;
            }
            pkgToUpdate.Description = modifiedPkg.Description;
            pkgToUpdate.Weight = modifiedPkg.Weight;
            pkgToUpdate.Height = modifiedPkg.Height;
            pkgToUpdate.Width = modifiedPkg.Width;
            pkgToUpdate.Depth = modifiedPkg.Depth;
            pkgToUpdate.Express = modifiedPkg.Express;


            _context.Attach(pkgToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(pkgToUpdate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            /*if (id == null)
                return NotFound();
            var pkgToUpdate = await _context.Packages.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (pkgToUpdate == null)
                return NotFound();
            bool sucess = await TryUpdateModelAsync<Package>(
                 pkgToUpdate,
                 "Package",   // Prefix for form value.
                   c => c.SenderID, c => c.ReceiverID, c => c.AddressID, c => c.Description, c => c.Weight, c => c.Width, c => c.Height, c => c.Depth, c => c.Express);
            if (sucess)
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            */

            return RedirectToPage("./Index");
        }

        private bool PackageExists(int id)
        {
            return _context.Packages.Any(e => e.Id == id);
        }
    }
}
