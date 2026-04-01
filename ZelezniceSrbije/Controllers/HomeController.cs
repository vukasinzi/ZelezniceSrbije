using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Models.ViewModels;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers;

public class HomeController : Controller
{
    private IRasporedService servis;
    public HomeController(IRasporedService servis)
    {
        this.servis = servis;
    }
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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
