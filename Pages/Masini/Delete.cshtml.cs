using auto.Data;
using auto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auto.Pages.Masini
{
    [Authorize(Roles = "Admin")]

    public class DeleteModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public DeleteModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masina = await _context.Masina.FindAsync(id);
            if (masina != null)
            {
                Masina = masina;
                _context.Masina.Remove(Masina);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
