using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services;

public interface IKondukterService
{
    Task<RasporedDTO?> VratiRaspored(int aktivnaRasporedId);
    Task<List<RasporedDTO>?> VratiRasporedeZaDanas();
    Task<bool> OcitajKartu(Guid token, int aktivnaRasporedId);
}