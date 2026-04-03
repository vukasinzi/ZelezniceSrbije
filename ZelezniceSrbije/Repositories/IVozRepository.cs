using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public interface IVozRepository
    {
        Task DodajTipVoza(TipVoza tipVoza);
        Task DodajVoz(Voz voz);
        Task IzmeniTipVoza(TipVoza tipVoza);
        Task IzmeniVoz(Voz voz);
        Task<List<TipVoza>> UcitajSveTipoveVoza();
        Task<List<Voz>> UcitajSveVozove();
        Task UkloniTipVoza(int id);
        Task UkloniVoz(int id);
    }
}
