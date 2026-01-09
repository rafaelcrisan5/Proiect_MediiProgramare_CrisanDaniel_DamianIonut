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

namespace auto.Pages.Angajati
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public DetailsModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        public Angajat Angajat { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var angajat = await _context.Angajat.FirstOrDefaultAsync(m => m.ID == id);
            if (angajat == null)
            {
                return NotFound();
            }
            else
            {
                Angajat = angajat;
            }
            return Page();
        }
    }
}
