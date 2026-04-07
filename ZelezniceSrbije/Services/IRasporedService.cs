using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services
{
    public interface IRasporedService
    {
        Task<bool> DodajRaspored(int linija_id, int voz_id, DateTime vreme_polaska);
        Task<bool> IzmeniRaspored(int id,int linija_id, int voz_id, DateTime vreme_polaska);
        Task<List<RasporedDTO>> PretraziAsync(string polaziste,string odrediste,DateTime datum);
        Task<List<Raspored>> UcitajRasporede(DateTime? datum);
        Task<List<Stanica>> UcitajStaniceAsync();
        Task<bool> UkloniRaspored(int id);
    }
}
