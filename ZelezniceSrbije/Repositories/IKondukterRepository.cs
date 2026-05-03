using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories;

public interface IKondukterRepository
{
    Task<RasporedDTO?> VratiRaspored(int raspored_id);
    Task<List<RasporedDTO>?> VratiRasporedeZaDanas();
    Task<bool> OcitajKartu(Guid token, int rasporedId);
}