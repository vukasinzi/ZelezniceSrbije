namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja konduktera u sistemu.
    /// Kondukter može da očitava i proverava karte.
    /// </summary>
    public class Kondukter : Korisnik
    {
        /// <summary>
        /// Broj legitimacije konduktera.
        /// </summary>
        public string Broj_legitimacije { get; set; }

        /// <summary>
        /// Kreira novog konduktera.
        /// </summary>
        /// <param name="ime">Ime konduktera.</param>
        /// <param name="prezime">Prezime konduktera.</param>
        /// <param name="email">Email konduktera.</param>
        /// <param name="lozinka">Lozinka konduktera.</param>
        /// <param name="broj_legitimacije">Broj legitimacije konduktera.</param>
        public Kondukter(string ime, string prezime, string email, string lozinka, string broj_legitimacije)
            : base(ime, prezime, email, lozinka)
        {
            Broj_legitimacije = broj_legitimacije?.Trim();
        }

        /// <summary>
        /// Proverava da li je kondukter validan.
        /// </summary>
        /// <returns>
        /// True ako su podaci validni, false ako nisu.
        /// </returns>
        public override bool JeValidan()
        {
            if (!base.JeValidan())
                return false;

            if (string.IsNullOrWhiteSpace(Broj_legitimacije) || Broj_legitimacije.Length > 50)
                return false;

            return true;
        }
    }
}