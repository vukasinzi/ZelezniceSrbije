using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public interface ILinijeRepository
    {
        Task DodajLiniju(Linija l);
        Task DodajStajalistaZaLiniju(List<StanicaLinija> fejk);
        Task DodajStanicu(Stanica s);
        Task<object> ProveriLiniju(string naziv);
        Task<object> ProveriStanicu(string naziv);
        Task<List<LinijaDTO>> UcitajSveLinije();
        Task<List<Stanica>>UcitajSveStanice(string region);
    }
}
