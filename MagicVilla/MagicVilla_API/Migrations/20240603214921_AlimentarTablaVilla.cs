using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "any", "Detalle de la villa 1", new DateTime(2024, 6, 3, 16, 49, 21, 54, DateTimeKind.Local).AddTicks(6476), new DateTime(2024, 6, 3, 16, 49, 21, 54, DateTimeKind.Local).AddTicks(6462), "", 50, "Villa 1", 5, 0.0 },
                    { 2, "any", "Detalle de la villa premium", new DateTime(2024, 6, 3, 16, 49, 21, 54, DateTimeKind.Local).AddTicks(6481), new DateTime(2024, 6, 3, 16, 49, 21, 54, DateTimeKind.Local).AddTicks(6480), "", 50, "Premium vista a la playa", 5, 0.0 },
                    { 3, "any", "Detalle de la villa 3", new DateTime(2024, 6, 3, 16, 49, 21, 54, DateTimeKind.Local).AddTicks(6485), new DateTime(2024, 6, 3, 16, 49, 21, 54, DateTimeKind.Local).AddTicks(6484), "", 50, "Villa 3", 5, 0.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
