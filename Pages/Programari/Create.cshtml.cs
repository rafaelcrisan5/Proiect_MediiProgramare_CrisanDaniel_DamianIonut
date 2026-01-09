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

namespace auto.Pages.Programari
{
    [Authorize(Roles = "Admin,User")]

    public class CreateModel : ProgramareServiciiPageModel
    {
        private readonly auto.Data.autoContext _context;

        public CreateModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["AngajatID"] = new SelectList(_context.Angajat, "ID", "NumeComplet");
            ViewData["MasinaID"] = new SelectList(_context.Masina, "ID", "NrInmatriculare");

            var programare = new Programare();
        programare.ProgramariServicii = new List<ProgramareServiciu>();
         PopulateAssignedServiciuData(_context, programare);
            return Page();
        }

        [BindProperty]
        public Programare Programare { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedServicii)
        {

            var newProgramare = new Programare();
            if (selectedServicii != null)
            {
                newProgramare.ProgramariServicii = new List<ProgramareServiciu>();
                foreach (var serv in selectedServicii)
                {
                    var servToAdd = new ProgramareServiciu
                    {
                        ServiciuID = int.Parse(serv)
                    };
                    newProgramare.ProgramariServicii.Add(servToAdd);
                }
            }

            // Mapam datele simple
            newProgramare.Data = Programare.Data;
            newProgramare.Status = Programare.Status; // ex: "In Asteptare"
            newProgramare.MasinaID = Programare.MasinaID;
            newProgramare.AngajatID = Programare.AngajatID;

            _context.Programare.Add(Programare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
