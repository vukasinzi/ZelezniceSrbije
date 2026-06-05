using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Services
{
    /// <inheritdoc/>
    public class LinijeService : ILinijeServis
    {
        /// <summary>
        /// Repozitorijum za rad sa linijama i stanicama.
        /// </summary>
        ILinijeRepository repo;

        /// <summary>
        /// Kreira novi servis za linije i stanice.
        /// </summary>
        /// <param name="repo">Repozitorijum za rad sa linijama i stanicama.</param>
        public LinijeService(ILinijeRepository repo)
        {
            this.repo = repo;
        }
        
        /// <inheritdoc/>
        public async Task<bool> DodajLiniju(string naziv, int cena_po_minutu, List<int> stanicaIds, List<int> redosled, List<int> vreme_od_polaska)
        {
            Linija l = new(naziv, cena_po_minutu);
            if (!l.JeValidan())
                return false;
            if (stanicaIds == null|| redosled == null || vreme_od_polaska == null)
                return false;
            if (stanicaIds.Count <= 1)
                return false;
            if (stanicaIds.Count != redosled.Count || stanicaIds.Count != vreme_od_polaska.Count)
                return false;

            var provera = await repo.ProveriLiniju(naziv);
            if (provera == null)
            {
                List<StanicaLinija> stajalista = new();
                for(int i =0;i<stanicaIds.Count;i++)
                {
                    if (stanicaIds[i] <= 0 || redosled[i] <= 0 || vreme_od_polaska[i] < 0)
                        return false;
                    stajalista.Add(new StanicaLinija(vreme_od_polaska[i], redosled[i], stanicaIds[i], 0));
                }
                await repo.DodajLinijuSaStajalistima(l, stajalista);
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<List<LinijaDTO>> UcitajSveLinije()
        {
            return await repo.UcitajSveLinije();
        }

        /// <inheritdoc/>
        public async Task<List<Stanica>> UcitajSveStanice(string region)
        {
            
            return await repo.UcitajSveStanice(region);

        }

        /// <inheritdoc/>
        public async Task<bool> IzmeniLiniju(int id, string naziv, int cena_po_minutu, List<int> stanicaIds, List<int> redosled, List<int> vreme_od_polaska)
        {
            Linija l = new(id, naziv, cena_po_minutu);
            List<StanicaLinija> stajalista = new();
            if (!l.JeValidan())
                return false;
            if (stanicaIds == null || redosled == null || vreme_od_polaska == null)
                return false;
            if (stanicaIds.Count != redosled.Count || stanicaIds.Count != vreme_od_polaska.Count)
                return false;
            for (int i = 0; i < redosled.Count; i++)
            {
                if (stanicaIds[i] <= 0 || redosled[i] <= 0 || vreme_od_polaska[i] < 0)
                    return false;
                StanicaLinija sl = new(vreme_od_polaska[i], redosled[i], stanicaIds[i], id);
                stajalista.Add(sl);
            }
            return await repo.IzmeniLiniju(l, stajalista);
        }

        /// <inheritdoc/>
        public async Task<bool> IzmeniStanicu(int id, string naziv, string region)
        {
            Stanica s = new(id, naziv, region);
            if (!s.JeValidan())
                return false;
            return await repo.IzmeniStanicu(s);
        }
    }
}
