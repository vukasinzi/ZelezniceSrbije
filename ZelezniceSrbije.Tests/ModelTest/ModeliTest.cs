using System;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Tests.ModelTest
{
    public class ModeliTest
    {
        [Theory]
        [InlineData("Petar", "Petrovic", "petar@gmail.com", "lozinka123", true)]
        [InlineData("", "Petrovic", "petar@gmail.com", "lozinka123", false)]
        [InlineData("Petar", "", "petar@gmail.com", "lozinka123", false)]
        [InlineData("Petar", "Petrovic", "", "lozinka123", false)]
        [InlineData("Petar", "Petrovic", "petargmail.com", "lozinka123", false)]
        [InlineData("Petar", "Petrovic", "petar@gmail.com", "123", false)]
        [InlineData(null, "Petrovic", "petar@gmail.com", "lozinka123", false)]
        [InlineData("Petar", null, "petar@gmail.com", "lozinka123", false)]
        [InlineData("Petar", "Petrovic", null, "lozinka123", false)]
        [InlineData("Petar", "Petrovic", "petar@gmail.com", null, false)]
        public void Korisnik_Test(string? ime, string? prezime, string? email, string? lozinka, bool trebaDaUspe)
        {
            var korisnik = new Korisnik(ime, prezime, email, lozinka);

            var rezultat = korisnik.JeValidan();

            Assert.Equal(trebaDaUspe, rezultat);
        }

        [Theory]
        [InlineData("Petar", "Petrovic", "petar@gmail.com", "0611234567", "lozinka123", true)]
        [InlineData("Petar", "Petrovic", "petar@gmail.com", "", "lozinka123", false)]
        [InlineData("Petar", "Petrovic", "petar@gmail.com", "061abc", "lozinka123", false)]
        [InlineData("Petar", "Petrovic", "petar@gmail.com", null, "lozinka123", false)]
        [InlineData("", "Petrovic", "petar@gmail.com", "0611234567", "lozinka123", false)]
        [InlineData("Petar", "", "petar@gmail.com", "0611234567", "lozinka123", false)]
        [InlineData("Petar", "Petrovic", "petargmail.com", "0611234567", "lozinka123", false)]
        [InlineData("Petar", "Petrovic", "petar@gmail.com", "0611234567", "123", false)]
        public void Putnik_Test(string? ime, string? prezime, string? email, string? broj_telefona, string? lozinka, bool trebaDaUspe)
        {
            var putnik = new Putnik(ime, prezime, email, broj_telefona, lozinka);

            var rezultat = putnik.JeValidan();

            Assert.Equal(trebaDaUspe, rezultat);
        }

        [Theory]
        [InlineData("Marko", "Markovic", "marko@gmail.com", "lozinka123", "LEG-001", true)]
        [InlineData("Marko", "Markovic", "marko@gmail.com", "lozinka123", "", false)]
        [InlineData("Marko", "Markovic", "marko@gmail.com", "lozinka123", null, false)]
        [InlineData("", "Markovic", "marko@gmail.com", "lozinka123", "LEG-001", false)]
        [InlineData("Marko", "", "marko@gmail.com", "lozinka123", "LEG-001", false)]
        [InlineData("Marko", "Markovic", "markogmail.com", "lozinka123", "LEG-001", false)]
        [InlineData("Marko", "Markovic", "marko@gmail.com", "123", "LEG-001", false)]
        public void Kondukter_Test(string? ime, string? prezime, string? email, string? lozinka, string? broj_legitimacije, bool trebaDaUspe)
        {
            var kondukter = new Kondukter(ime, prezime, email, lozinka, broj_legitimacije);

            var rezultat = kondukter.JeValidan();

            Assert.Equal(trebaDaUspe, rezultat);
        }

        [Theory]
        [InlineData("Milos", "Milosevic", "milos@gmail.com", "lozinka123", -1, true)]
        [InlineData("Milos", "Milosevic", "milos@gmail.com", "lozinka123", 0, true)]
        [InlineData("Milos", "Milosevic", "milos@gmail.com", "lozinka123", 1, false)]
        [InlineData("", "Milosevic", "milos@gmail.com", "lozinka123", -1, false)]
        [InlineData("Milos", "", "milos@gmail.com", "lozinka123", -1, false)]
        [InlineData("Milos", "Milosevic", "milosgmail.com", "lozinka123", -1, false)]
        [InlineData("Milos", "Milosevic", "milos@gmail.com", "123", -1, false)]
        public void Administrator_Test(string? ime, string? prezime, string? email, string? lozinka, int dani, bool trebaDaUspe)
        {
            var datum = DateTime.Today.AddDays(dani);
            var administrator = new Administrator(ime, prezime, email, lozinka, datum);

            var rezultat = administrator.JeValidan();

            Assert.Equal(trebaDaUspe, rezultat);
        }

        [Fact]
        public void Administrator_NullDatum_Test()
        {
            var administrator = new Administrator("Milos", "Milosevic", "milos@gmail.com", "lozinka123", null);

            var rezultat = administrator.JeValidan();

            Assert.False(rezultat);
        }

        [Theory]
        [InlineData("Beograd Centar", "Savski Venac", true)]
        [InlineData("", "Savski Venac", false)]
        [InlineData("Beograd Centar", "", false)]
        [InlineData(null, "Savski Venac", false)]
        [InlineData("Beograd Centar", null, false)]
        public void Stanica_Test(string? naziv, string? region, bool trebaDaUspe)
        {
            var stanica = new Stanica(naziv, region);

            var rezultat = stanica.JeValidan();

            Assert.Equal(trebaDaUspe, rezultat);
        }

        [Theory]
        [InlineData("SOKO", "Brzi voz", true)]
        [InlineData("", "Brzi voz", false)]
        [InlineData("SOKO", "", false)]
        [InlineData(null, "Brzi voz", false)]
        [InlineData("SOKO", null, false)]
        public void TipVoza_Test(string? naziv, string? opis, bool trebaDaUspe)
        {
            var tip = new TipVoza(naziv, opis);

            var rezultat = tip.JeValidan();

            Assert.Equal(trebaDaUspe, rezultat);
        }

        [Theory]
        [InlineData("IC", "SRB-IC-001", true, 1, true)]
        [InlineData("", "SRB-IC-001", true, 1, false)]
        [InlineData("IC", "", true, 1, false)]
        [InlineData("IC", "SRB-IC-001", true, 0, false)]
        [InlineData("IC", "SRB-IC-001", true, -1, false)]
        [InlineData(null, "SRB-IC-001", true, 1, false)]
        [InlineData("IC", null, true, 1, false)]
        public void Voz_Test(string? naziv, string? serijski_broj, bool aktivan, int tip_voza_id, bool trebaDaUspe)
        {
            var voz = new Voz(naziv, serijski_broj, aktivan, tip_voza_id);

            var rezultat = voz.JeValidan();

            Assert.Equal(trebaDaUspe, rezultat);
        }

        [Theory]
        [InlineData(1, 1, false, true)]
        [InlineData(0, 1, false, false)]
        [InlineData(1, 0, false, false)]
        [InlineData(-1, 1, false, false)]
        [InlineData(1, -1, false, false)]
        [InlineData(1, 1, true, false)]
        public void Raspored_Test(int linija_id, int voz_id, bool praznoVreme, bool trebaDaUspe)
        {
            var vreme = DateTime.Today.AddDays(1).AddHours(10);

            if (praznoVreme)
                vreme = default;

            var raspored = new Raspored(vreme, linija_id, voz_id);

            var rezultat = raspored.JeValidan();

            Assert.Equal(trebaDaUspe, rezultat);
        }

        [Fact]
        public void Karta_Test()
        {
            var token = Guid.NewGuid();
            var polazak = DateTime.Today.AddHours(10);
            var dolazak = DateTime.Today.AddHours(11).AddMinutes(30);

            var karta = new Karta(500, 101, 1, "Beograd Centar", "Novi Sad", "Beograd centar - Novi Sad", "SOKO", polazak, dolazak, 90, null, token);

            Assert.Equal(500, karta.Cena);
            Assert.Equal(101, karta.Putnik_id);
            Assert.Equal(1, karta.Raspored_id);
            Assert.Equal("Beograd Centar", karta.Polaziste);
            Assert.Equal("Novi Sad", karta.Odrediste);
            Assert.Equal("Beograd centar - Novi Sad", karta.Linija);
            Assert.Equal("SOKO", karta.Tip_voza);
            Assert.Equal(polazak, karta.Vreme_polaska);
            Assert.Equal(dolazak, karta.Vreme_dolaska);
            Assert.Equal(90, karta.Trajanje_min);
            Assert.Null(karta.Kondukter);
            Assert.Equal(token, karta.Qr_token);
            Assert.False(karta.Ocitana);
        }
    }
}