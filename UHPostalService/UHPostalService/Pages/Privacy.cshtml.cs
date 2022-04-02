using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UHPostalService.Pages
{
    [Authorize(AuthenticationSchemes ="Cookies")]
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}