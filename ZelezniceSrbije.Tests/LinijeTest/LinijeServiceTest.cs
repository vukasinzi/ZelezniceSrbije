using Microsoft.Data.Sqlite;

using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;
using ZelezniceSrbije.Services;
using Microsoft.EntityFrameworkCore;

namespace ZelezniceSrbije.Tests.LinijeTest
{
    public class LinijeServiceTest : IDisposable
    {
        private readonly VozAppContext context;
        private readonly SqliteConnection connection;
        private readonly LinijeServis servis;
        public LinijeServiceTest()
        {
            var db = TestBazaUMemoriji.KreirajContext();
            context = db.context;
            connection = db.connection;
            LinijeRepository repo = new(context);
            servis = new LinijeServis(repo);

        }
        public void Dispose()
        {
            context.Dispose();
            connection.Dispose();
        }
        [Fact]
        public async Task DodajLiniju_Uspesno_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
            var stanice_id = new List<int> { 6, 1, 2, 3 };
            var rezultat = await servis.DodajLiniju("Uzice - Subotica", 10, stanice_id, new List<int> { 6, 1, 2, 3 }, new List<int> { 0, 90, 180, 360 });
            
            Assert.True(rezultat);
            var linija = await context.Linija.FirstOrDefaultAsync(x => x.Naziv == "Uzice - Subotica");
            Assert.NotNull(linija);
            

            List<StanicaLinija> stanice = await context.StanicaLinija.Where(x => stanice_id.Contains(x.Stanica_id) && x.Linija_id == linija.Id).ToListAsync();
            Assert.Equal(4, stanice.Count);
        }
        [Fact]
        public async Task DodajLiniju_PrazanNaziv_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
            var rezultat = await servis.DodajLiniju("", 10, new List<int> { 6, 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }, new List<int> { 0, 90, 180, 360 });
            Assert.False(rezultat);
        }


        [Fact]
        public async Task DodajLiniju_NazivDuzi_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
            var rezultat = await servis.DodajLiniju(new string('A', 31), 10, new List<int> { 6, 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }, new List<int> { 0, 90, 180, 360 });
            Assert.False(rezultat);
        }
        [Fact]
        public async Task DodajLiniju_NulaCena_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
            var rezultat = await servis.DodajLiniju("Uzice - Subotica", 0, new List<int> { 6, 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }, new List<int> { 0, 90, 180, 360 });
            Assert.False(rezultat);
        }
        [Fact]
        public async Task DodajLiniju_NegativnaCena_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
            var rezultat = await servis.DodajLiniju("Uzice - Subotica", -5, new List<int> { 6, 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }, new List<int> { 0, 90, 180, 360 });
            Assert.False(rezultat);
        }
        [Fact]
        public async Task DodajLiniju_JednaStanica_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
            var rezultat = await servis.DodajLiniju("Uzice - Subotica", 10, new List<int> { 6 }, new List<int> { 1 }, new List<int> { 0 });
            Assert.False(rezultat);
        }

        [Fact]
        public async Task DodajLiniju_NullStanice_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
            var rezultat = await servis.DodajLiniju("Uzice - Subotica", 10, null, null, null);
            Assert.False(rezultat);
        }

        [Fact]
        public async Task DodajLiniju_LinijaVecPostoji_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
            await servis.DodajLiniju("Uzice - Subotica", 10, new List<int> { 6, 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }, new List<int> { 0, 90, 180, 360 });
            var rezultat = await servis.DodajLiniju("Uzice - Subotica", 10, new List<int> { 6, 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }, new List<int> { 0, 90, 180, 360 });
            Assert.False(rezultat);
        }


        /*stanice*/
        [Theory]
        [InlineData("Naziv","Region",true)]
        [InlineData(null,"Region",false)]
        [InlineData("Naziv",null,false)]
        [InlineData("Beograd Centar", "Savski Venac",false)]//vec postoji
        [InlineData("   ", "Region", false)] //samo razmaci
        [InlineData("Naziv", "   ", false)]  //samo razmaci
        public async Task DodajStanicu_Test(string naziv,string region,bool trebaDaUspe)
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
            var rezultat = await servis.DodajStanicu(naziv,region);
            if (trebaDaUspe)
            {
                var stanice = await servis.UcitajSveStanice(region);
                var nasa_stanica = stanice.FirstOrDefault(x => x.Naziv == naziv);
                Assert.True(rezultat);
                Assert.NotNull(nasa_stanica);
                Assert.Equal(naziv, nasa_stanica.Naziv);
            }
            else
                Assert.False(rezultat);
           
        }

        [Theory]
        [InlineData(1, true)]  
        [InlineData(0, false)] 
        [InlineData(-1, false)] 
        [InlineData(321213, false)] 
        public async Task UkloniLiniju_Test(int id, bool trebaDaUspe)
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);

            var rezultat = await servis.UkloniLiniju(id);

            Assert.Equal(trebaDaUspe, rezultat);
            if (trebaDaUspe)
            {
                var linija = await context.Linija.FindAsync(id);
                Assert.Null(linija);
            }
        }

        [Theory]
        [InlineData(1, true)]  
        [InlineData(0, false)]
        [InlineData(-1, false)] 
        [InlineData(321231, false)] 
        public async Task UkloniStanicu_Test(int id, bool trebaDaUspe)
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);

            var rezultat = await servis.UkloniStanicu(id);

            Assert.Equal(trebaDaUspe, rezultat);
            if (trebaDaUspe)
            {
                var stanica = await context.Stanica.FindAsync(id);
                Assert.Null(stanica);
            }
        }
        [Theory]
        [InlineData(null)]        
        [InlineData("")]         
        [InlineData("   ")]       
        [InlineData("Savski Venac")] 
        [InlineData("NepostojeciRegion")] 
        public async Task UcitajSveStanice_Test(string region)
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);

            var rezultat = await servis.UcitajSveStanice(region);

            Assert.NotNull(rezultat);
            if (!string.IsNullOrWhiteSpace(region))
                Assert.All(rezultat, s => Assert.Equal(region, s.Region));
        }

        [Fact]
        public async Task UcitajSveLinije_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);

            var rezultat = await servis.UcitajSveLinije();

            Assert.NotNull(rezultat);
            Assert.NotEmpty(rezultat);
            Assert.All(rezultat, l =>
            {
                Assert.NotNull(l.linija);
                Assert.NotEmpty(l.stanice);
            });
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public async Task IzmeniLiniju_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);

            var rezultat = await servis.IzmeniLiniju(1, "Novi Naziv", 10, new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 }, new List<int> { 0, 90, 180 });

            Assert.True(rezultat);
            var linija = await context.Linija.FindAsync(1);
            Assert.NotNull(linija);
            Assert.Equal("Novi Naziv", linija.Naziv);
            Assert.Equal(10, linija.Cena_po_minutu);
            var stanice = await context.StanicaLinija.Where(x => x.Linija_id == 1).ToListAsync();
            Assert.Equal(3, stanice.Count);
        }

        [Fact]
        public async Task IzmeniLiniju_PrazanNaziv_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);

            var rezultat = await servis.IzmeniLiniju(1, "", 10, new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 }, new List<int> { 0, 90, 180 });

            Assert.False(rezultat);
        }

        [Fact]
        public async Task IzmeniLiniju_NulaCena_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);

            var rezultat = await servis.IzmeniLiniju(1, "Novi Naziv", 0, new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 }, new List<int> { 0, 90, 180 });

            Assert.False(rezultat);
        }

        [Fact]
        public async Task IzmeniLiniju_NegativnaCena_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);

            var rezultat = await servis.IzmeniLiniju(1, "Novi Naziv", -5, new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 }, new List<int> { 0, 90, 180 });

            Assert.False(rezultat);
        }

        [Fact]
        public async Task IzmeniLiniju_NegativanId_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
            var rezultat = await servis.IzmeniLiniju(1, "Novi Naziv", 10, new List<int> { -1, 2, 3 }, new List<int> { 1, 2, 3 }, new List<int> { 0, 90, 180 });
            Assert.False(rezultat);
        }

        [Fact]
        public async Task IzmeniLiniju_NegativanRedosled_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
            var rezultat = await servis.IzmeniLiniju(1, "Novi Naziv", 10, new List<int> { 1, 2, 3 }, new List<int> { -1, 2, 3 }, new List<int> { 0, 90, 180 });
            Assert.False(rezultat);
        }

        [Fact]
        public async Task IzmeniLiniju_NazivPredugacak_Test()
        {
            await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
            var rezultat = await servis.IzmeniLiniju(1, "123456789101112131415161747127471247", 10, new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 }, new List<int> { 0, 90, 180 });
            Assert.False(rezultat);
        }

    }
}
