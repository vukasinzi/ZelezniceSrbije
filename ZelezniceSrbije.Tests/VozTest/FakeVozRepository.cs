using System;
using System.Collections.Generic;
using System.Text;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Tests.VozTest
{
    public class FakeVozRepository : IVozRepository
    {
        public List<Voz> vozovi { get; set; } = new();
        public List<TipVoza> tipovi_voza { get; set; } = new();
        public Task DodajTipVoza(TipVoza tipVoza)
        {
            tipovi_voza.Add(tipVoza);
            return Task.CompletedTask;

        }

        public Task DodajVoz(Voz voz)
        {
            vozovi.Add(voz);
            return Task.CompletedTask;

        }

        public Task IzmeniTipVoza(TipVoza tipVoza)
        {
            var t = tipovi_voza.FirstOrDefault(x=> x.Id==tipVoza.Id);
            t.Naziv = tipVoza.Naziv;
            t.Opis = tipVoza.Opis;
            return Task.CompletedTask;
        }
        
        public Task IzmeniVoz(Voz voz)
        {
            var t = vozovi.FirstOrDefault(x => x.Id == voz.Id);
            t.Naziv = voz.Naziv;
            t.Aktivan = voz.Aktivan;
            t.Serijski_broj = voz.Serijski_broj;
            t.Tip_voza_id = voz.Tip_voza_id;
            return Task.CompletedTask;
        }

        public Task<List<TipVoza>> UcitajSveTipoveVoza()
        {
            return Task.FromResult(tipovi_voza);
        }

        public Task<List<Voz>> UcitajSveVozove()
        {
            return Task.FromResult(vozovi);
        }

        public Task UkloniTipVoza(int id)
        {
            tipovi_voza.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;

        }

        public Task UkloniVoz(int id)
        {
            vozovi.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;
        }
    }
}
