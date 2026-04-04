namespace ZelezniceSrbije.Models
{
    public class Kondukter : Korisnik
    {
        public string Broj_legitimacije { get; set; }

        public Kondukter(string ime, string prezime, string email, string lozinka, string broj_legitimacije)
            : base(ime, prezime, email, lozinka)
        {
            Broj_legitimacije = broj_legitimacije?.Trim();
        }

        public override bool JeValidan()
        {
            if (!base.JeValidan())
                return false;

            if (string.IsNullOrWhiteSpace(Broj_legitimacije))
                return false;

            return true;
        }
    }
}