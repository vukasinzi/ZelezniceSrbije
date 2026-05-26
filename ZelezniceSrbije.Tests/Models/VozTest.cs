using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Tests.Models
{
    public class VozTest
    {
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
    }
}