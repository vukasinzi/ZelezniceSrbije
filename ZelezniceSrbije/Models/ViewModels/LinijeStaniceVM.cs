namespace ZelezniceSrbije.Models.ViewModels
{
    public class LinijeStaniceVM
    {
        public List<LinijaDTO> Linije { get; set; } = new List<LinijaDTO>();
        public List<Stanica> Stanice { get; set; } = new List<Stanica>();
    }
}
