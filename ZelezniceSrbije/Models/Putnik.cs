using System.Linq;

namespace ZelezniceSrbije.Models
{
    public class Putnik : Korisnik
    {
        public string Broj_telefona { get; set; }

        public Putnik(string ime, string prezime, string email, string broj_telefona, string lozinka)
            : base(ime, prezime, email, lozinka)
        {
            Broj_telefona = broj_telefona?.Trim();
        }

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