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
    public class SolicitudController : ControllerBase
    {
        private readonly SolicitudesContext _context;

        public SolicitudController(SolicitudesContext context)
        {
            _context = context;
        }

        // GET: api/Solicitud
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitud>>> GetSolicitud()
        {
          if (_context.Solicitud == null)
          {
              return NotFound();
          }
            return await _context.Solicitud.ToListAsync();
        }

        // GET: api/Solicitud/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitud>> GetSolicitud(int id)
        {
          if (_context.Solicitud == null)
          {
              return NotFound();
          }
            var solicitud = await _context.Solicitud.FindAsync(id);

            if (solicitud == null)
            {
                return NotFound();
            }

            return solicitud;
        }

        // PUT: api/Solicitud/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitud(int id, Solicitud solicitud)
        {
            if (id != solicitud.Id)
            {
                return BadRequest();
            }

            _context.Entry(solicitud).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudExists(id))
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

        // POST: api/Solicitud
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Solicitud>> PostSolicitud(Solicitud solicitud)
        {
          if (_context.Solicitud == null)
          {
              return Problem("Entity set 'SolicitudesContext.Solicitud'  is null.");
          }
            _context.Solicitud.Add(solicitud);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolicitud", new { id = solicitud.Id }, solicitud);
        }

        // DELETE: api/Solicitud/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitud(int id)
        {
            if (_context.Solicitud == null)
            {
                return NotFound();
            }
            var solicitud = await _context.Solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }

            _context.Solicitud.Remove(solicitud);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SolicitudExists(int id)
        {
            return (_context.Solicitud?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Procesa solicitudes
        // POST: api/Solicitud/Procesar/5
        [HttpPost("{id}")]
        // [Route("Procesar")]
        // [NonAction]
        public async Task<IActionResult> PostSolicitudProcesar(int id)
        {
            if (_context.Solicitud == null)
            {
                return NotFound();
            }

            // Busca la Solicitud
            var solicitud = await _context.Solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }

            // Busca el Flujo asociado
            var flujo = await _context.Flujo.FindAsync(solicitud.FlujoId);
            if (flujo == null)
            {
                return NotFound();
            }

            Console.WriteLine("Flujo: "+flujo.Nombre);

            // Busca los Pasos del Flujo
            var flujoPasos = _context.FlujoPaso.Where(x => x.FlujoId == solicitud.FlujoId).OrderBy(a => a.Prioridad); // await //(x => x.Atributo == valor && x.Atributo2 == valor2)
            if (flujoPasos == null)
            {
                return NotFound();
            }

            // Procesa el Flujo por cada paso
            foreach (var flujopaso in flujoPasos)
            {
                // Busca el Paso
                var paso = await _context.Paso.FindAsync(flujopaso.PasoId);
                if (paso == null)
                {
                    return NotFound();
                }
                Console.WriteLine($"Flujo: {flujopaso.FlujoId} Paso: {flujopaso.PasoId}  Priority: {flujopaso.Prioridad}  NombrePaso: {paso.Nombre}");
            }

            /*
            //var flujo = _context.Database.SqlQuery<dynamic>($"Select * from vwFlujosPasos FP where FP.FlujoId = {id} Order by FP.Prioridad, FP.PasoId").ToList();

            // Crear Clase asociada
            // for each:
            // Thread.start()

            */

            //return CreatedAtAction("GetFlujo", new { id = flujo.Id }, flujo);
            return CreatedAtAction("GetSolicitud", new { id = solicitud.Id }, solicitud);

            //return await _context.Solicitud.ToListAsync();
            //return NoContent();
        }
    }
}
