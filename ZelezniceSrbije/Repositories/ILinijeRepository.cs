using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public interface ILinijeRepository
    {
        Task DodajLiniju(Linija l);
        Task DodajStajalistaZaLiniju(List<StanicaLinija> fejk);
        Task DodajStanicu(Stanica s);
        Task UkloniLiniju(int id);
        Task UkloniStanicu(int id);
        Task<object> ProveriLiniju(string naziv);
        Task<object> ProveriStanicu(string naziv);
        Task<object> ProveriLiniju(int id);
        Task<object> ProveriStanicu(int id);
        Task<List<LinijaDTO>> UcitajSveLinije();
        Task<List<Stanica>>UcitajSveStanice(string region);
        Task IzmeniLiniju(Linija l, List<StanicaLinija> stajalista);
        Task IzmeniStanicu(Stanica s);
    }
}
