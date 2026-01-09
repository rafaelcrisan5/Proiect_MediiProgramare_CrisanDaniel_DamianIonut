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

namespace auto.Pages.Servici
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
            return Page();
        }

        [BindProperty]
        public Serviciu Serviciu { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Serviciu.Add(Serviciu);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
