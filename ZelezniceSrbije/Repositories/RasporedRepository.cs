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
        public async Task<List<Raspored>> PretraziAsync(string polaziste, string odrediste, DateTime datum)
        {
           // var lista = await db.Stajaliste.Where(s => s.IdLinija == r.Linija.Id).ToListAsync();

            return new List<Raspored>();
        }
        
        public async Task<List<Stanica>> UcitajStaniceAsync()
        {
            List<Stanica> stanice = await db.Stanica.AsNoTracking().ToListAsync();
            return await Task.FromResult(stanice);
        }
    }
}
