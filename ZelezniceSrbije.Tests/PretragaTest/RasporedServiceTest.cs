using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Repositories;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Tests.PretragaTest
{
    public class RasporedServiceTest : IDisposable
    {
        private readonly VozAppContext context;
        private readonly SqliteConnection connection;
        private readonly RasporedService servis;

        public RasporedServiceTest()
        {
            var db = TestBazaUMemoriji.KreirajContext();
            context = db.context;
            connection = db.connection;

            var repo = new RasporedRepository(context);
            servis = new RasporedService(repo);
        }

        private Task PopuniBazu()
        {
            return TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
        }

        public void Dispose()
        {
            context.Dispose();
            connection.Dispose();
        }

        [Fact]
        public async Task UcitajStanica_Test()
        {
            await PopuniBazu();

            var rezultat = await servis.UcitajStaniceAsync();

            Assert.NotNull(rezultat);
            Assert.Equal(8, rezultat.Count);
            Assert.Contains(rezultat, x => x.Naziv == "Beograd Centar");
            Assert.Contains(rezultat, x => x.Naziv == "Novi Sad");
            Assert.Contains(rezultat, x => x.Naziv == "Subotica");
        }

        [Theory]
        [InlineData("", "Novi Sad", false)]
        [InlineData(null, "Novi Sad", false)]
        [InlineData("Novi Sad", null, false)]
        [InlineData("Beograd Centar", "Beograd Centar", false)]
        [InlineData("Beograd Centar", "Novi Sad", true)]
        public async Task PretragaParametri_Test(string polaziste, string odrediste, bool trebaDaUspe)
        {
            if (trebaDaUspe)
                await PopuniBazu();

            var sutra = DateTime.Today.AddDays(1);
            var rezultat = await servis.PretraziAsync(polaziste, odrediste, sutra);

            if (trebaDaUspe)
            {
                Assert.NotNull(rezultat);
                Assert.NotEmpty(rezultat);
            }
            else
            {
                Assert.Null(rezultat);
            }
        }

        [Fact]
        public async Task PretragaDatumi_Test()
        {
            var rezultat = await servis.PretraziAsync("Beograd Centar", "Novi Sad", DateTime.Today.AddDays(-1));
            Assert.Null(rezultat);
        }

        [Fact]
        public async Task Pretraga_NepostojeceStanice_Test()
        {
            await PopuniBazu();

            var rezultat = await servis.PretraziAsync("Nepostojece Polaziste", "Novi Sad", DateTime.Today);

            Assert.Null(rezultat);
        }

        [Fact]
        public async Task Pretraga_DatumNepostojeci_Test()
        {
            await PopuniBazu();

            var rezultat = await servis.PretraziAsync("Beograd Centar", "Novi Sad", DateTime.Today.AddDays(1000));

            Assert.NotNull(rezultat);
            Assert.Empty(rezultat);
        }

        [Fact]
        public async Task Pretraga_PogresanRedosled_Test()
        {
            await PopuniBazu();

            var rezultat = await servis.PretraziAsync("Novi Sad", "Beograd Centar", DateTime.Today);

            Assert.NotNull(rezultat);
            Assert.Empty(rezultat);
        }

        [Fact]
        public async Task Pretraga_PogresnaLinija_Test()
        {
            await PopuniBazu();

            var rezultat = await servis.PretraziAsync("Beograd Centar", "Subotica", DateTime.Today);

            Assert.NotNull(rezultat);
            Assert.Empty(rezultat);
        }

        [Theory]
        [InlineData("Beograd Centar", "Novi Sad", 2)]
        [InlineData("Novi Sad", "Subotica", 1)]
        [InlineData("Beograd Centar", "Subotica", 0)]
        public async Task PretraziAsync_BrojPolazakaTest(string pol, string odr, int ocekivanBroj)
        {
            await PopuniBazu();

            var rezultat = await servis.PretraziAsync(pol, odr, DateTime.Today);

            Assert.NotNull(rezultat);
            Assert.Equal(ocekivanBroj, rezultat.Count);
        }

        [Fact]
        public async Task UcitajRasporede_NepostojeciDatumi_Test()
        {
            await PopuniBazu();

            Assert.Empty(await servis.UcitajRasporede(null));
            Assert.Empty(await servis.UcitajRasporede(DateTime.Today.AddDays(1000)));
        }
        [Fact]
        public async Task UcitajRasporede_Danas_Test()
        {
            await PopuniBazu();

            var rezultat = await servis.UcitajRasporede(DateTime.Today);

            Assert.NotNull(rezultat);
            Assert.Equal(11, rezultat.Count);
        }
        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        [InlineData(9999, false)]
        public async Task UkloniRaspored_Test(int id, bool trebaDaUspe)
        {
            await PopuniBazu();

            var rezultat = await servis.UkloniRaspored(id);
            var raspored = await context.Raspored.FindAsync(id);

            Assert.Equal(trebaDaUspe, rezultat);
            if (trebaDaUspe)
                Assert.Null(raspored);
        }
        [Theory]
        [InlineData(1, 1, false, true)]
        [InlineData(0, 1, false, false)]
        [InlineData(1, 0, false, false)]
        [InlineData(-1, 1, false, false)]
        [InlineData(1, -1, false, false)]
        [InlineData(1, 1, true, false)]
        public async Task DodajRaspored_Test(int linija_id, int voz_id, bool praznoVreme, bool trebaDaUspe)
        {
            await PopuniBazu();

            var vremePolaska = DateTime.Today.AddDays(2).AddHours(10);
            if (praznoVreme)
                vremePolaska = default;

            var rezultat = await servis.DodajRaspored(linija_id, voz_id, vremePolaska);
            var dodatiRaspored = await context.Raspored.FirstOrDefaultAsync(r => r.Linija_id == linija_id && r.Voz_id == voz_id && r.Vreme_polaska == vremePolaska);

            Assert.Equal(trebaDaUspe, rezultat);
            if (trebaDaUspe)
                Assert.NotNull(dodatiRaspored);
        }
        [Fact]
        public async Task IzmeniRaspored_Test()
        {
            await PopuniBazu();

            var novoVreme = DateTime.Today.AddDays(2).AddHours(18);
            var rezultat = await servis.IzmeniRaspored(1, 2, 2, novoVreme);
            var raspored = await context.Raspored.FindAsync(1);

            Assert.True(rezultat);
            Assert.NotNull(raspored);
            Assert.Equal(2, raspored.Linija_id);
            Assert.Equal(2, raspored.Voz_id);
            Assert.Equal(novoVreme, raspored.Vreme_polaska);
        }
        [Theory]
        [InlineData(1, 0, 2, false)]
        [InlineData(1, 2, 0, false)]
        [InlineData(1, -1, 2, false)]
        [InlineData(1, 2, -1, false)]
        [InlineData(9999, 2, 2, false)]
        public async Task IzmeniRaspored_NepostojeceVrednosti_Test(int id, int linija_id, int voz_id, bool trebaDaUspe)
        {
            await PopuniBazu();

            var stari = await context.Raspored.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            var rezultat = await servis.IzmeniRaspored(id, linija_id, voz_id, DateTime.Today.AddDays(3).AddHours(9));

            Assert.Equal(trebaDaUspe, rezultat);
            if (stari != null)
            {
                var novi = await context.Raspored.AsNoTracking().FirstAsync(r => r.Id == id);
                Assert.Equal(stari.Linija_id, novi.Linija_id);
                Assert.Equal(stari.Voz_id, novi.Voz_id);
                Assert.Equal(stari.Vreme_polaska, novi.Vreme_polaska);
            }
        }

        [Fact]
        public async Task IzmeniRaspored_NevalidnoVreme_Test()
        {
            await PopuniBazu();
            Assert.False(await servis.IzmeniRaspored(1, 2, 2, default));
        }
    }
}
