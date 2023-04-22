using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solicitudes.Models;

namespace Solicitudes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasoCampoController : ControllerBase
    {
        private readonly SolicitudesContext _context;

        public PasoCampoController(SolicitudesContext context)
        {
            _context = context;
        }

        // GET: api/PasoCampo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PasoCampo>>> GetPasoCampo()
        {
          if (_context.PasoCampo == null)
          {
              return NotFound();
          }
            return await _context.PasoCampo.ToListAsync();
        }

        // GET: api/PasoCampo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PasoCampo>> GetPasoCampo(int id)
        {
          if (_context.PasoCampo == null)
          {
              return NotFound();
          }
            var pasoCampo = await _context.PasoCampo.FindAsync(id);

            if (pasoCampo == null)
            {
                return NotFound();
            }

            return pasoCampo;
        }

        // PUT: api/PasoCampo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPasoCampo(int id, PasoCampo pasoCampo)
        {
            if (id != pasoCampo.Id)
            {
                return BadRequest();
            }

            _context.Entry(pasoCampo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasoCampoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PasoCampo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PasoCampo>> PostPasoCampo(PasoCampo pasoCampo)
        {
          if (_context.PasoCampo == null)
          {
              return Problem("Entity set 'SolicitudesContext.PasoCampo'  is null.");
          }
            _context.PasoCampo.Add(pasoCampo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPasoCampo", new { id = pasoCampo.Id }, pasoCampo);
        }

        // DELETE: api/PasoCampo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePasoCampo(int id)
        {
            if (_context.PasoCampo == null)
            {
                return NotFound();
            }
            var pasoCampo = await _context.PasoCampo.FindAsync(id);
            if (pasoCampo == null)
            {
                return NotFound();
            }

            _context.PasoCampo.Remove(pasoCampo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PasoCampoExists(int id)
        {
            return (_context.PasoCampo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
