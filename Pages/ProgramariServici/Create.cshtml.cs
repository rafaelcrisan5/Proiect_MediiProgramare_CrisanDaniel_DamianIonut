using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using auto.Data;
using auto.Models;

namespace auto.Pages.ProgramariServici
{
    public class CreateModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public CreateModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProgramareID"] = new SelectList(_context.Programare, "ID", "ID");
        ViewData["ServiciuID"] = new SelectList(_context.Set<Serviciu>(), "ID", "Denumire");
            return Page();
        }

        [BindProperty]
        public ProgramareServiciu ProgramareServiciu { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProgramareServiciu.Add(ProgramareServiciu);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
