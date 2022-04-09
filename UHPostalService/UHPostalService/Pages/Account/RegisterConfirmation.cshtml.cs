using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using UHPostalService.Data;

namespace UHPostalService.Pages.Account
{
    public class RegisterConfirmationModel : PageModel
    {
        private readonly ApplicationDbContext Db;

        public RegisterConfirmationModel(ApplicationDbContext Db)
        {
            this.Db = Db;
        }
        public string Email { get; set; }
        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = Db.Customers.Where(f => f.Email == email).FirstOrDefault();
            if (user == null)
            {
                if (Db.Employees.Where(f => f.Email == email).FirstOrDefault() == null)
                    return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;

            return Page();
        }
    }
}
