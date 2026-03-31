using System;
using System.Collections.Generic;
using System.Text;
using ZelezniceSrbije.Models;
using ZelezniceSrbije.Services;
using Microsoft.EntityFrameworkCore;
namespace ZelezniceSrbije.Tests.PretragaTest
{
    public class RasporedServiceTest
    {

        private RasporedService servis;
        private FakeRasporedRepository repo;
        public RasporedServiceTest()
        {
            repo = new();
            servis = new RasporedService(repo);
        }
        [Fact]
        public async Task UcitajStanica_Test()
        {
      
            var s1 = new Stanica(1, "Beograd Centar", "Savski Venac");
            var s2 = new Stanica(2, "Zemun", "Zemun");

            repo.stanice.AddRange(new[] { s1, s2 });

       
            var rezultat = await servis.UcitajStaniceAsync();

          
            Assert.Equal(2, rezultat.Count);
            Assert.Contains(rezultat, x => x.Naziv == "Beograd Centar");
            Assert.Contains(rezultat, x => x.Naziv == "Zemun");

        }
        [Theory]
        [InlineData("","Novi Sad",false)]
        [InlineData(null,"Novi Sad",false)]
        [InlineData("Novi Sad",null,false)]
        [InlineData("Beograd Centar","Beograd Centar",false)]
        [InlineData("Beograd Centar","Novi Sad",true)]

        public async Task PretragaParametri_Test(string polaziste, string odrediste, bool trebaDaUspe)
        {
            DateTime sutra = DateTime.Now.AddDays(1);
            var rezultat = await servis.PretraziAsync(polaziste, odrediste, sutra);
            if (trebaDaUspe)
                Assert.NotNull(rezultat);
            else
                Assert.Null(rezultat);
        }
        [Fact]
        public async Task PretragaDatumi_Test()
        {
            var rezultat = await servis.PretraziAsync("Beograd centar", "Novi sad", DateTime.Now.AddDays(-1));
            Assert.Null(rezultat);
        }


    }

}