using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Services
{
    public class RasporedService : IRasporedService
    {

        private IRasporedRepository repo;
        public RasporedService(IRasporedRepository repo)
        {
            this.repo = repo;
        }


        public async Task<List<RasporedDTO>> PretraziAsync(string polaziste, string odrediste, DateTime datum){
            List<RasporedDTO> lista = new();
            if ( polaziste == null || odrediste == null || polaziste?.Length == 0 || odrediste?.Length == 0 )
                return null;
            if (polaziste == odrediste)
                return null;
            if (datum < DateTime.Today)
                return null;

            lista =  await repo.PretraziAsync(polaziste, odrediste, datum);

            return lista;
        }

        public async Task<List<Raspored>> UcitajRasporede(DateTime? datum)
        {
            if (datum == null)
                return new List<Raspored>();
            List<Raspored> rasporedi = await repo.UcitajRasporede(datum);
            if (rasporedi == null || rasporedi.Count == 0)
                return new List<Raspored>();

            return rasporedi;
        }

        public async Task<List<Stanica>> UcitajStaniceAsync()
        {
            List<Stanica> lista = await repo.UcitajStaniceAsync();
            return lista;

        }

        public async Task<bool> UkloniRaspored(int id)
        {
            if (id <= 0)
                return false;
            var provera = await repo.ProveriRaspored(id);
            if (provera == null)
                return false;

            await repo.UkloniRaspored(id);
            return true;
        }
        public async Task<bool> DodajRaspored(int linija_id, int voz_id, DateTime vreme_polaska)
        {
            Raspored r = new(vreme_polaska, linija_id, voz_id);
            if (!r.JeValidan())
                return false;
            await repo.DodajRaspored(r);
            return true;
        }

        public async Task<bool> IzmeniRaspored(int id,int linija_id, int voz_id, DateTime vreme_polaska)
        {
            Raspored r = new(id,vreme_polaska, linija_id, voz_id);
            if (!r.JeValidan())
                return false;
           var provera = await repo.ProveriRaspored(id);
            if (provera == null)
                return false;
            await repo.IzmeniRaspored(r);
            return true;
        }
    }
}
