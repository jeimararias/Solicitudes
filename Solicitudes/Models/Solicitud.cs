namespace Solicitudes.Models
{
    public class Solicitud
    {
        public int Id { get; set; }
        public int FlujoId { get; set; }
        public int UserId { get; set; }
        public string Detalle { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public Int16 IDEstado { get; set; }

        /* Al parecer es mejor ejecutarlo en el controlador
        public async Task<int> Procesar(int solicitudId)
        {

        }
        */
    }
}
