namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja podatke o listi stanica.
    /// </summary>
    public class StaniceDTO
    {
        /// <summary>
        /// Lista stanica.
        /// </summary>
        public List<Stanica> Stanice { get; set; } = new();
    }
}