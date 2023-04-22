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
    public class PasoController : ControllerBase
    {
        private readonly SolicitudesContext _context;

        public PasoController(SolicitudesContext context)
        {
            _context = context;
        }

        // GET: api/Paso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paso>>> GetPaso()
        {
          if (_context.Paso == null)
          {
              return NotFound();
          }
            return await _context.Paso.ToListAsync();
        }

        // GET: api/Paso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paso>> GetPaso(int id)
        {
          if (_context.Paso == null)
          {
              return NotFound();
          }
            var paso = await _context.Paso.FindAsync(id);

            if (paso == null)
            {
                return NotFound();
            }

            return paso;
        }

        // PUT: api/Paso/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaso(int id, Paso paso)
        {
            if (id != paso.Id)
            {
                return BadRequest();
            }

            _context.Entry(paso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasoExists(id))
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

        // POST: api/Paso
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Paso>> PostPaso(Paso paso)
        {
          if (_context.Paso == null)
          {
              return Problem("Entity set 'SolicitudesContext.Paso'  is null.");
          }
            _context.Paso.Add(paso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaso", new { id = paso.Id }, paso);
        }

        // DELETE: api/Paso/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaso(int id)
        {
            if (_context.Paso == null)
            {
                return NotFound();
            }
            var paso = await _context.Paso.FindAsync(id);
            if (paso == null)
            {
                return NotFound();
            }

            _context.Paso.Remove(paso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PasoExists(int id)
        {
            return (_context.Paso?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
