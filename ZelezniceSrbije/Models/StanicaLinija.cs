namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja vezu između stanice i linije.
    /// Čuva redosled stanice na liniji i vreme od polaska.
    /// </summary>
    public class StanicaLinija//StanicaLinija
    {
        /// <summary>
        /// Kreira novu vezu između stanice i linije.
        /// </summary>
        /// <param name="vreme_od_polaska">Vreme od polaska do stanice.</param>
        /// <param name="redosled">Redosled stanice na liniji.</param>
        /// <param name="stanica_id">Identifikator stanice.</param>
        /// <param name="linija_id">Identifikator linije.</param>
        public StanicaLinija(int vreme_od_polaska, int redosled, int stanica_id, int linija_id)
        {
            Vreme_od_polaska = vreme_od_polaska;
            Redosled = redosled;
            Stanica_id = stanica_id;
            Linija_id = linija_id;
        }

        /// <summary>
        /// Jedinstveni identifikator veze između stanice i linije.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Vreme od polaska do stanice.
        /// </summary>
        public int Vreme_od_polaska { get; set; }

        /// <summary>
        /// Redosled stanice na liniji.
        /// </summary>
        public int Redosled { get; set; }

        /// <summary>
        /// Identifikator stanice.
        /// </summary>
        public int Stanica_id { get; set; }

        /// <summary>
        /// Identifikator linije.
        /// </summary>
        public int Linija_id { get; set; }

        /// <summary>
        /// Stanica koja pripada ovoj vezi.
        /// </summary>
        public Stanica Stanica { get; set; }

        /// <summary>
        /// Linija koja pripada ovoj vezi.
        /// </summary>
        public Linija Linija { get; set; }
    }
}