using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Models.ViewModels;
using ZelezniceSrbije.Repositories;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Tests.VozTest
{
    public class VozServiceTest : IDisposable
    {
        private readonly VozAppContext context;
        private readonly SqliteConnection connection;
        private readonly VozService servis;

        public VozServiceTest()
        {
            var db = TestBazaUMemoriji.KreirajContext();
            context = db.context;
            connection = db.connection;

            var repo = new VozRepository(context);
            servis = new VozService(repo);
        }

        public void Dispose()
        {
            context.Dispose();
            connection.Dispose();
        }

        [Fact]
        public async Task UcitajSveTipoveVoza_Test()
        {
            context.TipVoza.AddRange(
                new TipVoza(1, "Fejk1", "bukv laznjak"),
                new TipVoza(2, "Fejk2", "bukv laznjak"),
                new TipVoza(3, "Fejk3", "bukv laznjak")
            );
            await context.SaveChangesAsync();

            var lista = await servis.UcitajSveTipoveVoza();

            Assert.NotNull(lista);
            Assert.Equal(3, lista.Count);
            Assert.Contains(lista, x => x.Naziv == "Fejk1");
        }

        [Fact]
        public async Task UcitajSveVozove_Test()
        {
            context.TipVoza.AddRange(
                new TipVoza(1, "Tip1", "Opis"),
                new TipVoza(2, "Tip2", "Opis"),
                new TipVoza(3, "Tip3", "Opis")
            );
            context.Voz.AddRange(
                new Voz(1, "Laznjak1", "LA-33-ZNJAK1", false, 1),
                new Voz(2, "Laznjak2", "LA-33-ZNJAK2", true, 2),
                new Voz(3, "Laznjak3", "LA-33-ZNJAK3", false, 3)
            );
            await context.SaveChangesAsync();

            var lista = await servis.UcitajSveVozove();

            Assert.NotNull(lista);
            Assert.Equal(3, lista.Count);
            Assert.Contains(lista, x => x.Naziv == "Laznjak1");
        }

        [Theory]
        [InlineData("", "Opis", false)]
        [InlineData("Naziv", "", false)]
        [InlineData("", "", false)]
        [InlineData(null, "Opis", false)]
        [InlineData("Naziv", null, false)]
        [InlineData("Naziv", "Opis", true)]
        public async Task DodajTipVoza_Test(string? naziv, string? opis, bool trebaDaUspe)
        {
            var rezultat = await servis.DodajTipVoza(naziv!, opis!);

            Assert.Equal(trebaDaUspe, rezultat);

            var tipovi = await servis.UcitajSveTipoveVoza();
            if (trebaDaUspe)
                Assert.Single(tipovi);
            else
                Assert.Empty(tipovi);
        }


        [Theory]
        [InlineData("", "SRB-001", 1, true, false)]
        [InlineData("Voz 1", "", 1, true, false)]
        [InlineData("Voz 1", "SRB-001", 0, true, false)]
        [InlineData("Voz 1", "SRB-001", -1, true, false)]
        [InlineData(null, "SRB-001",1, true, false)]
        [InlineData("Voz 1",null,1, true, false)]
        [InlineData("Voz 1","SRB-001",1, true, true)]
        [InlineData("Voz 1","SRB-001",2, true, false)]
        public async Task DodajVoz_Test(string naziv, string serijski_broj, int tip_voza_id, bool aktivan, bool trebaDaUspe)
        {
            context.TipVoza.Add(new TipVoza(1, "Soko", "Brzi voz....."));
            var rezultat = await servis.DodajVoz(naziv, serijski_broj, tip_voza_id, aktivan);
            Assert.Equal(trebaDaUspe, rezultat);

            var vozovi = await servis.UcitajSveVozove();
            if (trebaDaUspe)
                Assert.Single(vozovi);
            else
                Assert.Empty(vozovi);
        }

      

        [Theory]
        [InlineData(1, "", "Brzi voz", false)]
        [InlineData(1, "Intercity", "", false)]
        [InlineData(1, null, "Brzi voz", false)]
        [InlineData(1, "Novi naziv", null, false)]
        [InlineData(1,"Novi naziv","Novi opis",true)]
        public async Task IzmeniTipVoza_Test(int id, string? naziv, string? opis, bool trebaDaUspe)
        {
            context.TipVoza.Add(new TipVoza(id, "Stari naziv", "Stari opis"));
            await context.SaveChangesAsync();

            var rezultat = await servis.IzmeniTipVoza(id, naziv, opis);
            Assert.Equal(trebaDaUspe, rezultat);
            if (trebaDaUspe)
            {
                List<TipVoza> tip = await servis.UcitajSveTipoveVoza();
                Assert.Equal(tip[0].Naziv, naziv);
            }
               
        }
      

        [Theory]
        [InlineData(1, "", "SRB-001", true, 1, false)]
        [InlineData(1, "Voz 1", "", true, 1, false)]
        [InlineData(1, "Voz 1", "SRB-001", true, 0, false)]
        [InlineData(1, "Voz 1", "SRB-001", true, -1, false)]
        [InlineData(1, null, "SRB-001", true, 1, false)]
        [InlineData(1, "Voz 1", null, true, 1, false)]
        [InlineData(1, "Voz 1", "SRB-001", true, 1, true)]
        public async Task IzmeniVoz_Test(int id, string naziv, string serijski_broj, bool aktivan, int tip_voza_id, bool trebaDaUspe)
        {
            context.TipVoza.Add(new TipVoza(1, "Tip1", "Opis"));
            context.Voz.Add(new Voz(id, "Stari naziv", "SRB-000", false, 1));
            await context.SaveChangesAsync();

            var rezultat = await servis.IzmeniVoz(id, naziv, serijski_broj, aktivan, tip_voza_id);
            Assert.Equal(trebaDaUspe, rezultat);
            if (trebaDaUspe)
            {
                List<Voz> tip = await servis.UcitajSveVozove();
                Assert.Equal(tip[0].Naziv, naziv);
            }
        }


        [Theory]
        [InlineData(1,true)]
        [InlineData(-1,false)]
        [InlineData(0,false)]
        public async Task UkloniTipVoza_Test(int id,bool trebaDaUspe)
        {
            context.TipVoza.Add(new TipVoza(1, "Fejk", "Opis"));
            await context.SaveChangesAsync();

            var rezultat = await servis.UkloniTipVoza(id);
            var tipovi = await servis.UcitajSveTipoveVoza();

            if (trebaDaUspe)
            {
                Assert.True(rezultat);
                Assert.Empty(tipovi);
            }
            else
            {
                Assert.False(rezultat);
                Assert.Single(tipovi);
            }
        }



        [Theory]
        [InlineData(1,true)]
        [InlineData(2,false)]
        [InlineData(-1,false)]
        public async Task UkloniVoz_Test(int id,bool trebaDaUspe)
        {
            context.TipVoza.Add(new TipVoza(1, "Fejk tip", "Opis"));
            context.Voz.Add(new Voz(1, "Fejk", "SRB-001", true, 1));
            await context.SaveChangesAsync();

            var rezultat = await servis.UkloniVoz(id);
            var vozovi = await servis.UcitajSveVozove();
            if (trebaDaUspe)
            {
                Assert.True(rezultat);
                Assert.Empty(vozovi);
            }
            else
            {
                Assert.False(rezultat);
                Assert.Single(vozovi);
            }    

        }

    }
}
