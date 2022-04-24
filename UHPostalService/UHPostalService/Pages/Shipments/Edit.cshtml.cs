#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Shipments
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
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
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
            int store = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("Store")).Value);
            if (store == 0)
            {
                return RedirectToPage("/Account/AccessDenied");
            }
            if (!ModelState.IsValid)
            {
                //return Page();
            }

            Package pkgToUpdate = _context.Packages
                .Include(p=>p.Sender)
                .Include(p=>p.Receiver)
                .Include(p=>p.Destination)
                .Where(p => p.Id == modifiedPkg.Id).FirstOrDefault();
            if(pkgToUpdate == null)
                return NotFound();
            //check if sender info is changed
            if (!From.Equals(pkgToUpdate.Sender))
            {
                //update senderid if already existed in db
                var existed = _context.Customers.Where(c => c.Email == From.Email).FirstOrDefault();
                if (existed != null && From.Equals(existed))
                {
                    pkgToUpdate.SenderID = existed.Id;
                }
                //check if sender email has not changed
                else if (From.Email.Equals(pkgToUpdate.Sender.Email))
                {
                    //change customer details
                    pkgToUpdate.Sender.Copy(From);
                    _context.Attach(pkgToUpdate.Sender).State = EntityState.Modified;
                }
                else if (existed != null && From.Email.Equals(existed.Email))
                {
                    ModelState.AddModelError(string.Empty, $"{From.Email} is already registered");
                    //OnGetAsync(modifiedPkg.Id);
                    return Page();
                }
                //email changed: create new customer
                else
                {
                    _context.Customers.Add(From);
                    await _context.SaveChangesAsync();
                    pkgToUpdate.SenderID = From.Id;
                }
            }
            //update receiver: similar to sender
            if (!To.Equals(pkgToUpdate.Receiver))
            {
                //update senderid if already existed in db
                var existed = _context.Customers.Where(c => c.Email == To.Email).FirstOrDefault();
                if (existed != null && To.Equals(existed))
                {
                    pkgToUpdate.ReceiverID = existed.Id;
                }
                //check if receiver email has not changed
                else if (To.Email.Equals(pkgToUpdate.Receiver.Email))
                {
                    //change customer details
                    pkgToUpdate.Receiver.Copy(To);
                    _context.Attach(pkgToUpdate.Receiver).State = EntityState.Modified;
                }
                else if (existed != null && To.Email.Equals(existed.Email))
                {
                    ModelState.AddModelError(string.Empty, $"{To.Email} is already registered");
                    //OnGetAsync(modifiedPkg.Id);
                    return Page();
                }
                //email changed: create new customer
                else
                {
                    _context.Customers.Add(To);
                    await _context.SaveChangesAsync();
                    pkgToUpdate.ReceiverID = To.Id;
                }
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
            pkgToUpdate.Copy(modifiedPkg.Weight, modifiedPkg.Height, modifiedPkg.Width, modifiedPkg.Depth, modifiedPkg.Express, modifiedPkg.Description);

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
            

            return RedirectToPage("./Index");
        }

        private bool PackageExists(int id)
        {
            return _context.Packages.Any(e => e.Id == id);
        }
    }
}
