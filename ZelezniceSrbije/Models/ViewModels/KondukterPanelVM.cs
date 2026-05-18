namespace ZelezniceSrbije.Models.ViewModels;

public class KondukterPanelVM
{
    public RasporedDTO? aktivna_voznja_detalji { get; set; }
    public List<RasporedDTO>? dostupni_rasporedi { get; set; }
}
