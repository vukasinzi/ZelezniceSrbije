using System.ComponentModel.DataAnnotations.Schema;

namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja voz u sistemu.
    /// Voz ima naziv, serijski broj, status aktivnosti i tip voza.
    /// </summary>
    public class Voz
    {
        /// <summary>
        /// Kreira novi voz sa zadatim identifikatorom.
        /// </summary>
        /// <param name="id">Identifikator voza.</param>
        /// <param name="naziv">Naziv voza.</param>
        /// <param name="serijski_broj">Serijski broj voza.</param>
        /// <param name="aktivan">Označava da li je voz aktivan.</param>
        /// <param name="tip_voza_id">Identifikator tipa voza.</param>
        public Voz(int id, string naziv, string serijski_broj, bool aktivan, int tip_voza_id)
        {
            Id = id;
            Naziv = naziv?.Trim();
            Serijski_broj = serijski_broj?.Trim();
            Aktivan = aktivan;
            Tip_voza_id = tip_voza_id;
        }

        /// <summary>
        /// Kreira novi voz.
        /// </summary>
        /// <param name="naziv">Naziv voza.</param>
        /// <param name="serijski_broj">Serijski broj voza.</param>
        /// <param name="aktivan">Označava da li je voz aktivan.</param>
        /// <param name="tip_voza_id">Identifikator tipa voza.</param>
        public Voz(string naziv, string serijski_broj, bool aktivan, int tip_voza_id)
        {
            Naziv = naziv?.Trim();
            Serijski_broj = serijski_broj?.Trim();
            Aktivan = aktivan;
            Tip_voza_id = tip_voza_id;
        }

        /// <summary>
        /// Jedinstveni identifikator voza.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Naziv voza.
        /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Serijski broj voza.
        /// </summary>
        public string Serijski_broj { get; set; }

        /// <summary>
        /// Označava da li je voz aktivan.
        /// </summary>
        public bool Aktivan { get; set; }

        /// <summary>
        /// Identifikator tipa voza.
        /// </summary>
        public int Tip_voza_id { get; set; }

        /// <summary>
        /// Tip voza kojem voz pripada.
        /// </summary>
        [ForeignKey(nameof(Tip_voza_id))]
        public TipVoza TipVoza { get; set; }

        /// <summary>
        /// Proverava da li je voz validan.
        /// </summary>
        /// <returns>
        /// True ako je voz validan, false ako nije.
        /// </returns>
        public bool JeValidan()
        {
            if (string.IsNullOrWhiteSpace(Naziv) || Naziv.Length > 100)
                return false;

            if (string.IsNullOrWhiteSpace(Serijski_broj) || Serijski_broj.Length > 50)
                return false;

            if (Tip_voza_id <= 0)
                return false;

            return true;
        }
    }
}