using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    public class VozRepository : IVozRepository
    {
        private readonly VozAppContext db;

        public VozRepository(VozAppContext db)
        {
            this.db = db;
        }

        public async Task DodajTipVoza(TipVoza tipVoza)
        {
            await db.TipVoza.AddAsync(tipVoza);
            await db.SaveChangesAsync();
        }

        public async Task DodajVoz(Voz voz)
        {
            await db.Voz.AddAsync(voz);
            await db.SaveChangesAsync();
        }

        public async Task<List<TipVoza>> UcitajSveTipoveVoza()
        {
            return await db.TipVoza.ToListAsync();
        }

        public async Task<List<Voz>> UcitajSveVozove()
        {
            return await db.Voz.ToListAsync();
        }

        public async Task UkloniTipVoza(int id)
        {
            var tip = await db.TipVoza.FindAsync(id);
            if (tip == null)
                return;

            db.TipVoza.Remove(tip);
            await db.SaveChangesAsync();
        }

        public async Task UkloniVoz(int id)
        {
            var voz = await db.Voz.FindAsync(id);
            if (voz == null)
                return;

            db.Voz.Remove(voz);
            await db.SaveChangesAsync();
        }

        public async Task IzmeniTipVoza(TipVoza tipVoza)
        {
            var tip = await db.TipVoza.FindAsync(tipVoza.Id);
            if (tip == null)
                return;

            tip.Naziv = tipVoza.Naziv;
            tip.Opis = tipVoza.Opis;
            await db.SaveChangesAsync();
        }

        public async Task IzmeniVoz(Voz voz)
        {
            var postojeceVozilo = await db.Voz.FindAsync(voz.Id);
            var tip = await db.TipVoza.FindAsync(voz.Tip_voza_id);
            if (postojeceVozilo == null || tip == null)
                return;

            postojeceVozilo.Naziv = voz.Naziv;
            postojeceVozilo.Aktivan = voz.Aktivan;
            postojeceVozilo.Serijski_broj = voz.Serijski_broj;
            postojeceVozilo.Tip_voza_id = voz.Tip_voza_id;

            await db.SaveChangesAsync();
        }
    }
}
