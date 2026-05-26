using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Tests.Models
{
    public class KondukterTest
    {
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
    }
}