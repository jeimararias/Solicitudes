using Microsoft.EntityFrameworkCore;

namespace Solicitudes.Models
{
    [Index(nameof(SolicitudId), nameof(PasoId), IsUnique = true)]
    public class SolicitudControl
    {
        public int Id { get; set; }
        public int SolicitudId { get; set; }
        public int PasoId { get; set; }
        public string Detalle { get; set; } = null!;
        public Int16 IDEstado { get; set; }
    }
}
