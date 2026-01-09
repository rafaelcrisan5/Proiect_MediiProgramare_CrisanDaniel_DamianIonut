using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using auto.Data;
using auto.Models;

namespace auto.Pages.Masini
{
    public class DetailsModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public DetailsModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        public Masina Masina { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masina = await _context.Masina.FirstOrDefaultAsync(m => m.ID == id);
            if (masina == null)
            {
                return NotFound();
            }
            else
            {
                Masina = masina;
            }
            return Page();
        }
    }
}
