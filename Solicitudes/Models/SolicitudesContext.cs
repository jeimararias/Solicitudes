using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

// This DbContext will do many things for us such as it will create databases and tables.
// You can check your newly added database file.

namespace Solicitudes.Models
{
    public class SolicitudesContext : DbContext
    {
        public SolicitudesContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Paso>()
                .HasMany(e => e.PasoCampos)
                .WithOne(e => e.Paso)
                .HasForeignKey(e => e.PasoId)
                .IsRequired();

            modelBuilder.Entity<Campo>()
                .HasMany(e => e.PasoCampos)
                .WithOne(e => e.Campo)
                .HasForeignKey(e => e.CampoId)
                .IsRequired();
            
            modelBuilder.Entity<Paso>()
                .HasMany(e => e.PasoCampos)
                .WithMany(e => e.Campo)  //Aqui genera un error, no se que esté esperando
                .UsingEntity(
                    "PasoCampo",
                    l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("CampoId").HasPrincipalKey(nameof(Campo.Id)),
                    r => r.HasOne(typeof(Post)).WithMany().HasForeignKey("PasoId").HasPrincipalKey(nameof(Paso.Id)),
                    j => j.HasKey("PasoId", "CampoId"));
            */
        }

        public DbSet<Flujo> Flujo { get; set; }
        public DbSet<Paso> Paso { get; set; }
        public DbSet<Campo> Campo { get; set; }
        public DbSet<FlujoPaso> FlujoPaso { get; set; }
        public DbSet<PasoCampo> PasoCampo { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Solicitud> Solicitud { get; set; }
        public DbSet<SolicitudData> SolicitudData { get; set; }
        public DbSet<SolicitudControl> SolicitudControl { get; set; }

        // Vistas
        //public DbSet<vwFlujosPasos> FlujosPasos { get; set; }
    }
}
