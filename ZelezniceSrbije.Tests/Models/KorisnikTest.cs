using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Tests.Models
{
    public class KorisnikTest
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
    }
}