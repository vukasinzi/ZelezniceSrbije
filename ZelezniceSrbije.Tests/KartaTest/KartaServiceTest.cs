using Microsoft.Data.Sqlite;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Repositories;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Tests.KartaTest;

public class KartaServiceTest : IDisposable
{
    private readonly VozAppContext context;
    private readonly KartaService servis;
    private readonly SqliteConnection connection;

    public KartaServiceTest()
    {
        var db = TestBazaUMemoriji.KreirajContext();
        context = db.context;
        connection = db.connection;
        var repo = new KartaRepository(context);
        servis = new KartaService(repo);

    }
    public void Dispose()
    {
        connection.Dispose();
        context.Dispose();
    }

    [Theory]
    [InlineData(101, 1, 1, 2, true)] // uspesna kupovina beograd novi sad
    [InlineData(102, 6, 1, 4, true)] // uspesna kupovina cela ruta beograd nis
    [InlineData(101, 7, 1, 5, true)] // uspesno deo rute beograd kragujevac
    [InlineData(102, 8, 5, 4, true)] // uspesno deo rute kragujevac nis
    [InlineData(101, 10, 8, 6, true)] // uspesno cacak uzice
    [InlineData(999, 1, 1, 2, false)] // pada jer putnik ne postoji
    [InlineData(101, 999, 1, 2, false)] // pada jer raspored ne postoji
    [InlineData(101, 1, 999, 2, false)] // pada jer polaziste ne postoji
    [InlineData(101, 1, 1, 1, false)] // pada jer je ista stanica
    [InlineData(101, 1, 2, 1, false)] // pada zbog pogresnog smera
    [InlineData(102, 6, 4, 5, false)] // pada zbog pogresnog smera na duzoj liniji
    [InlineData(101, 1, 1, 3, false)] // pada jer stanica nije na tom rasporedu
    [InlineData(101, 13, 1, 2, false)] // pada jer stanice pripadaju drugoj liniji    
    
    public async Task KupiKartu_Test(int putnik_id, int raspored_id, int polaziste_id, int odrediste_id,bool trebaDaUspe)
    {
        await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
        var rezultat = await servis.Kupi(putnik_id, raspored_id, polaziste_id, odrediste_id);

        if (trebaDaUspe)
        {
            Assert.NotNull(rezultat);
            Assert.Equal(1, context.Karta.Count());
        }
        else
        {
            Assert.Null(rezultat);
            Assert.Equal(0, context.Karta.Count());
        }
    }
    [Theory]
    [InlineData(1, 101, true)] // uspesno vraca postojecu kartu za ispravnog putnika
    [InlineData(999, 101, false)] // vraca null jer trazena karta ne postoji u bazi
    [InlineData(1, 102, false)] // vraca null jer postojeca karta pripada drugom putniku
    public async Task VratiPodatke_JednaKarta_Test(int karta_id, int putnik_id, bool trebaDaUspe)
    {
        await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
        
        Guid testToken = Guid.NewGuid();
        context.Karta.Add(new Karta(450m, 101, 1, 1, 2, testToken) { Id = 1 });
        await context.SaveChangesAsync();

        var rezultat = await servis.VratiPodatke(karta_id, putnik_id);

        if (trebaDaUspe)
        {
            Assert.NotNull(rezultat);
            Assert.Equal(1, rezultat.karta_id);
            Assert.Equal(testToken, rezultat.qr_token);
        }
        else
        {
            Assert.Null(rezultat);
        }
    }

    [Theory]
    [InlineData(101, true)] // uspesno vraca listu karata za putnika koji ima kupljene karte
    [InlineData(102, false)] // vraca null jer putnik postoji ali nema kupljene karte
    [InlineData(999, false)] // vraca null jer putnik ne postoji
    public async Task VratiPodatke_SveKarte_Test(int putnik_id, bool trebaDaUspe)
    {
        await TestBazaUMemoriji.PopuniSvePodatkeAsync(context);
        
        context.Karta.AddRange(
            new Karta(450m, 101, 1, 1, 2, Guid.NewGuid()) { Id = 1 },
            new Karta(1200m, 101, 6, 1, 4, Guid.NewGuid()) { Id = 2 }
        );
        await context.SaveChangesAsync();

        var rezultat = await servis.VratiPodatke(putnik_id);

        if (trebaDaUspe)
        {
            Assert.NotNull(rezultat);
            Assert.Equal(2, rezultat.Count);
        }
        else
        {
            Assert.Null(rezultat);
        }
    }
}