namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja šablon rasporeda vožnje.
    /// Šablon čuva liniju, voz, vreme polaska u toku dana i status aktivnosti.
    /// </summary>
    public class RasporedSablon
    {
        /// <summary>
        /// Jedinstveni identifikator šablona rasporeda.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifikator linije.
        /// </summary>
        public int Linija_id { get; set; }

        /// <summary>
        /// Identifikator voza.
        /// </summary>
        public int Voz_id { get; set; }

        /// <summary>
        /// Vreme polaska u toku dana.
        /// </summary>
        public TimeSpan Vreme_polaska_time { get; set; }

        /// <summary>
        /// Označava da li se šablon koristi za generisanje rasporeda.
        /// </summary>
        public bool Aktivan { get; set; }
    }
}