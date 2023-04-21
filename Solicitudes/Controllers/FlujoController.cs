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
    public class FlujoController : ControllerBase
    {
        private readonly SolicitudesContext _context;

        public FlujoController(SolicitudesContext context)
        {
            _context = context;
        }

        // GET: api/Flujo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flujo>>> GetFlujo()
        {
          if (_context.Flujo == null)
          {
              return NotFound();
          }
            return await _context.Flujo.ToListAsync();
        }

        // GET: api/Flujo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flujo>> GetFlujo(int id)
        {
          if (_context.Flujo == null)
          {
              return NotFound();
          }
            var flujo = await _context.Flujo.FindAsync(id);

            if (flujo == null)
            {
                return NotFound();
            }

            return flujo;
        }

        // PUT: api/Flujo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlujo(int id, Flujo flujo)
        {
            if (id != flujo.Id)
            {
                return BadRequest();
            }

            _context.Entry(flujo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlujoExists(id))
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

        // POST: api/Flujo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flujo>> PostFlujo(Flujo flujo)
        {
          if (_context.Flujo == null)
          {
              return Problem("Entity set 'SolicitudesContext.Flujo'  is null.");
          }
            _context.Flujo.Add(flujo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlujo", new { id = flujo.Id }, flujo);
        }

        // DELETE: api/Flujo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlujo(int id)
        {
            if (_context.Flujo == null)
            {
                return NotFound();
            }
            var flujo = await _context.Flujo.FindAsync(id);
            if (flujo == null)
            {
                return NotFound();
            }

            _context.Flujo.Remove(flujo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlujoExists(int id)
        {
            return (_context.Flujo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
