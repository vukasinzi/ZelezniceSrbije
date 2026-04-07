using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public interface IRasporedRepository
    {
        Task DodajRaspored(Raspored r);
        Task IzmeniRaspored(Raspored r);
        Task<List<RasporedDTO>> PretraziAsync(string polaziste, string odrediste, DateTime datum);
        Task<Raspored> ProveriRaspored(int id);
        Task<List<Raspored>> UcitajRasporede(DateTime? datum);
        Task<List<Stanica>> UcitajStaniceAsync();
        Task UkloniRaspored(int id);
    }
}
