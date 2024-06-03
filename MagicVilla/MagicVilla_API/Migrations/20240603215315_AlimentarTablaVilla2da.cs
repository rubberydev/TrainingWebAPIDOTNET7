using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla2da : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion", "Tarifa" },
                values: new object[] { new DateTime(2024, 6, 3, 16, 53, 15, 385, DateTimeKind.Local).AddTicks(4553), new DateTime(2024, 6, 3, 16, 53, 15, 385, DateTimeKind.Local).AddTicks(4539), 200.0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion", "Tarifa" },
                values: new object[] { new DateTime(2024, 6, 3, 16, 53, 15, 385, DateTimeKind.Local).AddTicks(4559), new DateTime(2024, 6, 3, 16, 53, 15, 385, DateTimeKind.Local).AddTicks(4558), 150.0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion", "Tarifa" },
                values: new object[] { new DateTime(2024, 6, 3, 16, 53, 15, 385, DateTimeKind.Local).AddTicks(4563), new DateTime(2024, 6, 3, 16, 53, 15, 385, DateTimeKind.Local).AddTicks(4562), 200.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion", "Tarifa" },
                values: new object[] { new DateTime(2024, 6, 3, 16, 49, 21, 54, DateTimeKind.Local).AddTicks(6476), new DateTime(2024, 6, 3, 16, 49, 21, 54, DateTimeKind.Local).AddTicks(6462), 0.0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion", "Tarifa" },
                values: new object[] { new DateTime(2024, 6, 3, 16, 49, 21, 54, DateTimeKind.Local).AddTicks(6481), new DateTime(2024, 6, 3, 16, 49, 21, 54, DateTimeKind.Local).AddTicks(6480), 0.0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion", "Tarifa" },
                values: new object[] { new DateTime(2024, 6, 3, 16, 49, 21, 54, DateTimeKind.Local).AddTicks(6485), new DateTime(2024, 6, 3, 16, 49, 21, 54, DateTimeKind.Local).AddTicks(6484), 0.0 });
        }
    }
}
