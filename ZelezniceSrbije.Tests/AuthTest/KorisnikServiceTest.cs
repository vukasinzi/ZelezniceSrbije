using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Tests.AuthTest
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
        [InlineData("Marko", "Marković", "marko@gmail.com", "0611234567", "sifra123", true)]   // ispravno
        [InlineData("", "Marković", "marko2@gmail.com", "0611234567", "sifra123", false)]       // prazno ime
        [InlineData("Marko", "", "marko3@gmail.com", "0611234567", "sifra123", false)]          // prazno prezime
        [InlineData("Marko", "Marković", "", "0611234567", "sifra123", false)]                  // prazan email
        [InlineData("Marko", "Marković", "nijemail", "0611234567", "sifra123", false)]          // email bez @
        [InlineData("ImeKojeJePredugo123456", "Marković", "marko4@gmail.com", "0611234567", "sifra123", false)] // ime > 20
        [InlineData("Marko", "PrezimeKojeJePredugoO", "marko5@gmail.com", "0611234567", "sifra123", false)]      // prezime > 20
        [InlineData("ImeKojeJePrecizno20k", "Marković", "marko6@gmail.com", "0611234567", "sifra123", true)]  // ime = 20
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

            var putnik1 = new Putnik("Marko", "Marković", "marko@gmail.com", "0611234567", "sifra");
            var putnik2 = new Putnik("Ana", "Anić", "marko@gmail.com", "0621234567", "sifra");

            await servis.RegistrujAsync(putnik1);
            var rezultat = await servis.RegistrujAsync(putnik2);

            Assert.Null(rezultat);
        }
        [Fact]
        public async Task NepostojeciLogInTest()
        {
     
            var putnik1 = new Putnik("Marko", "Marković", "marko@gmail.com", "06030120", "marko123");
            var putnik2 = new Putnik("Ana", "Anić", "ana@gmail.com", "07463063", "ana123");
       
            await servis.RegistrujAsync(putnik1);
            await servis.RegistrujAsync(putnik2);

            var lazniPutnik = new Putnik("Ana", "Anić", "ana@nepostojeca.com", "07463063", "ana123");
            var rez = await servis.LogInAsync(lazniPutnik.Email, lazniPutnik.Lozinka);

            Assert.Null(rez);

        }
        [Fact]
        public async Task PostojeciLogInTest()
        {
            var putnik1 = new Putnik("Marko", "Marković", "marko@gmail.com", "06030120", "marko123");
            var putnik2 = new Putnik("Ana", "Anić", "ana@gmail.com", "07463063", "ana123");

            await servis.RegistrujAsync(putnik1);
            await servis.RegistrujAsync(putnik2);

            var praviPutnik = new Putnik("Ana", "Anić", "ana@gmail.com", "07463063", "ana123");
            var rez = await servis.LogInAsync(praviPutnik.Email, praviPutnik.Lozinka);

            Assert.NotNull(rez);

        }

    }
}
