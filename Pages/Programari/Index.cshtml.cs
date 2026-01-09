using auto.Data;
using auto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace auto.Pages.Programari
{
    [Authorize(Roles = "Admin,User")]

    public class IndexModel : PageModel
    {
        private readonly auto.Data.autoContext _context;

        public IndexModel(auto.Data.autoContext context)
        {
            _context = context;
        }

        public IList<Programare> Programare { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

       
        public string DateSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
           
          
            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

    
            var programariQuery = _context.Programare
                .Include(p => p.Angajat)
                .Include(p => p.Masina)
                .ThenInclude(m => m.Client)
                .AsQueryable();

      
            if (!string.IsNullOrEmpty(SearchString))
            {
                programariQuery = programariQuery.Where(s =>
                    s.Masina.NrInmatriculare.Contains(SearchString));
            }

          
            switch (sortOrder)
            {
                case "date_desc":
             
                    programariQuery = programariQuery.OrderByDescending(s => s.Data);
                    break;

                default:

                    programariQuery = programariQuery.OrderBy(s => s.Data);
                    break;
            }

            Programare = await programariQuery.ToListAsync();
        }
    }
}