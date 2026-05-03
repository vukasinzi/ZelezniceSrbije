using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Models.ViewModels;

namespace ZelezniceSrbije.Repositories;

public class KartaRepository : IKartaRepository
{
    private readonly VozAppContext db;

    public KartaRepository(VozAppContext db)
    {
        this.db = db;
    }

    public async Task<bool> ProveriKartu(int putnik_id, int raspored_id, int polaziste_id, int odrediste_id)
    {
        var ok = await db.Putnik.AnyAsync(p => p.Id == putnik_id)
                 && await db.Raspored.AnyAsync(r => r.Id == raspored_id) &&
                 await db.StanicaLinija.AnyAsync(slp => slp.Stanica_id == polaziste_id)
                 && await db.StanicaLinija.AnyAsync(slo => slo.Stanica_id == odrediste_id);
        return ok;
    }

    public async Task<Karta> KupiKartu(int putnik_id, int raspored_id, int polaziste_id, int odrediste_id)
    {
        var podaci = await (
            from r in db.Raspored
            join v in db.Voz on r.Voz_id equals v.Id
            join tv in db.TipVoza on v.Tip_voza_id equals tv.Id
            join l in db.Linija on r.Linija_id equals l.Id
            join pol in db.StanicaLinija on l.Id equals pol.Linija_id
            join odr in db.StanicaLinija on l.Id equals odr.Linija_id
            join sp in db.Stanica on pol.Stanica_id equals sp.Id
            join so in db.Stanica on odr.Stanica_id equals so.Id
            where r.Id == raspored_id
                  && pol.Stanica_id == polaziste_id
                  && odr.Stanica_id == odrediste_id
                  && pol.Redosled < odr.Redosled
            select new
            {
                CenaKarte = (decimal)((odr.Vreme_od_polaska - pol.Vreme_od_polaska) * l.Cena_po_minutu),
                PolazisteNaziv = sp.Naziv,
                OdredisteNaziv = so.Naziv,
                LinijaNaziv = l.Naziv,
                TipVozaNaziv = tv.Naziv,
                Trajanje = odr.Vreme_od_polaska - pol.Vreme_od_polaska,
                VremePolaska = r.Vreme_polaska.AddMinutes(pol.Vreme_od_polaska),
                VremeDolaska = r.Vreme_polaska.AddMinutes(odr.Vreme_od_polaska)
            }
        ).FirstOrDefaultAsync();

        if (podaci == null)
            return null;

        Karta x = new(
            podaci.CenaKarte,
            putnik_id,
            raspored_id,
            podaci.PolazisteNaziv,
            podaci.OdredisteNaziv,
            podaci.LinijaNaziv,
            podaci.TipVozaNaziv,
            podaci.VremePolaska,
            podaci.VremeDolaska,
            podaci.Trajanje,
            null,
            Guid.NewGuid()
        );

        await db.Karta.AddAsync(x);
        await db.SaveChangesAsync();
        return x;
    }

    public async Task<KartaDTO> VratiKartu(int karta_id, int putnik_id)
    {
        /*
               *

                 //imam gresku za polaske, moram konvertovati u datum.
              select ka.id as karta_id,k.ime + ' ' + k.prezime as korisnik, ka.cena as cena_karte, sp.naziv as polaziste,so.naziv as odrediste,l.naziv as linija,tv.naziv as tip_voza,
               (odr.vreme_od_polaska - pol.vreme_od_polaska) as trajanje_min,
               dateadd(minute, pol.vreme_od_polaska, r.vreme_polaska) as vreme_polaska,
               dateadd(minute, odr.vreme_od_polaska, r.vreme_polaska) as vreme_dolaska,
               ka.ocitana,
               ka.datum_ocitavanja
               from Karta ka
               join Putnik p on p.id = ka.putnik_id
               join Korisnik k on k.id = p.id
               join Raspored r on r.id = ka.raspored_id
               join Linija l on l.id = r.linija_id
               join Voz v on v.id = r.voz_id
               join TipVoza tv on tv.id = v.tip_voza_id
               join StanicaLinija pol on pol.id = ka.polaziste_id
               join StanicaLinija odr on odr.id = ka.odrediste_id
               join Stanica sp on sp.id = pol.stanica_id
               join Stanica so on so.id = odr.stanica_id
               where ka.id = @karta_id and ka.putnik_id = @putnik_id and pol.redosled < odr.redosled;
    Nakon izmene arhitekture baze gde je karta sada arhiva, nema potrebe za ovoliko joinova.

               */
        var podaci = await (
            from ka in db.Karta
            join p in db.Putnik on ka.Putnik_id equals p.Id
            join k in db.Korisnik on p.Id equals k.Id
            where ka.Id == karta_id && ka.Putnik_id == putnik_id
            select new
            {
                KartaId = ka.Id,
                Korisnik = k.Ime + " " + k.Prezime,
                CenaKarte = ka.Cena,
                Polaziste = ka.Polaziste,
                Odrediste = ka.Odrediste,
                Linija = ka.Linija,
                TipVoza = ka.Tip_voza,
                Trajanje = ka.Trajanje_min,
                VremePolaska = ka.Vreme_polaska,
                VremeDolaska = ka.Vreme_dolaska,
                Ocitana = ka.Ocitana,
                DatumOcitavanja = ka.Datum_ocitavanja,
                QrToken = ka.Qr_token
            }
        ).FirstOrDefaultAsync();

        if (podaci == null)
            return null;

        KartaDTO karta = new(
            podaci.KartaId, 
            podaci.CenaKarte, 
            podaci.Korisnik, 
            podaci.Polaziste, 
            podaci.Odrediste,
            podaci.Linija, 
            podaci.TipVoza, 
            podaci.Trajanje.ToString(), 
            podaci.VremePolaska, 
            podaci.VremeDolaska,
            podaci.Ocitana, 
            podaci.DatumOcitavanja,
            podaci.QrToken
        );
        return karta;
    }

    public async Task<List<KartaDTO>> VratiKarte(int putnik_id)
    {
        var podaci = await (
            from ka in db.Karta
            join p in db.Putnik on ka.Putnik_id equals p.Id
            join k in db.Korisnik on p.Id equals k.Id
            where ka.Putnik_id == putnik_id
            orderby ka.Vreme_polaska descending, ka.Id descending
            select new
            {
                KartaId = ka.Id,
                Korisnik = k.Ime + " " + k.Prezime,
                CenaKarte = ka.Cena,
                Polaziste = ka.Polaziste,
                Odrediste = ka.Odrediste,
                Linija = ka.Linija,
                TipVoza = ka.Tip_voza,
                Trajanje = ka.Trajanje_min,
                VremePolaska = ka.Vreme_polaska,
                VremeDolaska = ka.Vreme_dolaska,
                Ocitana = ka.Ocitana,
                DatumOcitavanja = ka.Datum_ocitavanja,
                QrToken = ka.Qr_token
            }
        ).ToListAsync();

        if (podaci.Count == 0)
        {
            return null;
        }

        return podaci
            .Select(x => new KartaDTO(
                x.KartaId,
                x.CenaKarte,
                x.Korisnik,
                x.Polaziste,
                x.Odrediste,
                x.Linija,
                x.TipVoza,
                x.Trajanje.ToString(),
                x.VremePolaska,
                x.VremeDolaska,
                x.Ocitana,
                x.DatumOcitavanja,
                x.QrToken
            ))
            .ToList();
    }
}