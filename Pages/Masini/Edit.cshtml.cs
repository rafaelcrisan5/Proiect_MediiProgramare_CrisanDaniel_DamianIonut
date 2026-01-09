using auto.Data;
using auto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auto.Pages.Masini
{
    [Authorize(Roles = "Admin")]

    public class EditModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public EditModel(auto.Data.autoContext context)
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

            var masina =  await _context.Masina.FirstOrDefaultAsync(m => m.ID == id);
            if (masina == null)
            {
                return NotFound();
            }
            Masina = masina;
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "NumeComplet");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Masina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MasinaExists(Masina.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MasinaExists(int id)
        {
            return _context.Masina.Any(e => e.ID == id);
        }
    }
}
