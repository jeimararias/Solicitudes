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
    public class SolicitudDataController : ControllerBase
    {
        private readonly SolicitudesContext _context;

        public SolicitudDataController(SolicitudesContext context)
        {
            _context = context;
        }

        // GET: api/SolicitudData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudData>>> GetSolicitudData()
        {
          if (_context.SolicitudData == null)
          {
              return NotFound();
          }
            return await _context.SolicitudData.ToListAsync();
        }

        // GET: api/SolicitudData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudData>> GetSolicitudData(int id)
        {
          if (_context.SolicitudData == null)
          {
              return NotFound();
          }
            var solicitudData = await _context.SolicitudData.FindAsync(id);

            if (solicitudData == null)
            {
                return NotFound();
            }

            return solicitudData;
        }

        // PUT: api/SolicitudData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitudData(int id, SolicitudData solicitudData)
        {
            if (id != solicitudData.Id)
            {
                return BadRequest();
            }

            _context.Entry(solicitudData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudDataExists(id))
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

        // POST: api/SolicitudData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SolicitudData>> PostSolicitudData(SolicitudData solicitudData)
        {
          if (_context.SolicitudData == null)
          {
              return Problem("Entity set 'SolicitudesContext.SolicitudData'  is null.");
          }
            _context.SolicitudData.Add(solicitudData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolicitudData", new { id = solicitudData.Id }, solicitudData);
        }

        // DELETE: api/SolicitudData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitudData(int id)
        {
            if (_context.SolicitudData == null)
            {
                return NotFound();
            }
            var solicitudData = await _context.SolicitudData.FindAsync(id);
            if (solicitudData == null)
            {
                return NotFound();
            }

            _context.SolicitudData.Remove(solicitudData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SolicitudDataExists(int id)
        {
            return (_context.SolicitudData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
