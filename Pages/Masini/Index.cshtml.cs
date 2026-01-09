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

namespace auto.Pages.Masini
{
        [Authorize(Roles = "Admin,User")]

    public class IndexModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public IndexModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        public IList<Masina> Masina { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Masina = await _context.Masina
                .Include(m => m.Client).ToListAsync();
        }
    }
}
