using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace Solicitudes.Models
{
    [Index(nameof(PasoId), nameof(CampoId), IsUnique = true)]
    public class PasoCampo
    {
        public int Id { get; set; }
        public int PasoId { get; set; }
        public int CampoId { get; set; }
        public Boolean EsRequerido { get; set; }  // 1=Es campo Obligatorio
        public string Tipo { get; set; } = null!; // E=Entrada, S=Salida,...
        public string ExpresionRegular { get; set; } = null!; //RegEx asociada para validar previamente
        public Int16 IDEstado { get; set; }

        //Integridad Referencial
        /*  Comentareo desde aqui, porque no me ha funcionado bien..Many-to-many
        [ForeignKey("PasoId")]
        public Paso Pasos { get; set; } = null!;
        [ForeignKey("CampoId")]
        public Campo Campos { get; set; } = null!;
        */
    }

    /* Me falta crear esta Integridad Referencial
    ALTER TABLE PasosCampos
        ADD FOREIGN KEY(PasoId) REFERENCES Pasos(Id);
    ALTER TABLE PasosCampos
        ADD FOREIGN KEY(CampoId) REFERENCES Campos(Id);
    */
}
