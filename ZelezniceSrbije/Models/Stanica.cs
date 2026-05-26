namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja stanicu.
    /// Stanica ima naziv i region kojem pripada.
    /// </summary>
    public class Stanica
    {
        /// <summary>
        /// Kreira novu stanicu sa zadatim identifikatorom.
        /// </summary>
        /// <param name="id">Identifikator stanice.</param>
        /// <param name="naziv">Naziv stanice.</param>
        /// <param name="region">Region kojem stanica pripada.</param>
        public Stanica(int id, string naziv, string region)
        {
            Id = id;
            Naziv = naziv?.Trim();
            Region = region?.Trim();
        }

        /// <summary>
        /// Kreira novu stanicu.
        /// </summary>
        /// <param name="naziv">Naziv stanice.</param>
        /// <param name="region">Region kojem stanica pripada.</param>
        public Stanica(string naziv, string region)
        {
            Naziv = naziv?.Trim();
            Region = region?.Trim();
        }

        /// <summary>
        /// Proverava da li je stanica validna.
        /// </summary>
        /// <returns>
        /// True ako je stanica validna, false ako nije.
        /// </returns>
        public bool JeValidan()
        {
            if (string.IsNullOrWhiteSpace(Naziv) || string.IsNullOrWhiteSpace(Region))
                return false;

            return true;
        }

        /// <summary>
        /// Jedinstveni identifikator stanice.
        /// Dozvoljena vrednost: 0 za novu stanicu ili pozitivan broj za postojeću stanicu.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Naziv stanice.
        /// Dozvoljena vrednost: ne sme biti prazan.
        /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Region kojem stanica pripada.
        /// Dozvoljena vrednost: ne sme biti prazan.
        /// </summary>
        public string Region { get; set; }
    }
}