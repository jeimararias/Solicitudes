namespace Solicitudes.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string EntidadServicio { get; set; } = null!;
        public Int16 IDEstado { get; set; }
    }
}
