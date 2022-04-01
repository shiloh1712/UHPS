using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RegisterModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public string ReturnUrl { get; set; }
        [BindProperty]
        public Customer newcustomer { get; set; }
        public string ConfirmPassword { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = _db.Customers.Where(f => f.Email == newcustomer.Email).FirstOrDefault();
                if (user != null)
                {
                    ModelState.AddModelError(string.Empty, newcustomer.Email + " Already exists");
                }
                else
                {
                    user = new Customer { Email = newcustomer.Email, Password = newcustomer.Password };
                    _db.Customers.Add(user);
                    await _db.SaveChangesAsync();
                    return RedirectToPage("RegisterConfirmation", new { email = newcustomer.Email });
                }

            }

            return Page();
        }
    }
}

        