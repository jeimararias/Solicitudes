using Microsoft.EntityFrameworkCore;

namespace Solicitudes.Models
{
    [Index(nameof(SolicitudId), nameof(PasoId), nameof(CampoId), IsUnique = true)]
    public class SolicitudData
    {
        public int Id { get; set; }
        public int SolicitudId { get; set; }
        public int PasoId { get; set; }
        public int CampoId { get; set; }
        public string Dato { get; set; } = null!;  //Valor capturado para el campo CampoId. Pueder ser una ruta hacia una imagen, video, etc.
        public Int16 IDEstado { get; set; }
    }
}
