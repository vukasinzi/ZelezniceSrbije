using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Data;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Repositories
{
    /// <inheritdoc/>
    public class VozRepository : IVozRepository
    {
        /// <summary>
        /// Kontekst baze podataka aplikacije.
        /// </summary>
        private readonly VozAppContext db;

        /// <summary>
        /// Kreira novi repozitorijum za vozove.
        /// </summary>
        /// <param name="db">Kontekst baze podataka aplikacije.</param>
        public VozRepository(VozAppContext db)
        {
            this.db = db;
        }

        /// <inheritdoc/>
        public async Task DodajTipVoza(TipVoza tipVoza)
        {
            await db.TipVoza.AddAsync(tipVoza);
            await db.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DodajVoz(Voz voz)
        {
            await db.Voz.AddAsync(voz);
            await db.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<List<TipVoza>> UcitajSveTipoveVoza()
        {
            return await db.TipVoza.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Voz>> UcitajSveVozove()
        {
            return await db.Voz.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task UkloniTipVoza(int id)
        {
            var tip = await db.TipVoza.FindAsync(id);
            db.TipVoza.Remove(tip);
            await db.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UkloniVoz(int id)
        {
            var voz = await db.Voz.FindAsync(id);
            db.Voz.Remove(voz);
            await db.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task IzmeniTipVoza(TipVoza tipVoza)
        {
            var tip = await db.TipVoza.FindAsync(tipVoza.Id);
            tip.Naziv = tipVoza.Naziv;
            tip.Opis = tipVoza.Opis;
            await db.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task IzmeniVoz(Voz voz)
        {
            var postojeceVozilo = await db.Voz.FindAsync(voz.Id);
            postojeceVozilo.Naziv = voz.Naziv;
            postojeceVozilo.Aktivan = voz.Aktivan;
            postojeceVozilo.Serijski_broj = voz.Serijski_broj;
            postojeceVozilo.Tip_voza_id = voz.Tip_voza_id;
            await db.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> PostojiTipVoza(int id)
        {
            var tip_voza = await db.TipVoza.FindAsync(id);
            if (tip_voza != null)
                return true;
            return false;
        }

        /// <inheritdoc/>
        public async Task<bool> PostojiVoz(int id)
        {
            var voz = await db.Voz.FindAsync(id);
            if (voz != null)
                return true;
            return false;
        }
    }
}