using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using auto.Data;
using auto.Models;

namespace auto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramaresController : ControllerBase
    {
        private readonly autoContext _context;

        public ProgramaresController(autoContext context)
        {
            _context = context;
        }

        [HttpGet("ByPlate/{plate}")]
        public async Task<ActionResult<IEnumerable<object>>> GetProgramariByPlate(string plate)
        {
           
            string cleanPlate = plate.Replace(" ", "").ToUpper();

            var programari = await _context.Programare
                .Include(p => p.Masina)
                .Include(p => p.ProgramariServicii) 
                    .ThenInclude(ps => ps.Serviciu)  
                .Where(p => p.Masina.NrInmatriculare.Replace(" ", "") == cleanPlate)
                .ToListAsync();

            
            var rezultate = programari.Select(p => new
            {
                id = p.ID,
                data = p.Data,
                status = p.Status,
                numarInmatriculare = p.Masina?.NrInmatriculare,
                
                pretTotal = p.ProgramariServicii?.Sum(ps => ps.Serviciu?.Pret ?? 0) ?? 0
            });

            return Ok(rezultate);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Programare>> GetProgramare(int id)
        {
            var programare = await _context.Programare.FindAsync(id);
            if (programare == null) return NotFound();
            return programare;
        }

        [HttpPost]
        public async Task<ActionResult<Programare>> PostProgramare(Programare programare)
        {
            _context.Programare.Add(programare);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProgramare", new { id = programare.ID }, programare);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgramare(int id, Programare programare)
        {
            if (id != programare.ID) return BadRequest();
            _context.Entry(programare).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramareExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramare(int id)
        {
            var programare = await _context.Programare.FindAsync(id);
            if (programare == null) return NotFound();
            _context.Programare.Remove(programare);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProgramareExists(int id)
        {
            return _context.Programare.Any(e => e.ID == id);
        }
    }
}