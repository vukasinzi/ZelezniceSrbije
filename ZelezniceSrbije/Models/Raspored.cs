using ZelezniceSrbije.Models;

/// <summary>
/// Predstavlja raspored polaska voza.
/// Raspored sadrži vreme polaska, liniju i voz.
/// </summary>
public class Raspored
{
    /// <summary>
    /// Jedinstveni identifikator rasporeda.
    /// Dozvoljena vrednost: 0 za novi raspored ili pozitivan broj za postojeći raspored.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Vreme polaska voza.
    /// Dozvoljena vrednost: mora biti uneto vreme polaska.
    /// </summary>
    public DateTime Vreme_polaska { get; set; }

    /// <summary>
    /// Identifikator linije.
    /// Dozvoljena vrednost: mora biti veći od 0.
    /// </summary>
    public int Linija_id { get; set; }

    /// <summary>
    /// Identifikator voza.
    /// Dozvoljena vrednost: mora biti veći od 0.
    /// </summary>
    public int Voz_id { get; set; }

    /// <summary>
    /// Linija koja pripada rasporedu.
    /// Dozvoljena vrednost: linija povezana preko identifikatora Linija_id.
    /// </summary>
    public Linija? Linija { get; set; }

    /// <summary>
    /// Voz koji pripada rasporedu.
    /// Dozvoljena vrednost: voz povezan preko identifikatora Voz_id.
    /// </summary>
    public Voz? Voz { get; set; }

    /// <summary>
    /// Kreira prazan raspored.
    /// </summary>
    public Raspored() { }

    /// <summary>
    /// Kreira novi raspored.
    /// </summary>
    /// <param name="vreme_polaska">Vreme polaska voza.</param>
    /// <param name="linija_id">Identifikator linije.</param>
    /// <param name="voz_id">Identifikator voza.</param>
    public Raspored(DateTime vreme_polaska, int linija_id, int voz_id)
    {
        Vreme_polaska = vreme_polaska;
        Linija_id = linija_id;
        Voz_id = voz_id;
    }

    /// <summary>
    /// Kreira novi raspored sa zadatim identifikatorom.
    /// </summary>
    /// <param name="id">Identifikator rasporeda.</param>
    /// <param name="vreme_polaska">Vreme polaska voza.</param>
    /// <param name="linija_id">Identifikator linije.</param>
    /// <param name="voz_id">Identifikator voza.</param>
    public Raspored(int id, DateTime vreme_polaska, int linija_id, int voz_id)
    {
        Id = id;
        Vreme_polaska = vreme_polaska;
        Linija_id = linija_id;
        Voz_id = voz_id;
    }

    /// <summary>
    /// Proverava da li je raspored validan.
    /// </summary>
    /// <returns>
    /// True ako je raspored validan, false ako nije.
    /// </returns>
    public bool JeValidan()
    {
        if (Vreme_polaska == default)
            return false;

        if (Linija_id <= 0 || Voz_id <= 0)
            return false;

        return true;
    }
}