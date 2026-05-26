using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Tests.Models
{
    public class PutnikTest
    {
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
    }
}