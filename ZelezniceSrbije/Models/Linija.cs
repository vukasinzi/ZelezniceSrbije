namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja liniju voza.
    /// Linija ima naziv i cenu po minutu vožnje.
    /// </summary>
    public class Linija
    {
        /// <summary>
        /// Kreira novu liniju.
        /// </summary>
        /// <param name="naziv">Naziv linije.</param>
        /// <param name="cena_po_minutu">Cena vožnje po minutu.</param>
        public Linija(string naziv, int cena_po_minutu)
        {
            Naziv = naziv.Trim();
            Cena_po_minutu = cena_po_minutu;
        }

        /// <summary>
        /// Kreira novu liniju sa zadatim identifikatorom.
        /// </summary>
        /// <param name="id">Identifikator linije.</param>
        /// <param name="naziv">Naziv linije.</param>
        /// <param name="cena_po_minutu">Cena vožnje po minutu.</param>
        public Linija(int id, string naziv, int cena_po_minutu)
        {
            Id = id;
            Naziv = naziv?.Trim();
            Cena_po_minutu = cena_po_minutu;
        }

        /// <summary>
        /// Jedinstveni identifikator linije.
        /// Dozvoljena vrednost: 0 za novu liniju ili pozitivan broj za postojeću liniju.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Naziv linije.
        /// Dozvoljena vrednost: ne sme biti prazan i može imati najviše 30 karaktera.
        /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Cena vožnje po jednom minutu.
        /// Dozvoljena vrednost: mora biti veća od 0.
        /// </summary>
        public int Cena_po_minutu { get; set; }

        /// <summary>
        /// Proverava da li je linija validna.
        /// </summary>
        /// <returns>
        /// True ako je linija validna, false ako nije.
        /// </returns>
        internal bool JeValidan()
        {
            if (string.IsNullOrWhiteSpace(Naziv) || Naziv.Length > 30)
                return false;

            if (Cena_po_minutu <= 0)
                return false;

            return true;
        }
    }
}