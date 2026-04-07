using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Tests
{
    public static class TestBazaUMemoriji
    {
        public static (VozAppContext context, SqliteConnection connection) KreirajContext()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var opcije = new DbContextOptionsBuilder<VozAppContext>().UseSqlite(connection).Options;

            var context = new VozAppContext(opcije);
            context.Database.EnsureCreated();

            return (context, connection);
        }

        public static async Task PopuniSvePodatkeAsync(VozAppContext context)
        {
            var datum = DateTime.Today;

            if (!await context.Stanica.AnyAsync())
            {
                context.Stanica.AddRange(
                    new Stanica(1, "Beograd Centar", "Savski Venac"),
                    new Stanica(2, "Novi Sad", "Novi Sad"),
                    new Stanica(3, "Subotica", "Subotica"),
                    new Stanica(4, "Nis", "Nis"),
                    new Stanica(5, "Kragujevac", "Sumadija"),
                    new Stanica(6, "Uzice", "Zlatibor"),
                    new Stanica(7, "Kraljevo", "Raska"),
                    new Stanica(8, "Cacak", "Moravica")
                );
            }

            if (!await context.TipVoza.AnyAsync())
            {
                context.TipVoza.AddRange(
                    new TipVoza(1, "SOKO", "Brzi voz"),
                    new TipVoza(2, "REGIO", "Regionalni voz"),
                    new TipVoza(3, "TERETNI", "Teretni voz"),
                    new TipVoza(4, "NOCNI", "Nocni voz")
                );
            }

            if (!await context.Voz.AnyAsync())
            {
                context.Voz.AddRange(
                    new Voz(1, "IC", "SRB-IC-001", true, 1),
                    new Voz(2, "REG-1", "SRB-REG-001", true, 2),
                    new Voz(3, "REG-2", "SRB-REG-002", true, 2),
                    new Voz(4, "TER-1", "SRB-TER-001", true, 3),
                    new Voz(5, "NIGHT-1", "SRB-NOC-001", true, 4),
                    new Voz(6, "IC-2", "SRB-IC-002", true, 1),
                    new Voz(7, "REG-JUG", "SRB-REG-003", true, 2),
                    new Voz(8, "TER-2", "SRB-TER-002", true, 3)
                );
            }

            if (!await context.Linija.AnyAsync())
            {
                context.Linija.AddRange(
                    new Linija("Beograd centar - Novi Sad", 15) { Id = 1 },
                    new Linija("Novi Sad - Subotica", 14) { Id = 2 },
                    new Linija("Beograd centar - Nis", 17) { Id = 3 },
                    new Linija("Kragujevac - Uzice", 13) { Id = 4 },
                    new Linija("Kraljevo - Cacak", 11) { Id = 5 },
                    new Linija("Beograd centar - Kragujevac", 16) { Id = 6 }
                );
            }

            if (!await context.StanicaLinija.AnyAsync())
            {
                context.StanicaLinija.AddRange(
                    new StanicaLinija(0, 1, 1, 1),
                    new StanicaLinija(90, 2, 2, 1),

                    new StanicaLinija(0, 1, 2, 2),
                    new StanicaLinija(120, 2, 3, 2),

                    new StanicaLinija(0, 1, 1, 3),
                    new StanicaLinija(85, 2, 5, 3),
                    new StanicaLinija(180, 3, 4, 3),

                    new StanicaLinija(0, 1, 5, 4),
                    new StanicaLinija(70, 2, 8, 4),
                    new StanicaLinija(145, 3, 6, 4),

                    new StanicaLinija(0, 1, 7, 5),
                    new StanicaLinija(55, 2, 8, 5),

                    new StanicaLinija(0, 1, 1, 6),
                    new StanicaLinija(95, 2, 5, 6)
                );
            }

            if (!await context.Raspored.AnyAsync())
            {
                context.Raspored.AddRange(
                    new Raspored { Id = 1, Linija_id = 1, Voz_id = 1, Vreme_polaska = datum.AddHours(8) },
                    new Raspored { Id = 2, Linija_id = 1, Voz_id = 6, Vreme_polaska = datum.AddHours(12) },
                    new Raspored { Id = 3, Linija_id = 1, Voz_id = 1, Vreme_polaska = datum.AddDays(1).AddHours(8) },

                    new Raspored { Id = 4, Linija_id = 2, Voz_id = 2, Vreme_polaska = datum.AddHours(10) },
                    new Raspored { Id = 5, Linija_id = 2, Voz_id = 3, Vreme_polaska = datum.AddDays(1).AddHours(9) },

                    new Raspored { Id = 6, Linija_id = 3, Voz_id = 1, Vreme_polaska = datum.AddHours(7) },
                    new Raspored { Id = 7, Linija_id = 3, Voz_id = 7, Vreme_polaska = datum.AddHours(15) },
                    new Raspored { Id = 8, Linija_id = 3, Voz_id = 5, Vreme_polaska = datum.AddHours(22) },
                    new Raspored { Id = 9, Linija_id = 3, Voz_id = 5, Vreme_polaska = datum.AddDays(1).AddHours(22) },

                    new Raspored { Id = 10, Linija_id = 4, Voz_id = 3, Vreme_polaska = datum.AddHours(9) },
                    new Raspored { Id = 11, Linija_id = 4, Voz_id = 3, Vreme_polaska = datum.AddHours(17) },
                    new Raspored { Id = 12, Linija_id = 4, Voz_id = 8, Vreme_polaska = datum.AddDays(1).AddHours(5) },

                    new Raspored { Id = 13, Linija_id = 5, Voz_id = 7, Vreme_polaska = datum.AddHours(6) },
                    new Raspored { Id = 14, Linija_id = 5, Voz_id = 7, Vreme_polaska = datum.AddHours(14) },

                    new Raspored { Id = 15, Linija_id = 6, Voz_id = 2, Vreme_polaska = datum.AddHours(11) },
                    new Raspored { Id = 16, Linija_id = 6, Voz_id = 2, Vreme_polaska = datum.AddDays(1).AddHours(11) }
                );
            }

            if (!await context.Korisnik.AnyAsync())
            {
                context.Putnik.AddRange(
                    new Putnik("Petar", "Petrovic", "petar@gmail.com", "0611234567", "putnik123") { Id = 101 },
                    new Putnik("Ana", "Anic", "ana@gmail.com", "0629876543", "ana12345") { Id = 102 }
                );

                context.Admin.AddRange(
                    new Administrator("Milos", "Milosevic", "milos@gmail.com", "admin123", datum.AddDays(-30)) { Id = 201 },
                    new Administrator("Jelena", "Jovanovic", "jelena@gmail.com", "admin987", datum.AddDays(-120)) { Id = 202 }
                );

                context.Kondukter.AddRange(
                    new Kondukter("Marko", "Markovic", "marko@gmail.com", "kondukter123", "LEG-001") { Id = 301 },
                    new Kondukter("Nikola", "Nikolic", "nikola@gmail.com", "kondukter987", "LEG-002") { Id = 302 }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}
