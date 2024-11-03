using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusReservation.Migrations
{
    public partial class metodospago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Convert existing string values to enum int values
            migrationBuilder.Sql(@"
                UPDATE Reserva
                SET MetodoPago = CASE MetodoPago
                    WHEN 'Tarjeta Credito' THEN 0
                    WHEN 'Tarjeta Metro' THEN 1
                    WHEN 'Efectivo' THEN 2
                    ELSE 0 -- Default to TarjetaCredito if unknown
                END
            ");

            migrationBuilder.AlterColumn<int>(
                name: "MetodoPago",
                table: "Reserva",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MetodoPago",
                table: "Reserva",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // Optionally, convert back to string if needed
            migrationBuilder.Sql(@"
                UPDATE Reserva
                SET MetodoPago = CASE MetodoPago
                    WHEN 0 THEN 'Tarjeta Credito'
                    WHEN 1 THEN 'Tarjeta Metro'
                    WHEN 2 THEN 'Efectivo'
                    ELSE 'TarjetaCredito' -- Default to TarjetaCredito if unknown
                END
            ");
        }
    }
}