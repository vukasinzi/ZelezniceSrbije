namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja administratora sistema.
    /// Administrator može da upravlja admin panelom i nasleđuje klasu Korisnik.
    /// </summary>
    public class Administrator : Korisnik
    {
        /// <summary>
        /// Datum zaposlenja administratora.
        /// Dozvoljena vrednost: mora biti unet i ne sme biti datum u budućnosti.
        /// </summary>
        public DateTime? Datum_zaposlenja { get; set; }

        /// <summary>
        /// Kreira novog administratora.
        /// </summary>
        /// <param name="ime">Ime administratora.</param>
        /// <param name="prezime">Prezime administratora.</param>
        /// <param name="email">Email administratora.</param>
        /// <param name="lozinka">Lozinka administratora.</param>
        /// <param name="datum_zaposlenja">Datum zaposlenja administratora.</param>
        public Administrator(string ime, string prezime, string email, string lozinka, DateTime? datum_zaposlenja)
            : base(ime, prezime, email, lozinka)
        {
            Datum_zaposlenja = datum_zaposlenja;
        }

        /// <summary>
        /// Proverava da li je administrator validan.
        /// </summary>
        /// <returns>
        /// True ako su podaci validni, false ako nisu.
        /// </returns>
        public override bool JeValidan()
        {
            if (!base.JeValidan())
                return false;

            if (Datum_zaposlenja == null || Datum_zaposlenja.Value.Date > DateTime.Today)
                return false;

            return true;
        }
    }
}