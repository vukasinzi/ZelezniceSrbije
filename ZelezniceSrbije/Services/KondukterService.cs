using System.Runtime.InteropServices.JavaScript;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Services;

public class KondukterService : IKondukterService
{
    private IKondukterRepository repo;
    public KondukterService(IKondukterRepository repo)
    {
        this.repo = repo;
    }
    public async Task<RasporedDTO?> VratiRaspored(int raspored_id)
    {
        if (raspored_id <= 0)
            return null;
        return await repo.VratiRaspored(raspored_id);
        
    }

    public async Task<List<RasporedDTO>?> VratiRasporedeZaDanas()
    {
        DateTime dtm = DateTime.Now;
        return await repo.VratiRasporedeZaDanas();

    }

    public async Task<bool> OcitajKartu(Guid token, int raspored_id)
    {
        return await repo.OcitajKartu(token, raspored_id);
    }
}