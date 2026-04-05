using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Services
{
    public class LinijeServis : ILinijeServis
    {
        ILinijeRepository repo;
        public LinijeServis(ILinijeRepository repo)
        {
            this.repo = repo;
        }

        public async Task<bool> DodajLiniju(string naziv, int cena_po_minutu, List<int> stanicaIds, List<int> redosled, List<int> vreme_od_polaska)
        {
            Linija l = new(naziv, cena_po_minutu);
            if (!l.JeValidan())
                return false;
            if (stanicaIds == null || stanicaIds.Count <= 1)
                return false;

            var provera = await repo.ProveriLiniju(naziv);
            if (provera == null)
            {
                List<StanicaLinija> fejk = new();
                await repo.DodajLiniju(l);
                for(int i =0;i<stanicaIds.Count;i++)
                {
                    StanicaLinija sl = new(vreme_od_polaska[i], redosled[i], stanicaIds[i], l.Id);
                    fejk.Add(sl);
                }
                await repo.DodajStajalistaZaLiniju(fejk);
                fejk.Clear();
                return true;
            }
            return false;
        }

    
        public async Task<bool> DodajStanicu(string naziv, string region)
        {
            Stanica s = new(naziv, region);
            if (!s.JeValidan())
                return false;
            var provera = await repo.ProveriStanicu(naziv);
            if (provera == null)
            {
                await repo.DodajStanicu(s);
                return true;
            }
            return false;
        }

        public async Task<List<LinijaDTO>> UcitajSveLinije()
        {
            return await repo.UcitajSveLinije();
        }

        public async Task<List<Stanica>> UcitajSveStanice(string region)
        {
            
            return await repo.UcitajSveStanice(region);

        }
    }
}
