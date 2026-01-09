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

namespace auto.Pages.Servici
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
        public Serviciu Serviciu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviciu =  await _context.Serviciu.FirstOrDefaultAsync(m => m.ID == id);
            if (serviciu == null)
            {
                return NotFound();
            }
            Serviciu = serviciu;
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

            _context.Attach(Serviciu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiciuExists(Serviciu.ID))
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

        private bool ServiciuExists(int id)
        {
            return _context.Serviciu.Any(e => e.ID == id);
        }
    }
}
