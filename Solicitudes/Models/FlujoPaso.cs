using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Diagnostics.Metrics;

namespace Solicitudes.Models
{
    [Index(nameof(FlujoId), nameof(PasoId), IsUnique = true)]
    public class FlujoPaso
    {
        public int Id { get; set; }
        public int FlujoId { get; set; }
        public int PasoId { get; set; }
        public Int16 Prioridad { get; set; }  //Inicia en Cero. Registros con prioridades igual se ejecutan en paralelo
        public Int16 IDEstadoPaso_OK { get; set; }  //Código del estado OK para dar por correcto el paso
        public Int16 IDEstado { get; set; }

        //Integridad Referencial
        /*  Comentareo desde aqui, porque no me ha funcionado bien..Many-to-many
        public Flujo Flujo { get; set; } = null!; // Required reference navigation to principal
        public Paso Paso { get; set; } = null!; // Required reference navigation to principal
        */
    }

    /* Me falta crear esta Integridad referencial
    ALTER TABLE FlujoPaso
        ADD FOREIGN KEY(FlujoId) REFERENCES Flujos(Id);
    ALTER TABLE FlujoPaso
        ADD FOREIGN KEY(PasoId) REFERENCES Pasos(Id);
    */
}
