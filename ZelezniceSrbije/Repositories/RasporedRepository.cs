using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    /// <inheritdoc/>
    public class RasporedRepository : IRasporedRepository
    {
        /// <summary>
        /// Kontekst baze podataka aplikacije.
        /// </summary>
        private readonly VozAppContext db;

        /// <summary>
        /// Kreira novi repozitorijum za rasporede.
        /// </summary>
        /// <param name="db">Kontekst baze podataka aplikacije.</param>
        public RasporedRepository(VozAppContext db)
        {
            this.db = db;
        }

        /// <inheritdoc/>
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
            var odKad = dan == DateTime.Now.Date ? DateTime.Now : dan;

            List<RasporedDTO> rezultat = await (
                from r in db.Raspored
                join l in db.Linija on r.Linija_id equals l.Id
                join v in db.Voz on r.Voz_id equals v.Id
                join slPol in db.StanicaLinija on l.Id equals slPol.Linija_id
                join slOdr in db.StanicaLinija on l.Id equals slOdr.Linija_id
                where slPol.Stanica_id == pol.Id
                      && slOdr.Stanica_id == odr.Id
                      && slPol.Redosled < slOdr.Redosled
                      && r.Vreme_polaska.AddMinutes(slPol.Vreme_od_polaska) >=
                      odKad //vazna ispravka, ne smem proveravati kad je voz krenuo, vec ako nije jos dosao na tu stanicu
                      && r.Vreme_polaska <
                      sutra //treba svakako da omogucim kupovinu karte, bez obzira na to dal je krenuo sa pocetne stanice ili ne. ona je nebitna
                select new RasporedDTO
                {
                    Id = r.Id,
                    Linija = l.Naziv,
                    TipVoza = v.TipVoza.Naziv,
                    PolazakSaPol = r.Vreme_polaska.AddMinutes(slPol.Vreme_od_polaska),
                    DolazakNaOdr = r.Vreme_polaska.AddMinutes(slOdr.Vreme_od_polaska)
                }
            ).ToListAsync();

            return await Task.FromResult(rezultat);
        }

        /// <inheritdoc/>
        public async Task<Raspored> ProveriRaspored(int id)
        {
            return await db.Raspored.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<List<Raspored>> UcitajRasporede(DateTime? datum)
        {
            var dan = datum.Value.Date;
            var sutra = dan.AddDays(1);

            return await db.Raspored
                .AsNoTracking()
                .Where(r => r.Vreme_polaska >= dan && r.Vreme_polaska < sutra)
                .OrderBy(r => r.Vreme_polaska)
                .Include(r => r.Linija)
                .Include(r => r.Voz)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Stanica>> UcitajStaniceAsync()
        {
            List<Stanica> stanice = await db.Stanica.AsNoTracking().ToListAsync();
            return await Task.FromResult(stanice);
        }

        /// <inheritdoc/>
        public async Task UkloniRaspored(int id)
        {
            var raspored = await db.Raspored.FindAsync(id);
            if (raspored == null) return;

            db.Raspored.Remove(raspored);
            await db.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> DodajRaspored(Raspored r)
        {
            var postojiLinija = await db.Linija.AnyAsync(x => x.Id == r.Linija_id);
            if (!postojiLinija)
                return false;

            var postojiVoz = await db.Voz.AnyAsync(x => x.Id == r.Voz_id);
            if (!postojiVoz)
                return false;

            await db.Raspored.AddAsync(r);
            await db.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> IzmeniRaspored(Raspored r)
        {
            var nas_raspored = await db.Raspored.FindAsync(r.Id);
            if (nas_raspored == null)
                return false;

            var postojiLinija = await db.Linija.AnyAsync(x => x.Id == r.Linija_id);
            if (!postojiLinija)
                return false;

            var postojiVoz = await db.Voz.AnyAsync(x => x.Id == r.Voz_id);
            if (!postojiVoz)
                return false;

            nas_raspored.Voz_id = r.Voz_id;
            nas_raspored.Linija_id = r.Linija_id;
            nas_raspored.Vreme_polaska = r.Vreme_polaska;

            await db.SaveChangesAsync();

            return true;
        }
    }
}