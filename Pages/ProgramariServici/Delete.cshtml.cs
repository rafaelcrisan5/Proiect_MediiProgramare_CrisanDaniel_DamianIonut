using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using auto.Data;
using auto.Models;

namespace auto.Pages.ProgramariServici
{
    public class DeleteModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public DeleteModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProgramareServiciu ProgramareServiciu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programareserviciu = await _context.ProgramareServiciu.FirstOrDefaultAsync(m => m.ID == id);

            if (programareserviciu == null)
            {
                return NotFound();
            }
            else
            {
                ProgramareServiciu = programareserviciu;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programareserviciu = await _context.ProgramareServiciu.FindAsync(id);
            if (programareserviciu != null)
            {
                ProgramareServiciu = programareserviciu;
                _context.ProgramareServiciu.Remove(ProgramareServiciu);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
