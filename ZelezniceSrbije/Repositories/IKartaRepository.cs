using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories;

public interface IKartaRepository
{
    Task<bool> ProveriKartu(int putnik_id, int raspored_id, int polaziste_id, int odrediste_id);

    Task<Karta> KupiKartu(int putnik_id, int raspored_id, int polaziste_id, int odrediste_id);
    Task<KartaDTO> VratiKartu(int karta_id, int putnik_id);
    Task<List<KartaDTO>> VratiKarte(int putnik_id);
}