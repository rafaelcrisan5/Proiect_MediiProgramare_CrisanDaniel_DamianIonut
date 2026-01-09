using auto.Data;
using auto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auto.Pages.Masini
{
    [Authorize(Roles = "Admin")]

    public class CreateModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public CreateModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "NumeComplet");
            return Page();
        }

        [BindProperty]
        public Masina Masina { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Masina.Add(Masina);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
