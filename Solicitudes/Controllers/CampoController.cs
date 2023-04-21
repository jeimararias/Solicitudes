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
    public class CampoController : ControllerBase
    {
        private readonly SolicitudesContext _context;

        public CampoController(SolicitudesContext context)
        {
            _context = context;
        }

        // GET: api/Campo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campo>>> GetCampo()
        {
          if (_context.Campo == null)
          {
              return NotFound();
          }
            return await _context.Campo.ToListAsync();
        }

        // GET: api/Campo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Campo>> GetCampo(int id)
        {
          if (_context.Campo == null)
          {
              return NotFound();
          }
            var campo = await _context.Campo.FindAsync(id);

            if (campo == null)
            {
                return NotFound();
            }

            return campo;
        }

        // PUT: api/Campo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampo(int id, Campo campo)
        {
            if (id != campo.Id)
            {
                return BadRequest();
            }

            _context.Entry(campo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampoExists(id))
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

        // POST: api/Campo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Campo>> PostCampo(Campo campo)
        {
          if (_context.Campo == null)
          {
              return Problem("Entity set 'SolicitudesContext.Campo'  is null.");
          }
            _context.Campo.Add(campo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCampo", new { id = campo.Id }, campo);
        }

        // DELETE: api/Campo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampo(int id)
        {
            if (_context.Campo == null)
            {
                return NotFound();
            }
            var campo = await _context.Campo.FindAsync(id);
            if (campo == null)
            {
                return NotFound();
            }

            _context.Campo.Remove(campo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CampoExists(int id)
        {
            return (_context.Campo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
