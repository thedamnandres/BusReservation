using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusReservation.Migrations
{
    /// <inheritdoc />
    public partial class horarioseliminadosyedicionderutas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eliminar la tabla Horario
            migrationBuilder.DropTable(
                name: "Horario");

            // Agregar las columnas FechaSalida y Hora a la tabla Ruta
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSalida",
                table: "Ruta",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Hora",
                table: "Ruta",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar las columnas FechaSalida y Hora de la tabla Ruta
            migrationBuilder.DropColumn(
                name: "FechaSalida",
                table: "Ruta");

            migrationBuilder.DropColumn(
                name: "Hora",
                table: "Ruta");

            // Volver a crear la tabla Horario
            migrationBuilder.CreateTable(
                name: "Horario",
                columns: table => new
                {
                    HorarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RutaId = table.Column<int>(type: "int", nullable: false),
                    AsientosDisponibles = table.Column<int>(type: "int", nullable: false),
                    AsientosOcupados = table.Column<int>(type: "int", nullable: false),
                    FechaHoraSalida = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horario", x => x.HorarioId);
                    table.ForeignKey(
                        name: "FK_Horario_Ruta_RutaId",
                        column: x => x.RutaId,
                        principalTable: "Ruta",
                        principalColumn: "RutaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horario_RutaId",
                table: "Horario",
                column: "RutaId");
        }
    }
}