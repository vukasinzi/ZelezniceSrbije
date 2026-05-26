using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Tests.Models
{
    public class TipVozaTest
    {
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
    }
}