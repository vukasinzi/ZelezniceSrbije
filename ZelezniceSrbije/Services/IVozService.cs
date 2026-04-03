using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services
{
    public interface IVozService
    {
        Task<bool> DodajTipVoza(string naziv, string opis);
        Task<bool> DodajVoz(string naziv, string serijski_broj, int tip_voza_id, bool aktivan);
        Task<bool> IzmeniTipVoza(int id, string naziv, string opis);
        Task<bool> IzmeniVoz(int id, string naziv, string serijski_broj, bool aktivan, int tip_voza_id);
        Task<List<TipVoza>> UcitajSveTipoveVoza();
        Task<List<Voz>> UcitajSveVozove();
        Task<bool> UkloniTipVoza(int id);
        Task<bool> UkloniVoz(int id);
    }
}
