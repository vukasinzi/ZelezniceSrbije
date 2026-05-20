namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja osnovnog korisnika sistema.
    /// Korisnik ima ime, prezime, email i lozinku.
    /// </summary>
    public class Korisnik
    {
        /// <summary>
        /// Jedinstveni identifikator korisnika.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ime korisnika.
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime korisnika.
        /// </summary>
        public string Prezime { get; set; }

        /// <summary>
        /// Email adresa korisnika.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lozinka korisnika.
        /// </summary>
        public string Lozinka { get; set; }

        /// <summary>
        /// Kreira novog korisnika.
        /// </summary>
        /// <param name="ime">Ime korisnika.</param>
        /// <param name="prezime">Prezime korisnika.</param>
        /// <param name="email">Email adresa korisnika.</param>
        /// <param name="lozinka">Lozinka korisnika.</param>
        public Korisnik(string ime, string prezime, string email, string lozinka)
        {
            Ime = ime?.Trim();
            Prezime = prezime?.Trim();
            Email = email?.Trim();
            Lozinka = lozinka?.Trim();
        }

        /// <summary>
        /// Proverava da li su podaci korisnika validni.
        /// </summary>
        /// <returns>
        /// True ako su podaci validni, false ako nisu.
        /// </returns>
        public virtual bool JeValidan()
        {
            if (string.IsNullOrWhiteSpace(Ime) || Ime.Length > 20)
                return false;

            if (string.IsNullOrWhiteSpace(Prezime) || Prezime.Length > 20)
                return false;

            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@") || Email.Length > 150)
                return false;

            if (string.IsNullOrWhiteSpace(Lozinka) || Lozinka.Length < 6)
                return false;

            return true;
        }
    }
}