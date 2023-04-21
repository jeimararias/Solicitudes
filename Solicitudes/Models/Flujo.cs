using Microsoft.Extensions.Hosting;

namespace Solicitudes.Models
{
    public class Flujo
    {
        public int Id { get; set; }
        public string CodFlujo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string EntidadServicio { get; set; } = null!;
        public Int16 IDEstado { get; set; }

        //Integridad Referencial
        /*  Comentareo desde aqui, porque no me ha funcionado bien..Many-to-many
        public ICollection<FlujoPaso> FlujoPasos { get; } = new List<FlujoPaso>(); // Collection navigation containing dependents
        */
    }
}
