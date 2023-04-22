using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Solicitudes.Models;

namespace Solicitudes.Controllers
{
    //[RoutePrefix("api/[controller]")]
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
        // POST: api/Solicitud/5/Procesar
        [HttpPost("{id}/Procesar", Name = "Procesar")]
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

            bool existeDato = true; // Controla si existe algun error

            // Borrar todos los registros de control del proceso
            var idSolCtl = _context.SolicitudControl.Where(x => x.SolicitudId == id).Select(m => m.Id);
            if (idSolCtl != null)
            {
                foreach (var idsc in idSolCtl)
                {
                    var scctl = _context.SolicitudControl.Find(idsc);
                    if (scctl != null)
                    {
                        _context.SolicitudControl.Remove(scctl);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            // Busca las prioridades del flujo
            var prioridades = _context.FlujoPaso.Where(x => x.FlujoId == solicitud.FlujoId).Select(m => m.Prioridad).Distinct().OrderBy(m => m); // await //(x => x.Atributo == valor && x.Atributo2 == valor2)
            if (prioridades == null)
            {
                return NotFound();
            }

            foreach (var prioridad in prioridades)
            {
                Console.WriteLine($"Procesando pasos prioridad...: {prioridad}");

                // Busca los Pasos del Flujo de la prioridad
                var flujoPasos = _context.FlujoPaso.Where(x => x.FlujoId == solicitud.FlujoId && x.Prioridad == prioridad); // await //(x => x.Atributo == valor && x.Atributo2 == valor2)
                if (flujoPasos == null)
                {
                    return NotFound();
                }

                // Procesa todos los pasos con la misma prioridad deben ejecutar en forma asincrona
                foreach (var flujopaso in flujoPasos)
                {
                    // Convertir a método independiente para ejecutarlo asincrónicamente
                    // Thread.start()
                    // Busca el Paso
                    var paso = await _context.Paso.FindAsync(flujopaso.PasoId);
                    if (paso == null)
                    {
                        return NotFound();
                    }

                    Console.WriteLine($"Flujo: {flujopaso.FlujoId} Paso: {flujopaso.PasoId}  Priority: {flujopaso.Prioridad}  NombrePaso: {paso.Nombre}");

                    //Validar que exista el dato para el campo solicitado
                    // Busca los Campos para el Paso
                    var pasoCampos = _context.PasoCampo.Where(x => x.PasoId == flujopaso.PasoId && x.EsRequerido == true); // Que sea un campo Requerido
                    if (pasoCampos == null)
                    {
                        return NotFound();
                    }

                    // Valida que el campo exista en la Solicitud Data
                    foreach (var pasocampo in pasoCampos)
                    {
                        var data = _context.SolicitudData.Where(x => x.SolicitudId == solicitud.Id && x.PasoId == flujopaso.PasoId && x.CampoId == pasocampo.CampoId); // Busca si existe el registro
                        if (data == null)
                        {
                            existeDato = false;
                        }
                    }

                    SolicitudControl solControl = new SolicitudControl();
                    solControl.SolicitudId = solicitud.Id;
                    solControl.PasoId = flujopaso.PasoId;
                    if (existeDato)
                    {
                        solControl.Detalle = "Paso OK";
                        solControl.IDEstado = 3; //Correcto
                    }
                    else
                    {
                        solControl.Detalle = "Faltan datos para el paso";
                        solControl.IDEstado = 9; //Incorrecto
                    }
                    _context.SolicitudControl.Add(solControl);
                    await _context.SaveChangesAsync();
                }

                // esperar a que terminen los hilos para procesar la siguiente prioridad...
                // WaitCallback...
            }

            // Actualiza el estado de la solicitud //
            if (existeDato)
            {
                solicitud.IDEstado = 3;  //procesado ok
            }
            else
            {
                solicitud.IDEstado = 9;  //Faltan procesos
            }
            _context.Entry(solicitud).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            /* No me funcionó bien, entonces solo se retorna el estado de la Solicitud
            var resultado = _context.SolicitudControl.Where(x => x.SolicitudId == id);
            if (resultado == null)
            {
                return CreatedAtAction("GetSolicitud", new { id = solicitud.Id }, solicitud);
            }
            else
            {
                return CreatedAtAction("GetSolicitudControl", new { id = resultado.Id }, resultado);
            }
            */

            return CreatedAtAction("GetSolicitud", new { id = solicitud.Id }, solicitud);
        }
    }
}
