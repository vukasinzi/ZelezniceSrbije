using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Services;

public interface IKartaService
{
    Task<Karta> Kupi(int putnik_id,int raspored_id, int polaziste_id, int odrediste_id);
    Task<KartaDTO> VratiPodatke(int karta_id,int putnik_id);
    Task<List<KartaDTO>> VratiPodatke(int putnik_id);
}