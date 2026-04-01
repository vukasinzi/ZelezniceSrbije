namespace ZelezniceSrbije.Models
{
    public class Stanica
    {
        public Stanica(int id, string naziv, string opstina)
        {
            Id = id;
            Naziv = naziv;
            Opstina = opstina;
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opstina { get; set; }

    }
}
