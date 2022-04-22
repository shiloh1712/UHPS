using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UHPostalService.Pages
{
    public class QuoteModel : PageModel
    {
        public class InputModel
        {
            public float Weight { get; set; }
            public float Height { get; set; }
            public float Width { get; set; }
            public float Depth { get; set; }
            public bool Express { get; set; }

        }
        public InputModel Package { get; set; }
        public void OnGet()
        {
        }
    }
}
