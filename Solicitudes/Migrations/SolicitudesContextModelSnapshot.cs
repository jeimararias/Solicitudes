﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Solicitudes.Models;

#nullable disable

namespace Solicitudes.Migrations
{
    [DbContext(typeof(SolicitudesContext))]
    partial class SolicitudesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Solicitudes.Models.Campo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("IDEstado")
                        .HasColumnType("smallint");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Campo");
                });

            modelBuilder.Entity("Solicitudes.Models.Flujo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodFlujo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EntidadServicio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("IDEstado")
                        .HasColumnType("smallint");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Flujo");
                });

            modelBuilder.Entity("Solicitudes.Models.FlujoPaso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FlujoId")
                        .HasColumnType("int");

                    b.Property<short>("IDEstado")
                        .HasColumnType("smallint");

                    b.Property<short>("IDEstadoPaso_OK")
                        .HasColumnType("smallint");

                    b.Property<int>("PasoId")
                        .HasColumnType("int");

                    b.Property<short>("Prioridad")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("FlujoId", "PasoId")
                        .IsUnique();

                    b.ToTable("FlujoPaso");
                });

            modelBuilder.Entity("Solicitudes.Models.Paso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodPaso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("IDEstado")
                        .HasColumnType("smallint");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Paso");
                });

            modelBuilder.Entity("Solicitudes.Models.PasoCampo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CampoId")
                        .HasColumnType("int");

                    b.Property<bool>("EsRequerido")
                        .HasColumnType("bit");

                    b.Property<string>("ExpresionRegular")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("IDEstado")
                        .HasColumnType("smallint");

                    b.Property<int>("PasoId")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PasoId", "CampoId")
                        .IsUnique();

                    b.ToTable("PasoCampo");
                });

            modelBuilder.Entity("Solicitudes.Models.Solicitud", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("FlujoId")
                        .HasColumnType("int");

                    b.Property<short>("IDEstado")
                        .HasColumnType("smallint");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Solicitud");
                });

            modelBuilder.Entity("Solicitudes.Models.SolicitudControl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("IDEstado")
                        .HasColumnType("smallint");

                    b.Property<int>("PasoId")
                        .HasColumnType("int");

                    b.Property<int>("SolicitudId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SolicitudId", "PasoId")
                        .IsUnique();

                    b.ToTable("SolicitudControl");
                });

            modelBuilder.Entity("Solicitudes.Models.SolicitudData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CampoId")
                        .HasColumnType("int");

                    b.Property<string>("Dato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("IDEstado")
                        .HasColumnType("smallint");

                    b.Property<int>("PasoId")
                        .HasColumnType("int");

                    b.Property<int>("SolicitudId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SolicitudId", "PasoId", "CampoId")
                        .IsUnique();

                    b.ToTable("SolicitudData");
                });

            modelBuilder.Entity("Solicitudes.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EntidadServicio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("IDEstado")
                        .HasColumnType("smallint");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
