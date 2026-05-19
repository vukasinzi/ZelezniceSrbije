/// <summary>
/// Predstavlja podatke o karti koji se šalju korisniku.
/// </summary>
/// <param name="karta_id">Identifikator karte.</param>
/// <param name="cena">Cena karte.</param>
/// <param name="putnik">Ime i prezime putnika.</param>
/// <param name="polaziste">Polazna stanica.</param>
/// <param name="odrediste">Odredišna stanica.</param>
/// <param name="linija">Naziv linije.</param>
/// <param name="tip_voza">Tip voza.</param>
/// <param name="trajanje">Trajanje putovanja.</param>
/// <param name="vreme_polaska">Vreme polaska voza.</param>
/// <param name="vreme_dolaska">Vreme dolaska voza.</param>
/// <param name="ocitana">Označava da li je karta očitana.</param>
/// <param name="datum_ocitavanja">Datum kada je karta očitana.</param>
/// <param name="qr_token">QR token za proveru karte.</param>
public record KartaDTO(
    int karta_id,
    decimal cena,
    string putnik,
    string polaziste,
    string odrediste,
    string linija,
    string tip_voza,
    string trajanje,
    DateTime vreme_polaska,
    DateTime vreme_dolaska,
    bool ocitana,
    DateTime? datum_ocitavanja,
    Guid qr_token
);