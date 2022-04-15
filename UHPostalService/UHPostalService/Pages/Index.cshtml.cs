using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UHPostalService.Pages
{
    public class IndexModel : PageModel
    {
        //testing session
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public string Msg { get; set; }
        //end testing session


        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (Username != null && Username.Equals("abc") && Password.Equals("123"))
            {
                HttpContext.Session.SetString("username", Username);
                return RedirectToPage("Welcome");
            }
            else
            {
                Msg = "Invalid";
                return Page();
            }
        }

    }
}