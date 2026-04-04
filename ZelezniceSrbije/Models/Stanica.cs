namespace ZelezniceSrbije.Models
{
    public class Stanica
    {
        public Stanica(int id, string naziv, string region)
        {
            Id = id;
            Naziv = naziv?.Trim(); 
            Region = region?.Trim(); 
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Region { get; set; }

    }
}
