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


        public async Task<List<Stanica>> UcitajStaniceAsync()
        {
            List<Stanica> lista = await repo.UcitajStaniceAsync();
            return lista;

        }
    }
}
