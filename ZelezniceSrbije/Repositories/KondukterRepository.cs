using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories;

public class KondukterRepository : IKondukterRepository
{
    private VozAppContext db;
    public KondukterRepository(VozAppContext db) => this.db = db;
    public async Task<RasporedDTO?> VratiRaspored(int raspored_id)
    {
        var podaci = await (
            from r in db.Raspored
            join v in db.Voz on r.Voz_id equals v.Id
            join tv in db.TipVoza on v.Tip_voza_id equals tv.Id
            join l in db.Linija on r.Linija_id equals l.Id
            join pol in db.StanicaLinija on l.Id equals pol.Linija_id
            join odr in db.StanicaLinija on l.Id equals odr.Linija_id
            where r.Id == raspored_id
                  && pol.Redosled == db.StanicaLinija.Where(x => x.Linija_id == l.Id).Min(x => x.Redosled)
                  && odr.Redosled == db.StanicaLinija.Where(x => x.Linija_id == l.Id).Max(x => x.Redosled)
            select new
            {
                r.Id,
                Linija = l.Naziv,
                TipVoza = tv.Naziv,
                PolazakSaPol = r.Vreme_polaska.AddMinutes(pol.Vreme_od_polaska),
                DolazakNaOdr = r.Vreme_polaska.AddMinutes(odr.Vreme_od_polaska)
            }
        ).FirstOrDefaultAsync();

        if (podaci == null) return null;

        return new RasporedDTO
        {
            Id = podaci.Id,
            Linija = podaci.Linija,
            TipVoza = podaci.TipVoza,
            PolazakSaPol = podaci.PolazakSaPol,
            DolazakNaOdr = podaci.DolazakNaOdr
        };
    }

    public async Task<List<RasporedDTO>?> VratiRasporedeZaDanas()
    {
        DateTime dtm = DateTime.Now;
        var podaci = await (
            from r in db.Raspored
            join v in db.Voz on r.Voz_id equals v.Id
            join tv in db.TipVoza on v.Tip_voza_id equals tv.Id
            join l in db.Linija on r.Linija_id equals l.Id
            where r.Vreme_polaska.Date == dtm.Date && r.Vreme_polaska >= dtm
            orderby r.Vreme_polaska
            select new RasporedDTO
            {
                Id = r.Id,
                Linija = l.Naziv,
                TipVoza = tv.Naziv,
                PolazakSaPol = r.Vreme_polaska
            }
        ).ToListAsync();

        return podaci.Count == 0 ? null : podaci;
    }
    public async Task<bool> OcitajKartu(Guid token, int raspored_id)
    {
        var karta = await db.Karta.FirstOrDefaultAsync(k => k.Qr_token == token && k.Raspored_id == raspored_id && !k.Ocitana);
        if (karta == null) return false;

        karta.Ocitana = true;
        karta.Datum_ocitavanja = DateTime.Now;
        await db.SaveChangesAsync();
        return true;
    }
}