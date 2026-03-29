using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public interface IRasporedRepository
    {
        Task<List<Raspored>> PretraziAsync(string polaziste, string odrediste, DateTime datum);
        Task<List<Stanica>> UcitajStaniceAsync();
    }
}
