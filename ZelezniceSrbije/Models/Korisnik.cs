namespace ZelezniceSrbije.Models
{
    public class Korisnik
    {
        public Korisnik(string ime, string prezime, string email, string lozinka)
        {
            Ime = ime;
            Prezime = prezime;
            Email = email;
            Lozinka = lozinka;
        }

        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
       
        
    }
}
