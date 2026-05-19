namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja podatke o rasporedu koji se prikazuju korisniku.
    /// </summary>
    public class RasporedDTO
    {
        /// <summary>
        /// Jedinstveni identifikator rasporeda.
        /// </summary>
        public int Id { get; set; }   

        /// <summary>
        /// Naziv linije.
        /// </summary>
        public string Linija { get; set; }

        /// <summary>
        /// Tip voza.
        /// </summary>
        public string TipVoza { get; set; }

        /// <summary>
        /// Vreme polaska sa polazne stanice.
        /// </summary>
        public DateTime PolazakSaPol { get; set; }

        /// <summary>
        /// Vreme dolaska na odredišnu stanicu.
        /// </summary>
        public DateTime DolazakNaOdr { get; set; }
    }
}