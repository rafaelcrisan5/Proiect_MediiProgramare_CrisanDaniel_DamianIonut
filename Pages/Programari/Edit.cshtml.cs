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

namespace auto.Pages.Programari
{
    [Authorize(Roles = "Admin,User")]

    public class EditModel : ProgramareServiciiPageModel
    {
        private readonly auto.Data.autoContext _context;

        public EditModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Programare Programare { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Programare = await _context.Programare
                .Include(p => p.Masina)
                .Include(p => p.Angajat)
                .Include(p => p.ProgramariServicii).ThenInclude(p => p.Serviciu)
                .FirstOrDefaultAsync(m => m.ID == id);
            var programare =  await _context.Programare.FirstOrDefaultAsync(m => m.ID == id);
            if (programare == null)
            {
                return NotFound();
            }
            Programare = programare;

            PopulateAssignedServiciuData(_context, Programare);
            ViewData["AngajatID"] = new SelectList(_context.Angajat, "ID", "NumeComplet");
            ViewData["MasinaID"] = new SelectList(_context.Masina, "ID", "NrInmatriculare");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedServicii)
        {
            if (id == null) return NotFound();

            var programareToUpdate = await _context.Programare
                .Include(i => i.Masina)
                .Include(i => i.Angajat)
                .Include(i => i.ProgramariServicii).ThenInclude(i => i.Serviciu)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (programareToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync<Programare>(
                programareToUpdate,
                "Programare",
                i => i.Data,
                i => i.Status,
                i => i.MasinaID,
                i => i.AngajatID))
            {
                UpdateProgramareServicii(_context, selectedServicii, programareToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Daca esueaza validarea
            UpdateProgramareServicii(_context, selectedServicii, programareToUpdate);
            PopulateAssignedServiciuData(_context, programareToUpdate);
            ViewData["MasinaID"] = new SelectList(_context.Masina, "ID", "VIN");
            ViewData["AngajatID"] = new SelectList(_context.Angajat, "ID", "NumeComplet");

            return Page();
        }

         
    }
}
