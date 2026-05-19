namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja podatke o liniji, njenim stanicama i vremenu od polaska.
    /// </summary>
    public class LinijaDTO
    {
        /// <summary>
        /// Linija na koju se podaci odnose.
        /// </summary>
        public Linija linija { get; set; }

        /// <summary>
        /// Lista stanica koje pripadaju liniji.
        /// </summary>
        public List<Stanica> stanice { get; set; }

        /// <summary>
        /// Lista vremena od polaska do svake stanice.
        /// </summary>
        public List<int> vreme_od_polaska { get; set; }
    }
}