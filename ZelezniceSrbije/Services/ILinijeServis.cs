using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services
{
    public interface ILinijeServis
    {
        Task<bool> DodajLiniju(string naziv, int cena_po_minutu, List<int> stanicaIds, List<int> redosled, List<int> vreme_od_polaska);
        Task<bool> DodajStanicu(string naziv, string region);
        Task<bool> IzmeniLiniju(int id, string naziv, int cena_po_minutu, List<int> stanicaIds, List<int> redosled, List<int> vreme_od_polaska);
        Task<bool> IzmeniStanicu(int id, string naziv, string region);
        Task<List<LinijaDTO>> UcitajSveLinije();
        Task<List<Stanica>> UcitajSveStanice(string region);
        Task<bool> UkloniLiniju(int id);
        Task<bool> UkloniStanicu(int id);

    }
}
