using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Tests.KorisnikTest
{
    public class KorisnikServiceTest
    {
        private readonly KorisnikService servis;
        private readonly FakeKorisnikRepository repo;

        public KorisnikServiceTest()
        {
            repo = new FakeKorisnikRepository();
            servis = new KorisnikService(repo);
        }
        [Theory]
        [InlineData("Marko", "Markovic", "marko@gmail.com", "0611234567", "sifra123", true)]   // ispravno
        [InlineData("", "Markovic", "marko2@gmail.com", "0611234567", "sifra123", false)]       // prazno ime
        [InlineData("Marko", "", "marko3@gmail.com", "0611234567", "sifra123", false)]          // prazno prezime
        [InlineData("Marko", "Markovic", "", "0611234567", "sifra123", false)]                  // prazan email
        [InlineData("Marko", "Markovic", "nijemail", "0611234567", "sifra123", false)]          // email bez @
        [InlineData("ImeKojeJePredugo123456", "Markovic", "marko4@gmail.com", "0611234567", "sifra123", false)] // ime > 20
        [InlineData("Marko", "PrezimeKojeJePredugoO", "marko5@gmail.com", "0611234567", "sifra123", false)]      // prezime > 20
        [InlineData("ImeKojeJePrecizno20k", "Markovic", "marko6@gmail.com", "0611234567", "sifra123", true)]  // ime = 20
        [InlineData("Ime", "Prezime", "marecar@gmail.com", "421dsada4", "sifra", false)] 
        public async Task RegistracijaTestovi(string ime, string prezime, string email, string broj_telefona, string lozinka, bool trebaDaUspe)
        {
            Putnik p = new(ime, prezime, email, broj_telefona, lozinka);
            var rezultat = await servis.RegistrujAsync(p);

            if (trebaDaUspe)
                Assert.NotNull(rezultat);
            else
                Assert.Null(rezultat);
        }
        [Fact]
        public async Task DupliMejlovi()
        {

            var putnik1 = new Putnik("Marko", "Markovic", "marko@gmail.com", "0611234567", "sifra");
            var putnik2 = new Putnik("Ana", "Anic", "marko@gmail.com", "0621234567", "sifra");

            await servis.RegistrujAsync(putnik1);
            var rezultat = await servis.RegistrujAsync(putnik2);

            Assert.Null(rezultat);
        }
        [Fact]
        public async Task NepostojeciLogInTest()
        {
     
            var putnik1 = new Putnik("Marko", "Markovic", "marko@gmail.com", "06030120", "marko123");
            var putnik2 = new Putnik("Ana", "Anic", "ana@gmail.com", "07463063", "ana123");
       
            await servis.RegistrujAsync(putnik1);
            await servis.RegistrujAsync(putnik2);

            var lazniPutnik = new Putnik("Ana", "Anic", "ana@nepostojeca.com", "07463063", "ana123");
            var rez = await servis.LogInAsync(lazniPutnik.Email, lazniPutnik.Lozinka);

            Assert.Null(rez);

        }
        [Fact]
        public async Task PostojeciLogInTest()
        {
            var putnik1 = new Putnik("Marko", "Markovic", "marko@gmail.com", "06030120", "marko123");
            var putnik2 = new Putnik("Ana", "Anic", "ana@gmail.com", "07463063", "ana123");

            await servis.RegistrujAsync(putnik1);
            await servis.RegistrujAsync(putnik2);

            var praviPutnik = new Putnik("Ana", "Anic", "ana@gmail.com", "07463063", "ana123");
            var rez = await servis.LogInAsync(praviPutnik.Email, praviPutnik.Lozinka);

            Assert.NotNull(rez);

        }
        [Fact]
        public async Task UcitajSveAdmineTest()
        {
            //postavka !!
            var admin1 = new Administrator("Milos", "Milosevic", "milos@gmail.com", "lozinkalozinka", DateTime.Now);
            var admin2 = new Administrator("Borko", "Borkovic", "borko@gmail.com", "lozinka2lozinka", DateTime.Now);
            repo.admini.AddRange(admin1, admin2);
            //
            List<Administrator> ucitani = await servis.UcitajSveAdmine();
            Assert.Equal(2, ucitani.Count);
            Assert.Equal(admin1.Email, ucitani[0].Email);
            Assert.Equal(admin2.Email, ucitani[1].Email);
        }
        [Fact]
        public async Task UcitajSveKonduktereTest()
        {
            //postavka !!
            var kondukter1 = new Kondukter("Milos", "Milosevic", "milos@gmail.com", "lozinkalozinka", "111111");
            var kondukter2 = new Kondukter("Borko", "Borkovic", "borko@gmail.com", "lozinka2lozinka", "222222");
            repo.kondukteri.AddRange(kondukter1, kondukter2);
            //
            List<Kondukter> ucitani = await servis.UcitajSveKonduktere();
            Assert.Equal(2, ucitani.Count);
            Assert.Equal(kondukter1.Email, ucitani[0].Email);
            Assert.Equal(kondukter2.Email, ucitani[1].Email);
        }
        [Theory]
        [InlineData(true, "putnik1@gmail.com", "Kondukter", "", "", false)]               // fali broj legitimacije
        [InlineData(true, "putnik2@gmail.com", "Administrator", "", "123456", false)]    // fali datum
        [InlineData(true, "putnik3@gmail.com", "Kondukter", "", "123456", true)]         // ispravno kondukter
        [InlineData(true, "putnik4@gmail.com", "Administrator", "2024-01-01", "", true)] // ispravno admin
        [InlineData(false, "nepostoji@gmail.com", "Kondukter", "2024-01-01", "123", false)] // nepostojeci korisnik
        public async Task PromovisiUloguTest(bool korisnikPostoji, string email, string uloga, string datumIso, string brojLegitimacije, bool trebaDaUspe)
        {
            if (korisnikPostoji)
            {
                var putnik = new Putnik("Pera", "Peric", email, "0612345678", "sifra123");
                repo.korisnici.Add(putnik);
            }

            DateTime? datum = null;
            if (!string.IsNullOrWhiteSpace(datumIso))
            {
                datum = DateTime.ParseExact(datumIso, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            var rezultat = await servis.PromovisiUlogu(email, uloga, datum, brojLegitimacije);

            Assert.Equal(trebaDaUspe, rezultat);
        }
        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public async Task UkloniAdministratoraTest(int id, bool trebaDaUspe)
        {
            var admin1 = new Administrator("Milos", "Milosevic", "milos@gmail.com", "lozinka123", DateTime.Now);
            admin1.Id = 1;
            var admin2 = new Administrator("Ana", "Anic", "ana@gmail.com", "lozinka456", DateTime.Now);
            admin2.Id = 2;

            repo.admini.Add(admin1);
            repo.admini.Add(admin2);

            var ok = await servis.UkloniAdministratora(id);

            Assert.Equal(trebaDaUspe, ok);

            if (trebaDaUspe)
            {
                Assert.Single(repo.admini);
                Assert.Equal("Ana", repo.admini[0].Ime);
                Assert.Equal("Anic", repo.admini[0].Prezime);
            }
            else
            {
                Assert.Equal(2, repo.admini.Count);
            }
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public async Task UkloniKondukteraTest(int id, bool trebaDaUspe)
        {
            var kondukter1 = new Kondukter("Borko", "Borkovic", "borko@gmail.com", "lozinka123", "99999");
            kondukter1.Id = 1;
            var kondukter2 = new Kondukter("Jovan", "Jovanovic", "jovan@gmail.com", "lozinka456", "88888");
            kondukter2.Id = 2;

            repo.kondukteri.Add(kondukter1);
            repo.kondukteri.Add(kondukter2);

            var ok = await servis.UkloniKonduktera(id);

            Assert.Equal(trebaDaUspe, ok);

            if (trebaDaUspe)
            {
                Assert.Single(repo.kondukteri);
                Assert.Equal("Jovan", repo.kondukteri[0].Ime);
                Assert.Equal("Jovanovic", repo.kondukteri[0].Prezime);
            }
            else
            {
                Assert.Equal(2, repo.kondukteri.Count);
            }
        }

        [Theory]
        [InlineData(1, "Pera", "Peric", "pera@mail.com", "2024-01-01", true)]
        [InlineData(1, "", "Peric", "pera@mail.com", "2024-01-01", false)]
        [InlineData(1, "Pera", "", "pera@mail.com", "2024-01-01", false)]
        [InlineData(1, "Pera", "Peric", "", "2024-01-01", false)]
        [InlineData(1, "Pera", "Peric", "pera@mail.com", "", false)]
        [InlineData(1, "Pera", "Peric", "pera@mail.com", "2100-01-01", false)]
        public async Task IzmeniAdministratoraTest(int id, string ime, string prezime, string email, string datumIso, bool trebaDaUspe)
        {
            var admin = new Administrator("Stari", "Ime", "stari@gmail.com", "lozinka123", new DateTime(2020, 1, 1));
            admin.Id = 1;
            repo.admini.Add(admin);

            DateTime? datum = null;
            if (!string.IsNullOrWhiteSpace(datumIso))
                datum = DateTime.ParseExact(datumIso, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var ok = await servis.IzmeniAdministratora(id, ime, prezime, email, datum);
            Assert.Equal(trebaDaUspe, ok);
            if (trebaDaUspe)
                Assert.Equal(ime, repo.admini[0].Ime);
        }

        [Theory]
        [InlineData(1, "Pera", "Peric", "pera@mail.com", "12345", true)]
        [InlineData(1, "", "Peric", "pera@mail.com", "12345", false)]
        [InlineData(1, "Pera", "", "pera@mail.com", "12345", false)]
        [InlineData(1, "Pera", "Peric", "", "12345", false)]
        [InlineData(1, "Pera", "Peric", "pera@mail.com", "", false)]
        public async Task IzmeniKondukteraTest(int id, string ime, string prezime, string email, string brojLegitimacije, bool trebaDaUspe)
        {
            var kondukter = new Kondukter("Stari", "Ime", "stari@gmail.com", "lozinka123", "00000");
            kondukter.Id = 1;
            repo.kondukteri.Add(kondukter);

            var ok = await servis.IzmeniKonduktera(id, ime, prezime, email, brojLegitimacije);
            Assert.Equal(trebaDaUspe, ok);
            if (trebaDaUspe)
                Assert.Equal(ime, repo.kondukteri[0].Ime);
        }

    }
}
