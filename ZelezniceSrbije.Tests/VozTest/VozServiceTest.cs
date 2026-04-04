using System;
using System.Collections.Generic;
using System.Text;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Tests.VozTest
{
    public class VozServiceTest
    {
        private readonly FakeVozRepository repo;
        private readonly VozService servis;
        public VozServiceTest()
        {
            repo = new FakeVozRepository();
            servis = new(repo);
        }
        [Fact]
        public async Task UcitajSveTipoveVoza_Test()
        {
            var tip_voza1 = new TipVoza("Fejk1", "bukv laznjak");
            var tip_voza2 = new TipVoza("Fejk2", "bukv laznjak");
            var tip_voza3 = new TipVoza("Fejk3", "bukv laznjak");
            repo.tipovi_voza.AddRange(tip_voza1, tip_voza2, tip_voza3);
            var lista = await servis.UcitajSveTipoveVoza();
            Assert.Equal(3, lista.Count);
            Assert.NotNull(lista);
            Assert.Equal(tip_voza1.Naziv, lista[0].Naziv);

        }
        [Fact]
        public async Task UcitajSveVozove_Test()
        {

            var voz1 = new Voz("Laznjak1", "LA-33-ZNJAK1", false, 1);
            var voz2 = new Voz("Laznjak2", "LA-33-ZNJAK2", true, 2);
            var voz3 = new Voz("Laznjak3", "LA-33-ZNJAK3", false, 3);
           
            repo.vozovi.AddRange(voz1, voz2, voz3);
            var lista = await servis.UcitajSveVozove();
            Assert.Equal(3, lista.Count);
            Assert.NotNull(lista);
            Assert.Equal(voz1.Naziv, lista[0].Naziv);
        }
        [Theory]
        [InlineData("", "Opis",false)]
        [InlineData("Naziv", "",false)]
        [InlineData("", "",false)]
        [InlineData(null, "Opis",false)]
        [InlineData("Naziv", null,false)]
        [InlineData("Naziv","Opis",true)]
        public async Task DodajTipVoza_Test(string naziv, string opis,bool trebaDaUspe)
        {
            var rezultat = await servis.DodajTipVoza(naziv, opis);
            Assert.Equal(trebaDaUspe, rezultat);
        }

        [Theory]
        [InlineData("", "SRB-001", 1, true,false)]
        [InlineData("Voz 1", "", 1, true,false)]
        [InlineData("Voz 1", "SRB-001", 0, true,false)]
        [InlineData("Voz 1", "SRB-001", -1, true,false)]
        public async Task DodajVoz_Test(string naziv, string serijski_broj, int tip_voza_id, bool aktivan,bool trebaDaUspe)
        {
            var rezultat = await servis.DodajVoz(naziv, serijski_broj, tip_voza_id, aktivan);
            Assert.Equal(trebaDaUspe, rezultat);
        }
        [Fact]
        public async Task DodajVoz_ProveraListe_Test()
        {
            await servis.DodajVoz("Voz 1", "SRB-001", 1, true);
            var vozovi = await servis.UcitajSveVozove();
            Assert.Single(vozovi);
        }

        [Theory]
        [InlineData(1, "Intercity", "Brzi voz", true)]
        [InlineData(1, "", "Brzi voz", false)]
        [InlineData(1, "Intercity", "", false)]
        [InlineData(1, null, "Brzi voz", false)]
        [InlineData(1, "Intercity", null, false)]
        public async Task IzmeniTipVoza_Test(int id, string naziv, string opis, bool trebaDaUspe)
        {
            var tip_voza = new TipVoza("Stari naziv", "Stari opis");
            tip_voza.Id = id;
            repo.tipovi_voza.Add(tip_voza);

            var rezultat = await servis.IzmeniTipVoza(id, naziv, opis);
            Assert.Equal(trebaDaUspe, rezultat);
        }

        [Fact]
        public async Task IzmeniTipVoza_ProveraIzmene_Test()
        {
            var tip_voza = new TipVoza("Stari naziv", "Stari opis");
            tip_voza.Id = 1;
            repo.tipovi_voza.Add(tip_voza);

            await servis.IzmeniTipVoza(1, "Novi naziv", "Novi opis");

            Assert.Equal("Novi naziv", repo.tipovi_voza[0].Naziv);
            Assert.Equal("Novi opis", repo.tipovi_voza[0].Opis);
        }

        [Theory]
        [InlineData(1, "Voz 1", "SRB-001", true, 1, true)]
        [InlineData(1, "", "SRB-001", true, 1, false)]
        [InlineData(1, "Voz 1", "", true, 1, false)]
        [InlineData(1, "Voz 1", "SRB-001", true, 0, false)]
        [InlineData(1, "Voz 1", "SRB-001", true, -1, false)]
        public async Task IzmeniVoz_Test(int id, string naziv, string serijski_broj, bool aktivan, int tip_voza_id, bool trebaDaUspe)
        {
            var voz = new Voz("Stari naziv", "SRB-000", false, 1);
            voz.Id = id;
            repo.vozovi.Add(voz);

            var rezultat = await servis.IzmeniVoz(id, naziv, serijski_broj, aktivan, tip_voza_id);
            Assert.Equal(trebaDaUspe, rezultat);
        }

        [Fact]
        public async Task IzmeniVoz_ProveraIzmene_Test()
        {
            var voz = new Voz("Stari naziv", "SRB-000", false, 1);
            voz.Id = 1;
            repo.vozovi.Add(voz);

            await servis.IzmeniVoz(1, "Novi naziv", "SRB-001", true, 2);

            Assert.Equal("Novi naziv", repo.vozovi[0].Naziv);
            Assert.Equal("SRB-001", repo.vozovi[0].Serijski_broj);
            Assert.True(repo.vozovi[0].Aktivan);
            Assert.Equal(2, repo.vozovi[0].Tip_voza_id);
        }

        [Fact]
        public async Task UkloniTipVoza_Test()
        {
            var tip_voza = new TipVoza("Fejk", "Opis");
            tip_voza.Id = 1;
            repo.tipovi_voza.Add(tip_voza);

            var rezultat = await servis.UkloniTipVoza(1);
            Assert.True(rezultat);
            Assert.Empty(repo.tipovi_voza);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task UkloniTipVoza_NevazeciId_Test(int id)
        {
            var rezultat = await servis.UkloniTipVoza(id);
            Assert.False(rezultat);
        }

        [Fact]
        public async Task UkloniVoz_VazeciId_Test()
        {
            var voz = new Voz("Fejk", "SRB-001", true, 1);
            voz.Id = 1;
            repo.vozovi.Add(voz);

            var rezultat = await servis.UkloniVoz(1);
            Assert.True(rezultat);
            Assert.Empty(repo.vozovi);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task UkloniVoz_NevazeciId_Test(int id)
        {
            var rezultat = await servis.UkloniVoz(id);
            Assert.False(rezultat);
        }
    }
}
