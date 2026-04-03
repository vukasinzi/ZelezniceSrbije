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
        [InlineData("Ime", "Prezime", "marecar@gmail.com", "421dsada4", "sifra", false)] // ovde ostaje sifra, pada zbog nje
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
    }
}
