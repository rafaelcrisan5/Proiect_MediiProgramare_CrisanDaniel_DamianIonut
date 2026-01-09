using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using auto.Data;
using auto.Models;

namespace auto.Pages.ProgramariServici
{
    public class EditModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public EditModel(auto.Data.autoContext context)
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

            var programareserviciu =  await _context.ProgramareServiciu.FirstOrDefaultAsync(m => m.ID == id);
            if (programareserviciu == null)
            {
                return NotFound();
            }
            ProgramareServiciu = programareserviciu;
           ViewData["ProgramareID"] = new SelectList(_context.Programare, "ID", "ID");
           ViewData["ServiciuID"] = new SelectList(_context.Set<Serviciu>(), "ID", "Denumire");
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

            _context.Attach(ProgramareServiciu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramareServiciuExists(ProgramareServiciu.ID))
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

        private bool ProgramareServiciuExists(int id)
        {
            return _context.ProgramareServiciu.Any(e => e.ID == id);
        }
    }
}
