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
        //[HttpPost("{id}")]
        //[Route("Procesar")]
        [HttpPost("{id}", Name = "Procesar")]
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

            // Borrar todos los registros de control del proceso


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
                    // Busca el Paso
                    var paso = await _context.Paso.FindAsync(flujopaso.PasoId);
                    if (paso == null)
                    {
                        return NotFound();
                    }

                    Console.WriteLine($"Flujo: {flujopaso.FlujoId} Paso: {flujopaso.PasoId}  Priority: {flujopaso.Prioridad}  NombrePaso: {paso.Nombre}");

                    //Validar que exista el dato para el campo solicitado
                    bool existeDato = true;
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

                    // Graba el registro del Paso del proceso
                    /*
                    using (var context1 = new SolicitudControlContext())
                    {
                        var blog = context.Blogs.First();
                        blog.Url = "http://example.com/blog";
                        context.SaveChanges();
                    }
                    */

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



                    //Paso.wait
                }

                // esperar a que terminen los hilos para procesar la siguiente prioridad...
                // WaitCallback...
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
