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
        public async Task<bool> UkloniLiniju(int id)
        {
            try
            {

                if (id <= 0)
                    return false;
                var provera = await repo.ProveriLiniju(id);
                if (provera == null)
                    return false;

                await repo.UkloniLiniju(id);
                return true;
            }
            catch(Exception x)
            {
                return false;
            }
            
        }
        public async Task<bool> UkloniStanicu(int id)
        {
            try
            {
                if (id <= 0)
                    return false;
                var provera = await repo.ProveriStanicu(id);
                if (provera == null)
                    return false;

                await repo.UkloniStanicu(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<LinijaDTO>> UcitajSveLinije()
        {
            return await repo.UcitajSveLinije();
        }

        public async Task<List<Stanica>> UcitajSveStanice(string region)
        {
            
            return await repo.UcitajSveStanice(region);

        }

        public async Task<bool> IzmeniLiniju(int id, string naziv, int cena_po_minutu, List<int> stanicaIds, List<int> redosled, List<int> vreme_od_polaska)
        {
            Linija l = new(id, naziv, cena_po_minutu);
            List<StanicaLinija> stajalista = new();
            if (!l.JeValidan())
                return false;
            if (stanicaIds.Count != redosled.Count || stanicaIds.Count != vreme_od_polaska.Count)
                return false;
            for (int i = 0; i < redosled.Count; i++)
            {
                if (stanicaIds[i] <= 0 || redosled[i] <= 0 || vreme_od_polaska[i] <= 0)
                    return false;
                StanicaLinija sl = new(vreme_od_polaska[i], redosled[i], stanicaIds[i], id);
                stajalista.Add(sl);
            }
            
            await repo.IzmeniLiniju(l,stajalista);
            return true;
        }

      

        public async Task<bool> IzmeniStanicu(int id, string naziv, string region)
        {
            Stanica s = new(id, naziv, region);
            if (!s.JeValidan())
                return false;
            await repo.IzmeniStanicu(s);
            return true;
        }
    }
}
