using System.Runtime.InteropServices.JavaScript;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Services;

/// <inheritdoc/>
public class KondukterService : IKondukterService
{
    /// <summary>
    /// Repozitorijum za rad sa kondukterskim funkcionalnostima.
    /// </summary>
    private IKondukterRepository repo;

    /// <summary>
    /// Kreira novi servis za kondukterske funkcionalnosti.
    /// </summary>
    /// <param name="repo">Repozitorijum za rad sa kondukterskim funkcionalnostima.</param>
    public KondukterService(IKondukterRepository repo)
    {
        this.repo = repo;
    }

    /// <inheritdoc/>
    public async Task<RasporedDTO?> VratiRaspored(int raspored_id)
    {
        if (raspored_id <= 0)
            return null;
        return await repo.VratiRaspored(raspored_id);
        
    }

    /// <inheritdoc/>
    public async Task<List<RasporedDTO>?> VratiRasporedeZaDanas()
    {
        DateTime dtm = DateTime.Now;
        return await repo.VratiRasporedeZaDanas();

    }

    /// <inheritdoc/>
    public async Task<bool> OcitajKartu(Guid token, int raspored_id)
    {
        return await repo.OcitajKartu(token, raspored_id);
    }
}