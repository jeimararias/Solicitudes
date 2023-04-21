using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solicitudes.Migrations
{
    /// <inheritdoc />
    public partial class MigracionV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitudControl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    PasoId = table.Column<int>(type: "int", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDEstado = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudControl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    PasoId = table.Column<int>(type: "int", nullable: false),
                    CampoId = table.Column<int>(type: "int", nullable: false),
                    Dato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDEstado = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudControl_SolicitudId_PasoId",
                table: "SolicitudControl",
                columns: new[] { "SolicitudId", "PasoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudData_SolicitudId_PasoId_CampoId",
                table: "SolicitudData",
                columns: new[] { "SolicitudId", "PasoId", "CampoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudControl");

            migrationBuilder.DropTable(
                name: "SolicitudData");
        }
    }
}
