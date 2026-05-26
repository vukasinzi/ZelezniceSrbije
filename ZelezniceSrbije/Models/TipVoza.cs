namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja tip voza.
    /// Tip voza ima naziv i opis.
    /// </summary>
    public class TipVoza
    {
        /// <summary>
        /// Kreira novi tip voza sa zadatim identifikatorom.
        /// </summary>
        /// <param name="id">Identifikator tipa voza.</param>
        /// <param name="naziv">Naziv tipa voza.</param>
        /// <param name="opis">Opis tipa voza.</param>
        public TipVoza(int id, string naziv, string opis)
        {
            Id = id;
            Naziv = naziv?.Trim();
            Opis = opis?.Trim();
        }

        /// <summary>
        /// Kreira novi tip voza.
        /// </summary>
        /// <param name="naziv">Naziv tipa voza.</param>
        /// <param name="opis">Opis tipa voza.</param>
        public TipVoza(string naziv, string opis)
        {
            Naziv = naziv?.Trim();
            Opis = opis?.Trim();
        }

        /// <summary>
        /// Jedinstveni identifikator tipa voza.
        /// Dozvoljena vrednost: 0 za novi tip voza ili pozitivan broj za postojeći tip voza.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Naziv tipa voza.
        /// Dozvoljena vrednost: ne sme biti prazan i može imati najviše 100 karaktera.
        /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Opis tipa voza.
        /// Dozvoljena vrednost: ne sme biti prazan i može imati najviše 500 karaktera.
        /// </summary>
        public string Opis { get; set; }

        /// <summary>
        /// Proverava da li je tip voza validan.
        /// </summary>
        /// <returns>
        /// True ako je tip voza validan, false ako nije.
        /// </returns>
        public bool JeValidan()
        {
            if (string.IsNullOrWhiteSpace(Naziv) || Naziv.Length > 100)
                return false;

            if (string.IsNullOrWhiteSpace(Opis) || Opis.Length > 500)
                return false;

            return true;
        }
    }
}