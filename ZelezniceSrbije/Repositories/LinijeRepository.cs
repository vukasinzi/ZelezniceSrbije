using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    /// <inheritdoc/>
    public class LinijeRepository : ILinijeRepository
    {
        /// <summary>
        /// Kontekst baze podataka aplikacije.
        /// </summary>
        public VozAppContext db;

        /// <summary>
        /// Kreira novi repozitorijum za linije i stanice.
        /// </summary>
        /// <param name="db">Kontekst baze podataka aplikacije.</param>
        public LinijeRepository(VozAppContext db)
        {
            this.db = db;
        }

        /// <inheritdoc/>
        public async Task DodajLinijuSaStajalistima(Linija l, List<StanicaLinija> stajalista)
        {
            await db.Linija.AddAsync(l);
            foreach (var stajaliste in stajalista)
                stajaliste.Linija = l;
            await db.StanicaLinija.AddRangeAsync(stajalista);
            await db.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DodajStanicu(Stanica s)
        {
            await db.Stanica.AddAsync(s);
            await db.SaveChangesAsync();

        }

        /// <inheritdoc/>
        public async Task UkloniLiniju(int id)
        {
            var linija = await db.Linija.FindAsync(id);
            _ = db.Linija.Remove(linija);
            await db.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UkloniStanicu(int id)
        {
            var stanica = await db.Stanica.FindAsync(id);
            _ = db.Stanica.Remove(stanica);
            await db.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<object> ProveriLiniju(string naziv)
        {
            return await db.Linija.FirstOrDefaultAsync(x => x.Naziv == naziv);
        }

        /// <inheritdoc/>
        public async Task<object> ProveriLiniju(int id)
        {
            return await db.Linija.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<object> ProveriStanicu(int id)
        {
            return await db.Stanica.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc/>
        public async Task<object> ProveriStanicu(string naziv)
        {
            return await db.Stanica.FirstOrDefaultAsync(x => x.Naziv == naziv);
        }

        /// <inheritdoc/>
        public async Task<List<LinijaDTO>> UcitajSveLinije()
        {
            var redovi = await (
              from sl in db.StanicaLinija.AsNoTracking()
              join l in db.Linija.AsNoTracking() on sl.Linija_id equals l.Id
              join s in db.Stanica.AsNoTracking() on sl.Stanica_id equals s.Id
              orderby l.Id, sl.Redosled
              select new { Linija = l, Stanica = s ,StanicaLinija = sl}
             ).ToListAsync();

            List<LinijaDTO> linije = redovi.GroupBy(x => x.Linija.Id).Select(g => new LinijaDTO
             {
                 linija = g.First().Linija,
                 stanice = g.Select(x => x.Stanica).ToList(),
                 vreme_od_polaska = g.Select(x => x.StanicaLinija.Vreme_od_polaska).ToList()

             })
             .ToList();
            return linije.OrderBy(x=> x.linija.Naziv).ToList();
        }

        /// <inheritdoc/>
        public Task<List<Stanica>> UcitajSveStanice(string region)
        {
            if(region == null || region.Trim() == "")
              return db.Stanica.OrderBy(x=> x.Naziv).ToListAsync();

            return db.Stanica.Where(x => x.Region == region).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> IzmeniLiniju(Linija l, List<StanicaLinija> stajalista)
        {
            var linija = await db.Linija.FindAsync(l.Id);
            if (linija == null)
                return false;
            linija.Naziv = l.Naziv;
            linija.Cena_po_minutu = l.Cena_po_minutu;
            await db.StanicaLinija.Where(x => x.Linija_id == l.Id).ExecuteDeleteAsync();
            await db.StanicaLinija.AddRangeAsync(stajalista);
            await db.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> IzmeniStanicu(Stanica s)
        {
            var stanica = await db.Stanica.FindAsync(s.Id);
            if (stanica == null)
                return false;
            stanica.Naziv = s.Naziv;
            stanica.Region = s.Region;
            await db.SaveChangesAsync();
            return true;
        }
    }
}
