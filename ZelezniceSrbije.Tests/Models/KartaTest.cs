using System;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Tests.Models
{
    public class KartaTest
    {
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