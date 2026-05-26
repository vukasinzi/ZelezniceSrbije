using System.Linq;

namespace ZelezniceSrbije.Models
{
    /// <summary>
    /// Predstavlja putnika u sistemu.
    /// Putnik nasleđuje osnovne podatke korisnika i ima broj telefona.
    /// </summary>
    public class Putnik : Korisnik
    {
        /// <summary>
        /// Broj telefona putnika.
        /// Dozvoljena vrednost: ne sme biti prazan, sme sadržati samo cifre i može imati najviše 20 karaktera.
        /// </summary>
        public string Broj_telefona { get; set; }

        /// <summary>
        /// Kreira novog putnika.
        /// </summary>
        /// <param name="ime">Ime putnika.</param>
        /// <param name="prezime">Prezime putnika.</param>
        /// <param name="email">Email adresa putnika.</param>
        /// <param name="broj_telefona">Broj telefona putnika.</param>
        /// <param name="lozinka">Lozinka putnika.</param>
        public Putnik(string ime, string prezime, string email, string broj_telefona, string lozinka)
            : base(ime, prezime, email, lozinka)
        {
            Broj_telefona = broj_telefona?.Trim();
        }

        /// <summary>
        /// Proverava da li je putnik validan.
        /// </summary>
        /// <returns>
        /// True ako su podaci validni, false ako nisu.
        /// </returns>
        public override bool JeValidan()
        {
            if (!base.JeValidan())
                return false;

            if (string.IsNullOrWhiteSpace(Broj_telefona) || !Broj_telefona.All(char.IsDigit) || Broj_telefona.Length > 20)
                return false;

            return true;
        }
    }
}