using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZelezniceSrbije.Migrations
{
    /// <inheritdoc />
    public partial class migracija001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Linija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cena_po_minutu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linija", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stanica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipVoza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipVoza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    datum_zaposlenja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrator_Korisnik_Id",
                        column: x => x.Id,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kondukter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    broj_legitimacije = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kondukter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kondukter_Korisnik_Id",
                        column: x => x.Id,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Putnik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    broj_telefona = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Putnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Putnik_Korisnik_Id",
                        column: x => x.Id,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StanicaLinija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vreme_od_polaska = table.Column<int>(type: "int", nullable: false),
                    Redosled = table.Column<int>(type: "int", nullable: false),
                    Stanica_id = table.Column<int>(type: "int", nullable: false),
                    Linija_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanicaLinija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StanicaLinija_Linija_Linija_id",
                        column: x => x.Linija_id,
                        principalTable: "Linija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StanicaLinija_Stanica_Stanica_id",
                        column: x => x.Stanica_id,
                        principalTable: "Stanica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Voz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Serijski_broj = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aktivan = table.Column<bool>(type: "bit", nullable: false),
                    Tip_voza_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voz_TipVoza_Tip_voza_id",
                        column: x => x.Tip_voza_id,
                        principalTable: "TipVoza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RasporedSablon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    linija_id = table.Column<int>(type: "int", nullable: false),
                    voz_id = table.Column<int>(type: "int", nullable: false),
                    vreme_polaska_time = table.Column<TimeSpan>(type: "time", nullable: false),
                    aktivan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RasporedSablon", x => x.id);
                    table.ForeignKey(
                        name: "FK_RasporedSablon_Linija_linija_id",
                        column: x => x.linija_id,
                        principalTable: "Linija",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RasporedSablon_Voz_voz_id",
                        column: x => x.voz_id,
                        principalTable: "Voz",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Raspored",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vreme_polaska = table.Column<DateTime>(type: "datetime2", nullable: false),
                    linija_id = table.Column<int>(type: "int", nullable: false),
                    voz_id = table.Column<int>(type: "int", nullable: false),
                    sablon_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raspored", x => x.id);
                    table.ForeignKey(
                        name: "FK_Raspored_Linija_linija_id",
                        column: x => x.linija_id,
                        principalTable: "Linija",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Raspored_RasporedSablon_sablon_id",
                        column: x => x.sablon_id,
                        principalTable: "RasporedSablon",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Raspored_Voz_voz_id",
                        column: x => x.voz_id,
                        principalTable: "Voz",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Karta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ocitana = table.Column<bool>(type: "bit", nullable: false),
                    Datum_ocitavanja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Putnik_id = table.Column<int>(type: "int", nullable: false),
                    Raspored_id = table.Column<int>(type: "int", nullable: false),
                    Polaziste = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Odrediste = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Linija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tip_voza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kondukter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trajanje_min = table.Column<int>(type: "int", nullable: false),
                    Vreme_polaska = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vreme_dolaska = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Qr_token = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Karta_Korisnik_Putnik_id",
                        column: x => x.Putnik_id,
                        principalTable: "Korisnik",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Karta_Raspored_Raspored_id",
                        column: x => x.Raspored_id,
                        principalTable: "Raspored",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "Linija",
                columns: new[] { "Id", "Cena_po_minutu", "Naziv" },
                values: new object[,]
                {
                    { 1, 12, "Beograd Centar – Novi Sad" },
                    { 2, 12, "Novi Sad – Beograd Centar" },
                    { 3, 18, "Subotica – Beograd Centar" },
                    { 4, 16, "Beograd Centar – Subotica" },
                    { 5, 16, "Subotica – Beograd Centar" },
                    { 6, 8, "Zemun – Niš" },
                    { 7, 8, "Niš – Zemun" },
                    { 8, 8, "Zemun – Pančevo" },
                    { 9, 8, "Pančevo – Zemun" },
                    { 10, 8, "Zemun – Vršac" },
                    { 11, 8, "Vršac – Zemun" },
                    { 12, 8, "Zemun – Šid" },
                    { 13, 8, "Šid – Zemun" },
                    { 14, 8, "Novi Sad – Šid" },
                    { 15, 8, "Šid – Novi Sad" },
                    { 16, 8, "Zemun – Užice" },
                    { 17, 8, "Užice – Zemun" },
                    { 18, 8, "Zemun – Valjevo" },
                    { 19, 8, "Valjevo – Zemun" },
                    { 20, 12, "Beograd Centar - Vršac" }
                });

            migrationBuilder.InsertData(
                table: "Stanica",
                columns: new[] { "Id", "Naziv", "Region" },
                values: new object[,]
                {
                    { 1, "Beograd Centar", "Beograd" },
                    { 2, "Novi Beograd", "Beograd" },
                    { 3, "Zemun", "Beograd" },
                    { 4, "Batajnica", "Beograd" },
                    { 5, "Nova Pazova", "Srem" },
                    { 6, "Stara Pazova", "Srem" },
                    { 7, "Inđija", "Srem" },
                    { 8, "Beška", "Srem" },
                    { 9, "Sremski Karlovci", "Srem" },
                    { 10, "Petrovaradin", "Bačka" },
                    { 11, "Novi Sad", "Bačka" },
                    { 12, "Zmajevo", "Bačka" },
                    { 13, "Vrbas Nova", "Bačka" },
                    { 14, "Lovćenac-Mali Iđoš", "Bačka" },
                    { 15, "Bačka Topola", "Bačka" },
                    { 16, "Žednik", "Bačka" },
                    { 17, "Subotica", "Bačka" },
                    { 18, "Pančevo", "Banat" },
                    { 19, "Vršac", "Banat" },
                    { 20, "Ruma", "Srem" },
                    { 21, "Sremska Mitrovica", "Srem" },
                    { 22, "Šid", "Srem" },
                    { 23, "Lajkovac", "Mačva i Kolubara" },
                    { 24, "Valjevo", "Mačva i Kolubara" },
                    { 25, "Požega", "Zlatibor" },
                    { 26, "Užice", "Zlatibor" },
                    { 27, "Mladenovac", "Šumadija" },
                    { 28, "Lapovo", "Šumadija" },
                    { 29, "Jagodina", "Pomoravlje" },
                    { 30, "Ćuprija", "Pomoravlje" },
                    { 31, "Paraćin", "Pomoravlje" },
                    { 32, "Aleksinac", "Južna Srbija" },
                    { 33, "Niš", "Južna Srbija" }
                });

            migrationBuilder.InsertData(
                table: "TipVoza",
                columns: new[] { "Id", "Naziv", "Opis" },
                values: new object[,]
                {
                    { 1, "Soko", "Najbrzi vozicc" },
                    { 2, "InterCity", "Brzi međugradski voz" },
                    { 3, "Regio Express", "Polubrzi voz između malo većih mesta" },
                    { 4, "Regio", "Regionalni voz. Staje na svaku banderu" }
                });

            migrationBuilder.InsertData(
                table: "StanicaLinija",
                columns: new[] { "Id", "Linija_id", "Redosled", "Stanica_id", "Vreme_od_polaska" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 0 },
                    { 2, 1, 2, 2, 5 },
                    { 3, 1, 3, 3, 10 },
                    { 4, 1, 4, 4, 18 },
                    { 5, 1, 5, 5, 25 },
                    { 6, 1, 6, 6, 30 },
                    { 7, 1, 7, 7, 38 },
                    { 8, 1, 8, 8, 45 },
                    { 9, 1, 9, 9, 55 },
                    { 10, 1, 10, 10, 62 },
                    { 11, 1, 11, 11, 68 },
                    { 12, 2, 1, 11, 0 },
                    { 13, 2, 2, 10, 6 },
                    { 14, 2, 3, 9, 13 },
                    { 15, 2, 4, 8, 23 },
                    { 16, 2, 5, 7, 30 },
                    { 17, 2, 6, 6, 38 },
                    { 18, 2, 7, 5, 43 },
                    { 19, 2, 8, 4, 50 },
                    { 20, 2, 9, 3, 58 },
                    { 21, 2, 10, 2, 63 },
                    { 22, 2, 11, 1, 68 },
                    { 23, 3, 1, 17, 0 },
                    { 24, 3, 2, 16, 10 },
                    { 25, 3, 3, 15, 25 },
                    { 26, 3, 4, 14, 35 },
                    { 27, 3, 5, 13, 45 },
                    { 28, 3, 6, 12, 55 },
                    { 29, 3, 7, 11, 75 },
                    { 30, 3, 8, 10, 81 },
                    { 31, 3, 9, 9, 88 },
                    { 32, 3, 10, 8, 98 },
                    { 33, 3, 11, 7, 105 },
                    { 34, 3, 12, 6, 113 },
                    { 35, 3, 13, 5, 118 },
                    { 36, 3, 14, 4, 125 },
                    { 37, 3, 15, 3, 133 },
                    { 38, 3, 16, 2, 138 },
                    { 39, 3, 17, 1, 143 },
                    { 40, 4, 1, 1, 0 },
                    { 41, 4, 2, 2, 5 },
                    { 42, 4, 3, 3, 10 },
                    { 43, 4, 4, 4, 18 },
                    { 44, 4, 5, 5, 25 },
                    { 45, 4, 6, 6, 30 },
                    { 46, 4, 7, 7, 38 },
                    { 47, 4, 8, 8, 45 },
                    { 48, 4, 9, 9, 55 },
                    { 49, 4, 10, 10, 62 },
                    { 50, 4, 11, 11, 68 },
                    { 51, 4, 12, 12, 88 },
                    { 52, 4, 13, 13, 98 },
                    { 53, 4, 14, 14, 108 },
                    { 54, 4, 15, 15, 118 },
                    { 55, 4, 16, 16, 133 },
                    { 56, 4, 17, 17, 143 },
                    { 57, 5, 1, 17, 0 },
                    { 58, 5, 2, 15, 20 },
                    { 59, 5, 3, 13, 35 },
                    { 60, 5, 4, 11, 60 },
                    { 61, 5, 5, 1, 110 },
                    { 62, 6, 1, 3, 0 },
                    { 63, 6, 2, 2, 5 },
                    { 64, 6, 3, 1, 10 },
                    { 65, 6, 4, 27, 50 },
                    { 66, 6, 5, 28, 80 },
                    { 67, 6, 6, 29, 95 },
                    { 68, 6, 7, 30, 105 },
                    { 69, 6, 8, 31, 115 },
                    { 70, 6, 9, 32, 135 },
                    { 71, 6, 10, 33, 160 },
                    { 72, 7, 1, 33, 0 },
                    { 73, 7, 2, 32, 25 },
                    { 74, 7, 3, 31, 45 },
                    { 75, 7, 4, 30, 55 },
                    { 76, 7, 5, 29, 65 },
                    { 77, 7, 6, 28, 80 },
                    { 78, 7, 7, 27, 110 },
                    { 79, 7, 8, 1, 150 },
                    { 80, 7, 9, 2, 155 },
                    { 81, 7, 10, 3, 160 },
                    { 82, 8, 1, 3, 0 },
                    { 83, 8, 2, 2, 5 },
                    { 84, 8, 3, 1, 10 },
                    { 85, 8, 4, 18, 30 },
                    { 86, 9, 1, 18, 0 },
                    { 87, 9, 2, 1, 20 },
                    { 88, 9, 3, 2, 25 },
                    { 89, 9, 4, 3, 30 },
                    { 90, 10, 1, 3, 0 },
                    { 91, 10, 2, 2, 5 },
                    { 92, 10, 3, 1, 10 },
                    { 93, 10, 4, 18, 30 },
                    { 94, 10, 5, 19, 75 },
                    { 95, 11, 1, 19, 0 },
                    { 96, 11, 2, 18, 45 },
                    { 97, 11, 3, 1, 65 },
                    { 98, 11, 4, 2, 70 },
                    { 99, 11, 5, 3, 75 },
                    { 100, 12, 1, 3, 0 },
                    { 101, 12, 2, 4, 8 },
                    { 102, 12, 3, 5, 15 },
                    { 103, 12, 4, 6, 20 },
                    { 104, 12, 5, 20, 45 },
                    { 105, 12, 6, 21, 60 },
                    { 106, 12, 7, 22, 80 },
                    { 107, 13, 1, 22, 0 },
                    { 108, 13, 2, 21, 20 },
                    { 109, 13, 3, 20, 35 },
                    { 110, 13, 4, 6, 60 },
                    { 111, 13, 5, 5, 65 },
                    { 112, 13, 6, 4, 72 },
                    { 113, 13, 7, 3, 80 },
                    { 114, 14, 1, 11, 0 },
                    { 115, 14, 2, 10, 6 },
                    { 116, 14, 3, 9, 13 },
                    { 117, 14, 4, 8, 23 },
                    { 118, 14, 5, 7, 30 },
                    { 119, 14, 6, 6, 38 },
                    { 120, 14, 7, 20, 63 },
                    { 121, 14, 8, 21, 78 },
                    { 122, 14, 9, 22, 98 },
                    { 123, 15, 1, 22, 0 },
                    { 124, 15, 2, 21, 20 },
                    { 125, 15, 3, 20, 35 },
                    { 126, 15, 4, 6, 60 },
                    { 127, 15, 5, 7, 68 },
                    { 128, 15, 6, 8, 75 },
                    { 129, 15, 7, 9, 85 },
                    { 130, 15, 8, 10, 92 },
                    { 131, 15, 9, 11, 98 },
                    { 132, 16, 1, 3, 0 },
                    { 133, 16, 2, 2, 5 },
                    { 134, 16, 3, 1, 10 },
                    { 135, 16, 4, 23, 60 },
                    { 136, 16, 5, 24, 80 },
                    { 137, 16, 6, 25, 130 },
                    { 138, 16, 7, 26, 150 },
                    { 139, 17, 1, 26, 0 },
                    { 140, 17, 2, 25, 20 },
                    { 141, 17, 3, 24, 70 },
                    { 142, 17, 4, 23, 90 },
                    { 143, 17, 5, 1, 140 },
                    { 144, 17, 6, 2, 145 },
                    { 145, 17, 7, 3, 150 },
                    { 146, 18, 1, 3, 0 },
                    { 147, 18, 2, 2, 5 },
                    { 148, 18, 3, 1, 10 },
                    { 149, 18, 4, 23, 60 },
                    { 150, 18, 5, 24, 80 },
                    { 151, 19, 1, 24, 0 },
                    { 152, 19, 2, 23, 20 },
                    { 153, 19, 3, 1, 70 },
                    { 154, 19, 4, 2, 75 },
                    { 155, 19, 5, 3, 80 },
                    { 156, 20, 1, 1, 0 },
                    { 157, 20, 2, 18, 20 },
                    { 158, 20, 3, 19, 65 }
                });

            migrationBuilder.InsertData(
                table: "Voz",
                columns: new[] { "Id", "Aktivan", "Naziv", "Serijski_broj", "Tip_voza_id" },
                values: new object[,]
                {
                    { 1, true, "Stadler KISS 200", "KISS-200-01", 1 },
                    { 2, true, "Stadler KISS 200-2", "KISS-200-02", 2 },
                    { 3, true, "Stadler FLIRT 3", "FLIRT-3-01", 3 },
                    { 4, true, "Siemens Desiro", "DESIRO-012", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Karta_Putnik_id",
                table: "Karta",
                column: "Putnik_id");

            migrationBuilder.CreateIndex(
                name: "IX_Karta_Raspored_id",
                table: "Karta",
                column: "Raspored_id");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_email",
                table: "Korisnik",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Raspored_linija_id",
                table: "Raspored",
                column: "linija_id");

            migrationBuilder.CreateIndex(
                name: "IX_Raspored_sablon_id",
                table: "Raspored",
                column: "sablon_id");

            migrationBuilder.CreateIndex(
                name: "IX_Raspored_voz_id",
                table: "Raspored",
                column: "voz_id");

            migrationBuilder.CreateIndex(
                name: "IX_RasporedSablon_linija_id",
                table: "RasporedSablon",
                column: "linija_id");

            migrationBuilder.CreateIndex(
                name: "IX_RasporedSablon_voz_id",
                table: "RasporedSablon",
                column: "voz_id");

            migrationBuilder.CreateIndex(
                name: "IX_StanicaLinija_Linija_id",
                table: "StanicaLinija",
                column: "Linija_id");

            migrationBuilder.CreateIndex(
                name: "IX_StanicaLinija_Stanica_id",
                table: "StanicaLinija",
                column: "Stanica_id");

            migrationBuilder.CreateIndex(
                name: "IX_Voz_Tip_voza_id",
                table: "Voz",
                column: "Tip_voza_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "Karta");

            migrationBuilder.DropTable(
                name: "Kondukter");

            migrationBuilder.DropTable(
                name: "Putnik");

            migrationBuilder.DropTable(
                name: "StanicaLinija");

            migrationBuilder.DropTable(
                name: "Raspored");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Stanica");

            migrationBuilder.DropTable(
                name: "RasporedSablon");

            migrationBuilder.DropTable(
                name: "Linija");

            migrationBuilder.DropTable(
                name: "Voz");

            migrationBuilder.DropTable(
                name: "TipVoza");
        }
    }
}
