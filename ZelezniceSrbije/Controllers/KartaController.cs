using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ZelezniceSrbije.Models.ViewModels;
using ZelezniceSrbije.Services;

namespace ZelezniceSrbije.Controllers;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Kontroler za rad sa kartama.
/// Omogućava prikaz, kupovinu i generisanje QR koda za karte.
/// </summary>
[Authorize(Roles = "Administrator,Putnik,Kondukter")]
public class KartaController : Controller
{
   /// <summary>
   /// Servis za rad sa kartama.
   /// </summary>
   private IKartaService servis;

   /// <summary>
   /// Servis za generisanje QR kodova.
   /// </summary>
   private IQrService qr_servis;

   /// <summary>
   /// Kreira novi kontroler za karte.
   /// </summary>
   /// <param name="servis">Servis za rad sa kartama.</param>
   /// <param name="qr_servis">Servis za generisanje QR kodova.</param>
   public KartaController(IKartaService servis, IQrService qr_servis)
   {
      this.servis = servis;
      this.qr_servis = qr_servis;
   }

   /// <summary>
   /// Prikazuje stranicu sa kartama.
   /// </summary>
   /// <returns>
   /// View ako korisnik ima dozvoljenu ulogu, inače preusmerava na početnu stranicu.
   /// </returns>
   [HttpGet]
   public IActionResult Index()
   {
      if (User.IsInRole("Administrator") || User.IsInRole("Kondukter") || User.IsInRole("Putnik"))
         return View();
      return RedirectToAction("Index", "Home");
   }

   /// <summary>
   /// Prikazuje sve karte trenutno prijavljenog putnika.
   /// </summary>
   /// <returns>
   /// View sa listom karata i njihovim QR kodovima.
   /// </returns>
   [HttpGet]
   public async Task<IActionResult> MojeKarte()
   {
      int putnik_id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
      if (putnik_id <= 0)
         return BadRequest("Morate biti prijavljeni");

      var karteDto = await servis.VratiPodatke(putnik_id);
      if (karteDto == null)
         return View("Index", new List<KarteVM>());

      List<KarteVM> lista = new();
      foreach (KartaDTO kd in karteDto)
      {
         var url = Url.Action("Ocitaj", "Kondukter", new { token = kd.qr_token }, Request.Scheme);
         var qr = qr_servis.GenerisiQrKod(url);
         KarteVM k = new(kd, $"data:image/png;base64,{Convert.ToBase64String(qr)}");
         lista.Add(k);
      }
      return View("Index", lista);
   }

   /// <summary>
   /// Kupuje kartu za trenutno prijavljenog putnika.
   /// </summary>
   /// <param name="raspored_id">Identifikator rasporeda.</param>
   /// <param name="polaziste_id">Identifikator polazne stanice.</param>
   /// <param name="odrediste_id">Identifikator odredišne stanice.</param>
   /// <returns>
   /// View za štampu kupljene karte ako je kupovina uspešna, inače BadRequest ili preusmeravanje.
   /// </returns>
   [HttpPost]
   public async Task<IActionResult> Kupi(int raspored_id, int polaziste_id, int odrediste_id)
   {
      int putnik_id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
      if (putnik_id <= 0)
         return RedirectToAction("Index", "Home");

      var Karta = await servis.Kupi(putnik_id, raspored_id, polaziste_id, odrediste_id);
      if (Karta == null)
         return BadRequest("Neuspesna kupovina");

      var Karta_DTO = await servis.VratiPodatke(Karta.Id, putnik_id);
      //sad ide qr
      if (Karta_DTO == null)
      {
         return BadRequest("Neuspesna kupovina");
      }

      var url = Url.Action("Ocitaj", "Kondukter", new { token = Karta.Qr_token }, Request.Scheme)!;
      var qr = qr_servis.GenerisiQrKod(url);
      ViewData["QrImageData"] = $"data:image/png;base64,{Convert.ToBase64String(qr)}";

      return View("Print", Karta_DTO);
   }
}