using System;
using System.Linq;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Tests.QrTest
{
    public class QrServiceTest
    {
        private readonly QrService servis;

        public QrServiceTest()
        {
            servis = new QrService();
        }

        [Theory]
        [InlineData("KARTA-1")]
        [InlineData("https://srbvoz.rs/karta/123")]
        [InlineData("Beograd Centar-Novi Sad-2026")]
        public void GenerisiQrKod_Test(string payload)
        {
            var rezultat = servis.GenerisiQrKod(payload);

            Assert.NotNull(rezultat);
            Assert.NotEmpty(rezultat);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("     ")]
        public void GenerisiQrKod_PrazanPayload_Test(string? payload)
        {
            var greska = Assert.Throws<ArgumentException>(() => servis.GenerisiQrKod(payload));

            Assert.Equal("QR payload je prazan.", greska.Message);
        }

        [Fact]
        public void GenerisiQrKod_Png_Test()
        {
            var rezultat = servis.GenerisiQrKod("KARTA-1");
        //png signature - svaki png pocinje sa ovih 8 bajtova. ovo je nacin da proverimo je l ovo zapravo png
            byte[] pngPotpis = { 137, 80, 78, 71, 13, 10, 26, 10 };

            Assert.True(rezultat.Take(8).SequenceEqual(pngPotpis));
        }

        [Fact]
        public void GenerisiQrKod_IstiPayload_Test()
        {
            var prvi = servis.GenerisiQrKod("KARTA-1");
            var drugi = servis.GenerisiQrKod("KARTA-1");

            Assert.Equal(prvi, drugi);
        }
    }
}