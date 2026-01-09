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

namespace auto.Pages.Servici
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public DetailsModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        public Serviciu Serviciu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviciu = await _context.Serviciu.FirstOrDefaultAsync(m => m.ID == id);
            if (serviciu == null)
            {
                return NotFound();
            }
            else
            {
                Serviciu = serviciu;
            }
            return Page();
        }
    }
}
