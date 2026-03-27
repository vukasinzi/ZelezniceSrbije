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
        [InlineData("ImeKojeJePredugo123456", "Marković", "marko4@gmail.com", "0611234567", "sifra", false)] // ime > 20
        [InlineData("Marko", "PrezimeKojeJePredugoO", "marko5@gmail.com", "0611234567", "sifra", false)]      // prezime > 20
        [InlineData("ImeKojeJePrecizno20k", "Marković", "marko6@gmail.com", "0611234567", "sifra", true)]  // ime = 20
        [InlineData("Ime","Prezime","marecar@gmail.com","421dsada4","sifra",false)]
        public async Task RegistracijaTestovi(string ime, string prezime, string email, string broj_telefona, string lozinka, bool trebaDaUspe)
        {
            Putnik p = new(ime, prezime, email, broj_telefona, lozinka);
            var rezultat = await servis.RegistrujAsync(p);
            if (trebaDaUspe == true)
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
    }
}
