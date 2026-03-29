using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services
{
    public interface IRasporedService
    {
        Task<List<Raspored>> PretraziAsync(string polaziste,string odrediste,DateTime datum);
        Task<List<Stanica>> UcitajStaniceAsync();
    }
}
