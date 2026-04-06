using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Tests.KorisnikTest
{
    public class KorisnikServiceTest : IDisposable
    {
        private readonly VozAppContext context;
        private readonly SqliteConnection connection;
        private readonly KorisnikService servis;

        public KorisnikServiceTest()
        {
            var db = TestBazaUMemoriji.KreirajContext();
            context = db.context;
            connection = db.connection;
            var repo = new KorisnikRepository(context);
            servis = new KorisnikService(repo);
        }

        public void Dispose()
        {
            context.Dispose();
            connection.Dispose();
        }

        [Theory]
        [InlineData("Marko", "Markovic", "marko@gmail.com", "0611234567", "sifra123", true)]
        [InlineData("", "Markovic", "marko2@gmail.com", "0611234567", "sifra123", false)]
        [InlineData("Marko", "", "marko3@gmail.com", "0611234567", "sifra123", false)]
        [InlineData("Marko", "Markovic", "", "0611234567", "sifra123", false)]
        [InlineData("Marko", "Markovic", "nijemail", "0611234567", "sifra123", false)]
        [InlineData("ImeKojeJePredugo123456", "Markovic", "marko4@gmail.com", "0611234567", "sifra123", false)]
        [InlineData("Marko", "PrezimeKojeJePredugoO", "marko5@gmail.com", "0611234567", "sifra123", false)]
        [InlineData("ImeKojeJePrecizno20k", "Markovic", "marko6@gmail.com", "0611234567", "sifra123", true)]
        [InlineData("Ime", "Prezime", "marecar@gmail.com", "421dsada4", "sifra", false)]
        public async Task Registracija_Test(string ime, string prezime, string email, string brojTelefona, string lozinka, bool trebaDaUspe)
        {
            var p = new Putnik(ime, prezime, email, brojTelefona, lozinka);
            var rezultat = await servis.RegistrujAsync(p);

            if (trebaDaUspe)
                Assert.NotNull(rezultat);
            else
                Assert.Null(rezultat);
        }

        [Fact]
        public async Task Registracija_DupliMejlovi_Test()
        {
            var putnik1 = new Putnik("Marko", "Markovic", "marko@gmail.com", "0611234567", "sifra123");
            var putnik2 = new Putnik("Ana", "Anic", "marko@gmail.com", "0621234567", "sifra123");

            await servis.RegistrujAsync(putnik1);
            var rezultat = await servis.RegistrujAsync(putnik2);

            Assert.Null(rezultat);
        }

        [Fact]
        public async Task Login_NepostojeciLogInTest()
        {
            var putnik1 = new Putnik("Marko", "Markovic", "marko@gmail.com", "06030120", "marko123");
            var putnik2 = new Putnik("Ana", "Anic", "ana@gmail.com", "07463063", "ana123");

            await servis.RegistrujAsync(putnik1);
            await servis.RegistrujAsync(putnik2);

            var rez = await servis.LogInAsync("ana@nepostojeca.com", "ana123");
            Assert.Null(rez);
        }

        [Fact]
        public async Task Login_PostojeciLogInTest()
        {
            var putnik1 = new Putnik("Marko", "Markovic", "marko@gmail.com", "06030120", "marko123");
            var putnik2 = new Putnik("Ana", "Anic", "ana@gmail.com", "07463063", "ana123");

            await servis.RegistrujAsync(putnik1);
            await servis.RegistrujAsync(putnik2);

            var rez = await servis.LogInAsync("ana@gmail.com", "ana123");
            Assert.NotNull(rez);
        }

        [Fact]
        public async Task UcitajSveAdmineTest()
        {
            var admin1 = new Administrator("Milos", "Milosevic", "milos@gmail.com", "lozinkalozinka", DateTime.Now);
            var admin2 = new Administrator("Borko", "Borkovic", "borko@gmail.com", "lozinka2lozinka", DateTime.Now);

            context.Admin.AddRange(admin1, admin2);
            await context.SaveChangesAsync();

            List<Administrator> ucitani = await servis.UcitajSveAdmine();

            Assert.Equal(2, ucitani.Count);
            Assert.Contains(ucitani, x => x.Email == admin1.Email);
            Assert.Contains(ucitani, x => x.Email == admin2.Email);
        }

        [Fact]
        public async Task UcitajSveKonduktereTest()
        {
            var kondukter1 = new Kondukter("Milos", "Milosevic", "milos@gmail.com", "lozinkalozinka", "111111");
            var kondukter2 = new Kondukter("Borko", "Borkovic", "borko@gmail.com", "lozinka2lozinka", "222222");

            context.Kondukter.AddRange(kondukter1, kondukter2);
            await context.SaveChangesAsync();

            List<Kondukter> ucitani = await servis.UcitajSveKonduktere();

            Assert.Equal(2, ucitani.Count);
            Assert.Contains(ucitani, x => x.Email == kondukter1.Email);
            Assert.Contains(ucitani, x => x.Email == kondukter2.Email);
        }

        [Theory]
        [InlineData(true, "putnik1@gmail.com", "Kondukter", "", "", false)]
        [InlineData(true, "putnik2@gmail.com", "Administrator", "", "123456", false)]
        [InlineData(true, "putnik3@gmail.com", "Kondukter", "", "123456", true)]
        [InlineData(true, "putnik4@gmail.com", "Administrator", "2024-01-01", "", true)]
        [InlineData(false, "nepostoji@gmail.com", "Kondukter", "2024-01-01", "123", false)]
        [InlineData(true, "putnik4@gmail.com", "Masinovodja", "2024-01-01", "", true)]
        [InlineData(true, "putnik4@gmail.com", "Stjuart", "2024-01-01", "", true)]

        public async Task PromovisiUloguTest(bool korisnikPostoji, string email, string uloga, string datumIso, string brojLegitimacije, bool trebaDaUspe)
        {
            if (korisnikPostoji)
            {
                var putnik = new Putnik("Pera", "Peric", email, "0612345678", "sifra123");
                context.Putnik.Add(putnik);
                await context.SaveChangesAsync();
            }

            DateTime? datum = null;
            if (!string.IsNullOrWhiteSpace(datumIso))
                datum = DateTime.ParseExact(datumIso, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var rezultat = await servis.PromovisiUlogu(email, uloga, datum, brojLegitimacije);
            Assert.Equal(trebaDaUspe, rezultat);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public async Task UkloniAdministratoraTest(int id, bool trebaDaUspe)
        {
            var admin1 = new Administrator("Milos", "Milosevic", "milos@gmail.com", "lozinka123", DateTime.Now) { Id = 1 };
            var admin2 = new Administrator("Ana", "Anic", "ana@gmail.com", "lozinka456", DateTime.Now) { Id = 2 };

            context.Admin.AddRange(admin1, admin2);
            await context.SaveChangesAsync();

            var ok = await servis.UkloniAdministratora(id);
            Assert.Equal(trebaDaUspe, ok);

            var admini = await servis.UcitajSveAdmine();
            if (trebaDaUspe)
            {
                Assert.Single(admini);
                Assert.Equal(2, admini[0].Id);
            }
            else
            {
                Assert.Equal(2, admini.Count);
            }
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public async Task UkloniKondukteraTest(int id, bool trebaDaUspe)
        {
            var kondukter1 = new Kondukter("Borko", "Borkovic", "borko@gmail.com", "lozinka123", "99999") { Id = 1 };
            var kondukter2 = new Kondukter("Jovan", "Jovanovic", "jovan@gmail.com", "lozinka456", "88888") { Id = 2 };

            context.Kondukter.AddRange(kondukter1, kondukter2);
            await context.SaveChangesAsync();

            var ok = await servis.UkloniKonduktera(id);
            Assert.Equal(trebaDaUspe, ok);

            var kondukteri = await servis.UcitajSveKonduktere();
            if (trebaDaUspe)
            {
                Assert.Single(kondukteri);
                Assert.Equal(2, kondukteri[0].Id);
            }
            else
            {
                Assert.Equal(2, kondukteri.Count);
            }
        }

        [Theory]
        [InlineData(1, "Pera", "Peric", "pera@mail.com", "2024-01-01", true)]
        [InlineData(1, "", "Peric", "pera@mail.com", "2024-01-01", false)]
        [InlineData(1, "Pera", "", "pera@mail.com", "2024-01-01", false)]
        [InlineData(1, "Pera", "Peric", "", "2024-01-01", false)]
        [InlineData(1, "Pera", "Peric", "pera@mail.com", "", false)]
        [InlineData(1, "Pera", "Peric", "pera@mail.com", "2100-01-01", false)]
        [InlineData(1, null, "Peric", "pera@mail.com", "2100-01-01", false)]
        [InlineData(1, "Pera", null, "pera@mail.com", "2100-01-01", false)]
        [InlineData(1, "Pera","Peric", null, null, false)]
        public async Task IzmeniAdministratoraTest(int id, string ime, string prezime, string email, string datumIso, bool trebaDaUspe)
        {
            var admin = new Administrator("Stari", "Ime", "stari@gmail.com", "lozinka123", new DateTime(2020, 1, 1)) { Id = 1 };
            context.Admin.Add(admin);
            await context.SaveChangesAsync();

            DateTime? datum = null;
            if (!string.IsNullOrWhiteSpace(datumIso))
                datum = DateTime.ParseExact(datumIso, "yyyy-MM-dd", CultureInfo.InvariantCulture);  

            var ok = await servis.IzmeniAdministratora(id, ime, prezime, email, datum);
            Assert.Equal(trebaDaUspe, ok);

            if (trebaDaUspe)
            {
                var nasAdmin = await context.Admin.FirstAsync(x => x.Id == id);
                Assert.Equal(ime, nasAdmin.Ime);
            }
        }

        [Theory]
        [InlineData(1, "Pera", "Peric", "pera@mail.com", "12345", true)]
        [InlineData(1, "", "Peric", "pera@mail.com", "12345", false)]
        [InlineData(1, "Pera", "", "pera@mail.com", "12345", false)]
        [InlineData(1, "Pera", "Peric", "", "12345", false)]
        [InlineData(1, "Pera", "Peric", "pera@mail.com", "", false)]
        [InlineData(1, "Pera", null, "pera@mail.com", "2100-01-01", false)]
        [InlineData(1, "Pera", "Peric", null, null, false)]
        public async Task IzmeniKondukteraTest(int id, string ime, string prezime, string email, string brojLegitimacije, bool trebaDaUspe)
        {
            var kondukter = new Kondukter("Stari", "Ime", "stari@gmail.com", "lozinka123", "00000") { Id = 1 };
            context.Kondukter.Add(kondukter);
            await context.SaveChangesAsync();

            var ok = await servis.IzmeniKonduktera(id, ime, prezime, email, brojLegitimacije);
            Assert.Equal(trebaDaUspe, ok);

            if (trebaDaUspe)
            {
                var nasKondukter = await context.Kondukter.FirstAsync(x => x.Id == id);
                Assert.Equal(ime, nasKondukter.Ime);
            }
        }
    }
}
