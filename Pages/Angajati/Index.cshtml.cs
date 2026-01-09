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
    public class IndexModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public IndexModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        public IList<Angajat> Angajat { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Angajat = await _context.Angajat.ToListAsync();
        }
    }
}
