using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Tests.PretragaTest
{
    public class RasporedRepositoryTest
    {
        private async Task PopuniTestPodatke(VozAppContext context)
        {
            var bg = new Stanica(1, "Beograd Centar", "Savski Venac");
            var ns = new Stanica(2, "Novi Sad", "Novi Sad");
            var sub = new Stanica(3, "Subotica", "Subotica");
            context.Stanica.AddRange(bg, ns, sub);

            context.TipVoza.Add(new TipVoza { Id = 1, Naziv = "SOKO", Opis = "Brzi voz" });
            var voz = new Voz(1, "IC", "Letacki", true, 1);
            context.Voz.Add(voz);



            var linijaBgNs = new Linija { Id = 1, Naziv = "Beograd centar - Novi Sad" };
            var linijaNsSub = new Linija { Id = 2, Naziv = "Novi Sad - Subotica" };
            context.Linija.AddRange(linijaBgNs, linijaNsSub);

            context.StanicaLinija.AddRange(
                new StanicaLinija { Linija_id = 1, Stanica_id = 1, Redosled = 1, Vreme_od_polaska = 0 },
                new StanicaLinija { Linija_id = 1, Stanica_id = 2, Redosled = 2, Vreme_od_polaska = 90 },
                new StanicaLinija { Linija_id = 2, Stanica_id = 2, Redosled = 1, Vreme_od_polaska = 0 },
                new StanicaLinija { Linija_id = 2, Stanica_id = 3, Redosled = 2, Vreme_od_polaska = 120 }
            );

            var danas = DateTime.Today;
            context.Raspored.AddRange(
                new Raspored { Id = 1, Linija_id = 1, Voz_id = 1, Vreme_polaska = danas.AddHours(8) },
                new Raspored { Id = 2, Linija_id = 1, Voz_id = 1, Vreme_polaska = danas.AddHours(12) },
                new Raspored { Id = 3, Linija_id = 1, Voz_id = 1, Vreme_polaska = danas.AddDays(1).AddHours(8) },
                new Raspored { Id = 4, Linija_id = 2, Voz_id = 1, Vreme_polaska = danas.AddHours(10) }
            );

            await context.SaveChangesAsync();
        }
        private VozAppContext KreirajContext(string ime)
        {
            var opcije = new DbContextOptionsBuilder<VozAppContext>()
                .UseInMemoryDatabase(ime)
                .Options;
            return new VozAppContext(opcije);
        }
        [Fact]
        public async Task Pretraga_DatumNepostojeci_Test()
        {
            VozAppContext context = KreirajContext(Guid.NewGuid().ToString());
            await PopuniTestPodatke(context);
            RasporedRepository repo = new(context);

            var rezultat = await repo.PretraziAsync("Beograd Centar", "Novi Sad", DateTime.Today.AddDays(10));
            Assert.Empty(rezultat);
        }
        [Fact]
        public async Task Pretraga_PogresanRedosled_Test()
        {
            VozAppContext context = KreirajContext(Guid.NewGuid().ToString());
            await PopuniTestPodatke(context);
            RasporedRepository repo = new(context);
            var rezultat = await repo.PretraziAsync("Novi Sad", "Beograd Centar", DateTime.Today);

            Assert.Empty(rezultat);
        }
        [Fact]
        public async Task Pretraga_PogresnaLinija_Test()
        {
            VozAppContext context = KreirajContext(Guid.NewGuid().ToString());
            await PopuniTestPodatke(context);
            RasporedRepository repo = new(context);
            var rezultat = await repo.PretraziAsync("Beograd Centar", "Subotica", DateTime.Today);
            Assert.Empty(rezultat);

        }
        [Fact]
        public async Task Pretraga_BrojPolazaka_Test()
        {
            VozAppContext context = KreirajContext(Guid.NewGuid().ToString());
            await PopuniTestPodatke(context);
            RasporedRepository repo = new(context);
            var rezultat = await repo.PretraziAsync("Beograd Centar", "Novi Sad", DateTime.Today);
            Assert.Equal(2,rezultat.Count);
        }
        [Theory]
        [InlineData("Beograd Centar", "Novi Sad", 2)]
        [InlineData("Novi Sad", "Subotica", 1)]
        [InlineData("Beograd Centar", "Subotica", 0)]
        public async Task PretraziAsync_RazliciteRelacije_VracaOdgovarajuciBroj(string pol, string odr, int ocekivanBroj)
        {
            VozAppContext context = KreirajContext(Guid.NewGuid().ToString());
            await PopuniTestPodatke(context);
            RasporedRepository repo = new(context);
      
            var rezultat = await repo.PretraziAsync(pol, odr, DateTime.Today);
            Assert.NotNull(rezultat);
            Assert.Equal(ocekivanBroj,rezultat.Count);
        }

    }
}
