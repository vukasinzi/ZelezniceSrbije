namespace ZelezniceSrbije.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }

        public Korisnik(string ime, string prezime, string email, string lozinka)
        {
            Ime = ime?.Trim();
            Prezime = prezime?.Trim();
            Email = email?.Trim();
            Lozinka = lozinka?.Trim();
        }

        public virtual bool JeValidan()
        {
            if (string.IsNullOrWhiteSpace(Ime) || Ime.Length > 20 ||
                string.IsNullOrWhiteSpace(Prezime) || Prezime.Length > 20)
                return false;

            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
                return false;

            if (string.IsNullOrWhiteSpace(Lozinka) || Lozinka.Length < 6)
                return false;

            return true;
        }
    }
}