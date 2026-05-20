using ZelezniceSrbije.Models;
using ZelezniceSrbije.Models.ViewModels;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Services;

/// <inheritdoc/>
public class KartaService : IKartaService
{
    /// <summary>
    /// Repozitorijum za rad sa kartama.
    /// </summary>
    private IKartaRepository repo;

    /// <summary>
    /// Kreira novi servis za karte.
    /// </summary>
    /// <param name="repo">Repozitorijum za rad sa kartama.</param>
    public KartaService(IKartaRepository repo)
    {
        this.repo = repo;
    }

    /// <inheritdoc/>
    public async Task<Karta> Kupi(int putnik_id, int raspored_id, int polaziste_id, int odrediste_id)
    {
        if (putnik_id <= 0 || raspored_id <= 0 || odrediste_id <= 0)
            return null;

        var podaci = await repo.ProveriKartu(putnik_id, raspored_id, polaziste_id, odrediste_id);
        if (!podaci)
            return null;

        return await repo.KupiKartu(putnik_id, raspored_id, polaziste_id, odrediste_id);
    }

    /// <inheritdoc/>
    public async Task<KartaDTO> VratiPodatke(int karta_id, int putnik_id)
    {
        return await repo.VratiKartu(karta_id, putnik_id);
    }

    /// <inheritdoc/>
    public async Task<List<KartaDTO>> VratiPodatke(int putnik_id)
    {
        return await repo.VratiKarte(putnik_id);
    }
}