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
       public async Task<List<Raspored>> PretraziAsync(string polaziste, string odrediste, DateTime datum){
            List<Raspored> lista = new();
            if (polaziste?.Length == 0 || odrediste?.Length == 0 )
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
