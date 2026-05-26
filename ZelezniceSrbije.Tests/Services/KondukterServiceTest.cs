using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Tests.KondukterTest
{
    public class KondukterServiceTest : IDisposable
    {
        private readonly VozAppContext context;
        private readonly SqliteConnection connection;
        private readonly KondukterService servis;

        public KondukterServiceTest()
        {
            var db = TestBazaUMemoriji.KreirajContext();
            context = db.context;
            connection = db.connection;

            var repo = new KondukterRepository(context);
            servis = new KondukterService(repo);
        }

        private Task PopuniBazu()
        {
            return TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
        }

        private async Task<Karta> DodajKartu(bool ocitana = false)
        {
            var karta = new Karta(500,101,1,"Beograd Centar","Novi Sad","Beograd centar - Novi Sad","SOKO",DateTime.Today.AddHours(12),DateTime.Today.AddHours(13).AddMinutes(30),90,null,Guid.NewGuid());

            karta.Ocitana = ocitana;

            if (ocitana)
                karta.Datum_ocitavanja = DateTime.Now;

            context.Karta.Add(karta);
            await context.SaveChangesAsync();

            return karta;
        }

        public void Dispose()
        {
            context.Dispose();
            connection.Dispose();
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        [InlineData(9999, false)]
        public async Task VratiRaspored_Test(int raspored_id, bool trebaDaUspe)
        {
            await PopuniBazu();

            var rezultat = await servis.VratiRaspored(raspored_id);

            if (trebaDaUspe)
            {
                Assert.NotNull(rezultat);
                Assert.Equal(raspored_id, rezultat.Id);
                Assert.False(string.IsNullOrWhiteSpace(rezultat.Linija));
                Assert.False(string.IsNullOrWhiteSpace(rezultat.TipVoza));
            }
            else
            {
                Assert.Null(rezultat);
            }
        }

        [Fact]
        public async Task VratiRasporedeZaDanas_Test()
        {
            await PopuniBazu();

            var prvi = await context.Raspored.FirstAsync(x => x.Id == 1);
            var drugi = await context.Raspored.FirstAsync(x => x.Id == 2);

            prvi.Vreme_polaska = DateTime.Now.AddHours(1);
            drugi.Vreme_polaska = DateTime.Now.AddHours(2);

            await context.SaveChangesAsync();

            var now = DateTime.Now;
            var rezultat = await servis.VratiRasporedeZaDanas();

            Assert.NotNull(rezultat);
            Assert.NotEmpty(rezultat);
            Assert.All(rezultat, x => Assert.Equal(DateTime.Today, x.PolazakSaPol.Date));
            Assert.All(rezultat, x => Assert.True(x.PolazakSaPol >= now));
        }

        [Fact]
        public async Task VratiRasporedeZaDanas_Sortirano_Test()
        {
            await PopuniBazu();

            var prvi = await context.Raspored.FirstAsync(x => x.Id == 1);
            var drugi = await context.Raspored.FirstAsync(x => x.Id == 2);
            var treci = await context.Raspored.FirstAsync(x => x.Id == 4);

            prvi.Vreme_polaska = DateTime.Now.AddHours(3);
            drugi.Vreme_polaska = DateTime.Now.AddHours(1);
            treci.Vreme_polaska = DateTime.Now.AddHours(2);

            await context.SaveChangesAsync();

            var rezultat = await servis.VratiRasporedeZaDanas();

            Assert.NotNull(rezultat);

            var vremena = rezultat.Select(x => x.PolazakSaPol).ToList();
            var sortirano = vremena.OrderBy(x => x).ToList();

            Assert.Equal(sortirano, vremena);
        }

        [Fact]
        public async Task VratiRasporedeZaDanas_NemaPolazaka_Test()
        {
            await PopuniBazu();

            var rasporedi = await context.Raspored.ToListAsync();

            foreach (var r in rasporedi)
                r.Vreme_polaska = DateTime.Today.AddDays(5).AddHours(10);

            await context.SaveChangesAsync();

            var rezultat = await servis.VratiRasporedeZaDanas();

            Assert.Null(rezultat);
        }

        [Fact]
        public async Task OcitajKartu_Test()
        {
            await PopuniBazu();

            var karta = await DodajKartu();

            var rezultat = await servis.OcitajKartu(karta.Qr_token, karta.Raspored_id);
            var osvezena = await context.Karta.AsNoTracking().FirstAsync(x => x.Id == karta.Id);

            Assert.True(rezultat);
            Assert.True(osvezena.Ocitana);
            Assert.NotNull(osvezena.Datum_ocitavanja);
        }

        [Fact]
        public async Task OcitajKartu_NepostojeciToken_Test()
        {
            await PopuniBazu();

            await DodajKartu();

            var brojOcitanihPre = await context.Karta.CountAsync(x => x.Ocitana);

            var rezultat = await servis.OcitajKartu(Guid.NewGuid(), 1);

            var brojOcitanihPosle = await context.Karta.CountAsync(x => x.Ocitana);

            Assert.False(rezultat);
            Assert.Equal(brojOcitanihPre, brojOcitanihPosle);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(9999)]
        public async Task OcitajKartu_NepostojeciRaspored_Test(int raspored_id)
        {
            await PopuniBazu();

            var karta = await DodajKartu();
            var brojOcitanihPre = await context.Karta.CountAsync(x => x.Ocitana);

            var rezultat = await servis.OcitajKartu(karta.Qr_token, raspored_id);

            var brojOcitanihPosle = await context.Karta.CountAsync(x => x.Ocitana);

            Assert.False(rezultat);
            Assert.Equal(brojOcitanihPre, brojOcitanihPosle);
        }

        [Fact]
        public async Task OcitajKartu_VecOcitana_Test()
        {
            await PopuniBazu();

            var karta = await DodajKartu();

            var prviPut = await servis.OcitajKartu(karta.Qr_token, karta.Raspored_id);
            var drugiPut = await servis.OcitajKartu(karta.Qr_token, karta.Raspored_id);

            var osvezena = await context.Karta.AsNoTracking().FirstAsync(x => x.Id == karta.Id);

            Assert.True(prviPut);
            Assert.False(drugiPut);
            Assert.True(osvezena.Ocitana);
        }

        [Fact]
        public async Task OcitajKartu_PrazanToken_Test()
        {
            await PopuniBazu();
            await DodajKartu();
            var brojOcitanihPre = await context.Karta.CountAsync(x => x.Ocitana);
            var rezultat = await servis.OcitajKartu(Guid.Empty, 1);
            var brojOcitanihPosle = await context.Karta.CountAsync(x => x.Ocitana);

            Assert.False(rezultat);
            Assert.Equal(brojOcitanihPre, brojOcitanihPosle);
        }

        [Fact]
        public async Task OcitajKartu_VecOcitanaOdStarta_Test()
        {
            await PopuniBazu();

            var karta = await DodajKartu(true);

            var rezultat = await servis.OcitajKartu(karta.Qr_token, karta.Raspored_id);
            var osvezena = await context.Karta.AsNoTracking().FirstAsync(x => x.Id == karta.Id);

            Assert.False(rezultat);
            Assert.True(osvezena.Ocitana);
            Assert.NotNull(osvezena.Datum_ocitavanja);
        }
    }
}