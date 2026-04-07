using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Services
{
    public class VozService : IVozService
    {
        private readonly IVozRepository repo;

        public VozService(IVozRepository repo)
        {
            this.repo = repo;
        }

        public async Task<bool> DodajTipVoza(string naziv, string opis)
        {
            TipVoza tv = new(naziv, opis);
            if (!tv.JeValidan())
                return false;

            await repo.DodajTipVoza(tv);
            return true;
        }

        public async Task<bool> DodajVoz(string naziv, string serijski_broj, int tip_voza_id, bool aktivan)
        {
            Voz voz = new(naziv, serijski_broj, aktivan, tip_voza_id);
            if (!voz.JeValidan())
                return false;
            var postoji = await repo.PostojiTipVoza(tip_voza_id);

            if (!postoji)
                return false;

            await repo.DodajVoz(voz);
            return true;
        }

        public async Task<List<Voz>> UcitajSveVozove()
        {
            return await repo.UcitajSveVozove();
        }

        public async Task<List<TipVoza>> UcitajSveTipoveVoza()
        {
            return await repo.UcitajSveTipoveVoza();
        }

        public async Task<bool> UkloniTipVoza(int id)
        {
            if (id <= 0)
                return false;
            var postoji = await repo.PostojiTipVoza(id);

            if (!postoji)
                return false;
            await repo.UkloniTipVoza(id);
            return true;
        }

        public async Task<bool> UkloniVoz(int id)
        {
            if (id <= 0)
                return false;
            var postoji = await repo.PostojiVoz(id);
            if (!postoji)
                return false;
            await repo.UkloniVoz(id);
            return true;
        }

        public async Task<bool> IzmeniVoz(int id, string naziv, string serijski_broj, bool aktivan, int tip_voza_id)
        {
            Voz voz = new(id, naziv, serijski_broj, aktivan, tip_voza_id);
            if (!voz.JeValidan())
                return false;
            var postoji = await repo.PostojiVoz(id);
            var postoji2 = await repo.PostojiTipVoza(tip_voza_id);
            if (!postoji || !postoji2)
                return false;
            await repo.IzmeniVoz(voz);
            return true;
        }

        public async Task<bool> IzmeniTipVoza(int id, string naziv, string opis)
        {
            TipVoza tipVoza = new(id, naziv, opis);
            if (!tipVoza.JeValidan())
                return false;
            var postoji = await repo.PostojiTipVoza(id);
            if (!postoji)
                return false;
            await repo.IzmeniTipVoza(tipVoza);
            return true;
        }
    }
}
