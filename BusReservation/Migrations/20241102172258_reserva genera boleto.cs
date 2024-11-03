using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusReservation.Migrations
{
    /// <inheritdoc />
    public partial class reservageneraboleto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Boleto_ReservaId",
                table: "Boleto");

            migrationBuilder.CreateIndex(
                name: "IX_Boleto_ReservaId",
                table: "Boleto",
                column: "ReservaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Boleto_ReservaId",
                table: "Boleto");

            migrationBuilder.CreateIndex(
                name: "IX_Boleto_ReservaId",
                table: "Boleto",
                column: "ReservaId");
        }
    }
}
