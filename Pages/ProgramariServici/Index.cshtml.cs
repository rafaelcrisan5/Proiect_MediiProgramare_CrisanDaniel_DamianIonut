using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using auto.Data;
using auto.Models;

namespace auto.Pages.ProgramariServici
{
    public class IndexModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public IndexModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        public IList<ProgramareServiciu> ProgramareServiciu { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ProgramareServiciu = await _context.ProgramareServiciu
                .Include(p => p.Programare)
                .Include(p => p.Serviciu).ToListAsync();
        }
    }
}
