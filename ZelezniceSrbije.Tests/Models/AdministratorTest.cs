using System;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Tests.Models
{
    public class AdministratorTest
    {
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
    }
}