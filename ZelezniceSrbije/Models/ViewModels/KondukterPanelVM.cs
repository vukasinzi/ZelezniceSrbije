namespace ZelezniceSrbije.Models.ViewModels;

public class KondukterPanelVM
{
    public AktivnaVoznja? aktivna_voznja { get; set; }
    public RasporedDTO? aktivna_voznja_detalji { get; set; }
    public List<RasporedDTO>? dostupni_rasporedi { get; set; }
}