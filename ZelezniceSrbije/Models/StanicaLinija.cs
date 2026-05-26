namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja vezu između stanice i linije.
    /// Čuva redosled stanice na liniji i vreme od polaska.
    /// </summary>
    public class StanicaLinija
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
        /// Dozvoljena vrednost: 0 za novu vezu ili pozitivan broj za postojeću vezu.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Vreme od polaska do stanice.
        /// Dozvoljena vrednost: mora biti veće ili jednako 0.
        /// </summary>
        public int Vreme_od_polaska { get; set; }

        /// <summary>
        /// Redosled stanice na liniji.
        /// Dozvoljena vrednost: mora biti veći od 0.
        /// </summary>
        public int Redosled { get; set; }

        /// <summary>
        /// Identifikator stanice.
        /// Dozvoljena vrednost: mora biti veći od 0.
        /// </summary>
        public int Stanica_id { get; set; }

        /// <summary>
        /// Identifikator linije.
        /// Dozvoljena vrednost: mora biti veći od 0.
        /// </summary>
        public int Linija_id { get; set; }

        /// <summary>
        /// Stanica koja pripada ovoj vezi.
        /// Dozvoljena vrednost: stanica povezana preko identifikatora Stanica_id.
        /// </summary>
        public Stanica Stanica { get; set; }

        /// <summary>
        /// Linija koja pripada ovoj vezi.
        /// Dozvoljena vrednost: linija povezana preko identifikatora Linija_id.
        /// </summary>
        public Linija Linija { get; set; }

        /// <summary>
        /// Proverava da li je veza između stanice i linije validna.
        /// </summary>
        /// <returns>
        /// True ako je veza validna, false ako nije.
        /// </returns>
        public bool JeValidan()
        {
            if (Vreme_od_polaska < 0 || Redosled <= 0)
                return false;

            if (Stanica_id <= 0 || Linija_id <= 0)
                return false;

            return true;
        }
    }
}