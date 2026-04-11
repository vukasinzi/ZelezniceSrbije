using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Services;

public class KartaService : IKartaService
{
    private IKartaRepository repo;

    public KartaService(IKartaRepository repo)
    {
        this.repo = repo;
    }
    
    public async Task<Karta> Kupi(int putnik_id,int raspored_id, int polaziste_id, int odrediste_id)
    {
        var podaci = await repo.ProveriKartu(putnik_id, raspored_id, polaziste_id, odrediste_id);
        if (!podaci)
            return null;
        return await repo.KupiKartu(putnik_id,raspored_id, polaziste_id,odrediste_id);
    
    }

    public async Task<KartaDTO> VratiPodatke(int karta_id,int putnik_id)
    {
        
        return await repo.VratiKartu(karta_id,putnik_id);

    }

    public async Task<List<KartaDTO>> VratiPodatke(int putnik_id)
    {
        return await repo.VratiKarte(putnik_id);
    
    }
}