using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UHPostalService.Data;
using UHPostalService.Models;


namespace UHPostalService.Pages.Account.Employees
{
    public class LoginModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;
        public LoginModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }


        }
        public string ReturnUrl { get; set; }

        [BindProperty]
        public InputModel SignInUser { get; set; }
        public async Task OnGetAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            ReturnUrl = returnUrl;

        }


        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = _context.Employees.Where(f => f.Email == SignInUser.Email && f.Password == SignInUser.Password).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email or Password");
                    return Page();
                }
                var store = String.IsNullOrEmpty(user.StoreID.ToString()) ? "0" : user.StoreID.ToString();

                //claim loggin info/roles
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim("Store", store)
                    //new Claim("UserDefined", "whatever"),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties { IsPersistent = true });

                return LocalRedirect(returnUrl);
            }

            return Page();
        }

    }
}
