using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Tests.Models
{
    public class StanicaTest
    {
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
    }
}