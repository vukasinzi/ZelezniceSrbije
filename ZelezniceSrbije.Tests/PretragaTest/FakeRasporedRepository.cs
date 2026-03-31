using System;
using System.Collections.Generic;
using System.Text;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Repositories;

namespace ZelezniceSrbije.Tests.PretragaTest
{
    public class FakeRasporedRepository : IRasporedRepository
    {
        public List<Stanica> stanice { get; set; } = new();
        public List<RasporedDTO> rasporedi { get; set; } = new();
        public Task<List<RasporedDTO>> PretraziAsync(string polaziste, string odrediste, DateTime datum)
        {
            
            return Task.FromResult(rasporedi);
        }

        public Task<List<Stanica>> UcitajStaniceAsync()
        {
            return Task.FromResult(stanice);
        }
    }
}
