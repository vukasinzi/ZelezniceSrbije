using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public class LinijeRepository : ILinijeRepository
    {
        public VozAppContext db;
        public LinijeRepository(VozAppContext db)
        {
            this.db = db;
        }

        public async Task DodajLiniju(Linija l)
        {

            await db.Linija.AddAsync(l);
            await db.SaveChangesAsync();
        }

        public async Task DodajStajalistaZaLiniju(List<StanicaLinija> fejk)
        {
            await db.StanicaLinija.AddRangeAsync(fejk);
            await db.SaveChangesAsync();

        }

        public async Task DodajStanicu(Stanica s)
        {
            await db.Stanica.AddAsync(s);
            await db.SaveChangesAsync();

        }

        public async Task UkloniLiniju(int id)
        {
            var linija = await db.Linija.FindAsync(id);
            _ = db.Linija.Remove(linija);
            await db.SaveChangesAsync();
        }
        public async Task UkloniStanicu(int id)
        {
            var stanica = await db.Stanica.FindAsync(id);
            _ = db.Stanica.Remove(stanica);
            await db.SaveChangesAsync();
        }
        public async Task<object> ProveriLiniju(string naziv)
        {
            return await db.Linija.FirstOrDefaultAsync(x => x.Naziv == naziv);
        }
        public async Task<object> ProveriLiniju(int id)
        {
            return await db.Linija.FindAsync(id);
        }
        public async Task<object> ProveriStanicu(int id)
        {
            return await db.Stanica.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<object> ProveriStanicu(string naziv)
        {
            return await db.Stanica.FirstOrDefaultAsync(x => x.Naziv == naziv);
        }

        public async Task<List<LinijaDTO>> UcitajSveLinije()
        {
            var redovi = await (
              from sl in db.StanicaLinija.AsNoTracking()
              join l in db.Linija.AsNoTracking() on sl.Linija_id equals l.Id
              join s in db.Stanica.AsNoTracking() on sl.Stanica_id equals s.Id
              orderby l.Id, sl.Redosled
              select new { Linija = l, Stanica = s }
             ).ToListAsync();

            List<LinijaDTO> linije = redovi.GroupBy(x => x.Linija.Id).Select(g => new LinijaDTO
             {
                 linija = g.First().Linija,
                 stanice = g.Select(x => x.Stanica).ToList()
             })
             .ToList();
            return linije.OrderBy(x=> x.linija.Naziv).ToList();
        }

      
        public Task<List<Stanica>> UcitajSveStanice(string region)
        {
            if(region == null || region.Trim() == "")
              return db.Stanica.OrderBy(x=> x.Naziv).ToListAsync();

            return db.Stanica.Where(x => x.Region == region).ToListAsync();
        }

        public async Task IzmeniLiniju(Linija l, List<StanicaLinija> stajalista)
        {
           var linija =  await db.Linija.FindAsync(l.Id);
            linija.Naziv = l.Naziv;
            linija.Cena_po_minutu = l.Cena_po_minutu;
            await db.StanicaLinija.Where(x => x.Linija_id == l.Id).ExecuteDeleteAsync();
            await db.StanicaLinija.AddRangeAsync(stajalista);
            await db.SaveChangesAsync();
        }

        public async Task IzmeniStanicu(Stanica s)
        {
            var stanica = await db.Stanica.FindAsync(s.Id);
            stanica.Naziv = s.Naziv;
            stanica.Region = s.Region;
            await db.SaveChangesAsync();
        }
    }
}
