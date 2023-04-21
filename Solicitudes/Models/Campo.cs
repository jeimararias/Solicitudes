using Azure;

namespace Solicitudes.Models
{
    public class Campo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public Int16 IDEstado { get; set; }

        //Integridad Referencial
        /*  Comentareo desde aqui, porque no me ha funcionado bien..Many-to-many
        public List<PasoCampo> PasoCampos { get; } = new();
        public List<Paso> Pasos { get; } = new();
        */
    }
}
