using Azure;

namespace Solicitudes.Models
{
    public class Paso
    {
        public int Id { get; set; }
        public string CodPaso { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public Int16 IDEstado { get; set; }

        //Integridad Referencial

        /*  Comentareo desde aqui, porque no me ha funcionado bien..Many-to-many
        public List<FlujoPaso> FlujoPasos { get; } = new();
        public List<Flujo> Flujos { get; } = new();
        public List<PasoCampo> PasoCampos { get; } = new();
        public List<Campo> Campos { get; } = new();
        */
    }
}
