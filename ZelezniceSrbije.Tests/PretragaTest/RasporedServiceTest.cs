using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
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
    }
}
