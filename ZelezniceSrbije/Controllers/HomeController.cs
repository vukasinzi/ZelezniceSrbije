using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Models.ViewModels;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers;

/// <summary>
/// Kontroler za početnu stranicu aplikacije.
/// Omogućava prikaz početne stranice, pretragu rasporeda i prikaz grešaka.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Servis za rad sa rasporedima vožnje.
    /// </summary>
    private IRasporedService servis;

    /// <summary>
    /// Kreira novi kontroler za početnu stranicu.
    /// </summary>
    /// <param name="servis">Servis za rad sa rasporedima.</param>
    public HomeController(IRasporedService servis)
    {
        this.servis = servis;
    }

    /// <summary>
    /// Pretražuje rasporede prema polazištu, odredištu i datumu.
    /// </summary>
    /// <param name="polaziste">Polazna stanica.</param>
    /// <param name="odrediste">Odredišna stanica.</param>
    /// <param name="datum">Datum putovanja.</param>
    /// <returns>
    /// View sa rezultatima pretrage ili porukom o grešci ako je datum u prošlosti.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Pretrazi(string polaziste, string odrediste, DateTime datum)
    {
        if (datum.Date < DateTime.Today)
        {
            ModelState.AddModelError("datum", "Datum ne može biti u prošlosti.");
            var vmErr = new HomeIndexVM
            {
                Stanice = await servis.UcitajStaniceAsync(),
                Rasporedi = new List<RasporedDTO>()
            };
            return View("~/Views/Home/Index.cshtml", vmErr);
        }

        ViewData["Datum"] = datum;
        ViewData["Polaziste"] = polaziste;
        ViewData["Odrediste"] = odrediste;

        var vm = new HomeIndexVM
        {
            Stanice = await servis.UcitajStaniceAsync(),
            Rasporedi = await servis.PretraziAsync(polaziste, odrediste, datum)
        };

        return View("~/Views/Home/Index.cshtml", vm);
    }

    /// <summary>
    /// Prikazuje početnu stranicu aplikacije.
    /// </summary>
    /// <returns>
    /// View sa listom stanica i praznom listom rasporeda.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var vm = new HomeIndexVM
        {
            Stanice = await servis.UcitajStaniceAsync(),
            Rasporedi = new List<RasporedDTO>()
        };
        return View(vm);
    }

    /// <summary>
    /// Prikazuje stranicu sa informacijama o privatnosti.
    /// </summary>
    /// <returns>
    /// View za stranicu privatnosti.
    /// </returns>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Prikazuje stranicu sa informacijama o grešci.
    /// </summary>
    /// <returns>
    /// View sa podacima o grešci.
    /// </returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}