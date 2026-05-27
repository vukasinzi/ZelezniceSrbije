using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZelezniceSrbije.Migrations
{
    /// <inheritdoc />
    public partial class migracija002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RasporedSablon",
                columns: new[] { "id", "aktivan", "linija_id", "voz_id", "vreme_polaska_time" },
                values: new object[,]
                {
                    { 1, true, 1, 1, new TimeSpan(0, 7, 0, 0, 0) },
                    { 2, true, 1, 2, new TimeSpan(0, 13, 0, 0, 0) },
                    { 3, true, 1, 1, new TimeSpan(0, 19, 0, 0, 0) },
                    { 4, true, 2, 2, new TimeSpan(0, 9, 0, 0, 0) },
                    { 5, true, 2, 1, new TimeSpan(0, 15, 0, 0, 0) },
                    { 6, true, 2, 2, new TimeSpan(0, 21, 0, 0, 0) },
                    { 7, true, 3, 1, new TimeSpan(0, 6, 0, 0, 0) },
                    { 8, true, 3, 2, new TimeSpan(0, 14, 0, 0, 0) },
                    { 9, true, 4, 2, new TimeSpan(0, 9, 30, 0, 0) },
                    { 10, true, 4, 1, new TimeSpan(0, 17, 30, 0, 0) },
                    { 11, true, 5, 1, new TimeSpan(0, 7, 15, 0, 0) },
                    { 12, true, 5, 2, new TimeSpan(0, 16, 15, 0, 0) },
                    { 13, true, 6, 1, new TimeSpan(0, 5, 0, 0, 0) },
                    { 14, true, 6, 2, new TimeSpan(0, 15, 0, 0, 0) },
                    { 15, true, 7, 2, new TimeSpan(0, 9, 30, 0, 0) },
                    { 16, true, 7, 1, new TimeSpan(0, 19, 30, 0, 0) },
                    { 17, true, 8, 1, new TimeSpan(0, 6, 30, 0, 0) },
                    { 18, true, 8, 2, new TimeSpan(0, 12, 30, 0, 0) },
                    { 19, true, 8, 1, new TimeSpan(0, 18, 30, 0, 0) },
                    { 20, true, 9, 2, new TimeSpan(0, 7, 30, 0, 0) },
                    { 21, true, 9, 1, new TimeSpan(0, 13, 30, 0, 0) },
                    { 22, true, 9, 2, new TimeSpan(0, 19, 30, 0, 0) },
                    { 23, true, 10, 1, new TimeSpan(0, 7, 0, 0, 0) },
                    { 24, true, 10, 2, new TimeSpan(0, 14, 0, 0, 0) },
                    { 25, true, 11, 2, new TimeSpan(0, 9, 0, 0, 0) },
                    { 26, true, 11, 1, new TimeSpan(0, 16, 0, 0, 0) },
                    { 27, true, 12, 1, new TimeSpan(0, 8, 0, 0, 0) },
                    { 28, true, 12, 2, new TimeSpan(0, 16, 0, 0, 0) },
                    { 29, true, 13, 2, new TimeSpan(0, 10, 15, 0, 0) },
                    { 30, true, 13, 1, new TimeSpan(0, 18, 15, 0, 0) },
                    { 31, true, 14, 1, new TimeSpan(0, 7, 45, 0, 0) },
                    { 32, true, 14, 2, new TimeSpan(0, 13, 45, 0, 0) },
                    { 33, true, 15, 2, new TimeSpan(0, 10, 0, 0, 0) },
                    { 34, true, 15, 1, new TimeSpan(0, 16, 0, 0, 0) },
                    { 35, true, 16, 1, new TimeSpan(0, 6, 0, 0, 0) },
                    { 36, true, 16, 2, new TimeSpan(0, 14, 15, 0, 0) },
                    { 37, true, 17, 2, new TimeSpan(0, 9, 45, 0, 0) },
                    { 38, true, 17, 1, new TimeSpan(0, 18, 0, 0, 0) },
                    { 39, true, 18, 1, new TimeSpan(0, 7, 10, 0, 0) },
                    { 40, true, 18, 2, new TimeSpan(0, 15, 10, 0, 0) },
                    { 41, true, 19, 2, new TimeSpan(0, 9, 0, 0, 0) },
                    { 42, true, 19, 1, new TimeSpan(0, 17, 0, 0, 0) },
                    { 43, true, 20, 1, new TimeSpan(0, 8, 30, 0, 0) },
                    { 44, true, 20, 2, new TimeSpan(0, 20, 30, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "RasporedSablon",
                keyColumn: "id",
                keyValue: 44);
        }
    }
}
