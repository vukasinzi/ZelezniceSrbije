using System;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Tests.Models
{
    public class RasporedTest
    {
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
    }
}