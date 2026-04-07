using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public class RasporedRepository : IRasporedRepository
    {
        private readonly VozAppContext db;
        public RasporedRepository(VozAppContext db)
        {
            this.db = db;
        }
        public async Task<List<RasporedDTO>> PretraziAsync(string polaziste, string odrediste, DateTime datum)
        {
            Stanica pol = await db.Stanica.FirstOrDefaultAsync(x => x.Naziv == polaziste);
            Stanica odr = await db.Stanica.FirstOrDefaultAsync(x => x.Naziv == odrediste);
            if (pol == null || odr == null)
                return null;

            /*
             polazna stanica mora biti pre odredisne stanice.
             treba kalkulisati vreme rasporeda+dolazak na tu stanicu
            select distinct r.id,r.vreme_polaska,l.naziv,v.naziv from Raspored r
            join Linija l on (r.linija_id = l.id)
            join Voz v on (r.voz_id = v.id)
            join StanicaLinija sl_pol on(l.id = sl_pol.linija_id)
            join StanicaLinija sl_odr on(l.id = sl_odr.linija_id)
            where sl_pol.stanica_id = 1 and sl_odr.stanica_id = 2
            and sl_pol.redosled < sl_odr.redosled
            order by r.vreme_polaska asc
            kod testiran u ssmsu, sve top.
            
            //fali samo deo za datume al to je dodato ovde . r.vremepolaska >= dan  i r. vremepolaska< sutra
             
             */
            var dan = datum.Date;
            var sutra = dan.AddDays(1);
            List<RasporedDTO> rezultat = await (
             from r in db.Raspored
             join l in db.Linija on r.Linija_id equals l.Id
             join v in db.Voz on r.Voz_id equals v.Id
             join slPol in db.StanicaLinija on l.Id equals slPol.Linija_id
             join slOdr in db.StanicaLinija on l.Id equals slOdr.Linija_id
             where slPol.Stanica_id == pol.Id
                && slOdr.Stanica_id == odr.Id
                && slPol.Redosled < slOdr.Redosled &&
                r.Vreme_polaska >= dan
                && r.Vreme_polaska < sutra
             select new RasporedDTO
             {
                 Linija = l.Naziv,
                 TipVoza = v.TipVoza.Naziv,
                 PolazakSaPol = r.Vreme_polaska.AddMinutes(slPol.Vreme_od_polaska),
                 DolazakNaOdr = r.Vreme_polaska.AddMinutes(slOdr.Vreme_od_polaska)
             }
             ).ToListAsync();

            return await Task.FromResult(rezultat);
            
        }
      
        public async Task<Raspored> ProveriRaspored(int id)
        {
            return await db.Raspored.FindAsync(id);
        }

        public async Task<List<Raspored>> UcitajRasporede(DateTime? datum)
        {
            var dan = datum.Value.Date;
            var sutra = dan.AddDays(1);

            return await db.Raspored
                .AsNoTracking()
                .Where(r => r.Vreme_polaska >= dan && r.Vreme_polaska < sutra)
                .OrderByDescending(r => r.Vreme_polaska)
                .Include(r => r.Linija)
                .Include(r => r.Voz)
                .ToListAsync();
        }

        public async Task<List<Stanica>> UcitajStaniceAsync()
        {
            List<Stanica> stanice = await db.Stanica.AsNoTracking().ToListAsync();
            return await Task.FromResult(stanice);
        }

        public async Task UkloniRaspored(int id)
        {
            var raspored = await db.Raspored.FindAsync(id);
            if (raspored == null) return;

            db.Raspored.Remove(raspored);
            await db.SaveChangesAsync();
        }
        public async Task DodajRaspored(Raspored r)
        {
            await db.Raspored.AddAsync(r);
            await db.SaveChangesAsync();
        }

        public async Task IzmeniRaspored(Raspored r)
        {
            var nas_raspored = await db.Raspored.FindAsync(r.Id);
            if (nas_raspored == null)
                return;
            nas_raspored.Voz_id = r.Voz_id;
            nas_raspored.Linija_id = r.Linija_id;
            nas_raspored.Vreme_polaska = r.Vreme_polaska;
            await db.SaveChangesAsync();
        }
    }
}
