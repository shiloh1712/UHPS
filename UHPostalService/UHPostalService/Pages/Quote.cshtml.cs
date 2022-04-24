#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UHPostalService.Data;
using UHPostalService.Models;

namespace UHPostalService.Pages
{
    public class QuoteModel : PageModel
    {
        private readonly UHPostalService.Data.ApplicationDbContext _context;

        public QuoteModel(UHPostalService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        /*public class InputModel
        {
            public float Weight { get; set; }
            public float Height { get; set; }
            public float Width { get; set; }
            public float Depth { get; set; }
            public bool Express { get; set; }

        }
        [BindProperty]
        public InputModel Package { get; set; }*/
        public IList<ShipmentClass> ShipmentClass { get; set; }
        public float final { get; set; }
        [BindProperty]
        public float Weight { get; set; }
        [BindProperty]
        public float Height { get; set; }
        [BindProperty]
        public float Length { get; set; }
        [BindProperty]
        public float Width { get; set; }
        [BindProperty]
        public bool Express { get; set; }
        public int test { get; set; }
        public float def { get; set; }

        public async Task OnGetAsync(float wei, float hei, float len, float wid, int filterby)
        {
            Weight = wei;
            Height = hei;
            Length = len;
            Width = wid;
            //Express = exp;

            if (wei == 0 || Height == def || Width == def || Length == def)
            {
                ModelState.AddModelError(string.Empty, "No fields can be zero");
                

            }
            else
            {
                var shipclass = _context.ShipmentClasses.Where(s => s.MaxWidth >= Width && s.MaxHeight >= Height && s.MaxLength >= Length).OrderBy(s=>s.GroundCost).FirstOrDefault();
                if (shipclass == null)
                {
                    shipclass = _context.ShipmentClasses.OrderByDescending(s => s.GroundCost).FirstOrDefault();
                }

                if (/*Express == false*/filterby == 1)
                {
                    final = shipclass.GroundCost;
                }
                else
                {
                    final = shipclass.ExpressCost;
                }
                final *= Weight;
                ShipmentClass = await _context.ShipmentClasses.ToListAsync();

            }
        }
    }
}
