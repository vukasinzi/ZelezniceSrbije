using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Data
{
    public class VozAppContext : DbContext
    {
        public VozAppContext(DbContextOptions<VozAppContext> opcije):base(opcije)
        {
        }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Putnik> Putnik { get; set; }
        public DbSet<Administrator> Admin { get; set; }
        public DbSet<Kondukter> Kondukter { get; set; }
        public DbSet<Stajaliste> Stajaliste { get; set; }//LINIJA STANICA
        public DbSet<Raspored> Raspored { get; set; }
        public DbSet<Voz> Voz { get; set; }
        public DbSet<Linija> Linija { get; set; }
        public DbSet<Stanica> Stanica { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Putnik>().ToTable("Putnik");
            modelBuilder.Entity<Kondukter>().ToTable("Kondukter");
            modelBuilder.Entity<Administrator>().ToTable("Administrator");
            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
             modelBuilder.Entity<Stajaliste>().ToTable("Stajaliste");
            modelBuilder.Entity<Raspored>().ToTable("Raspored");
            modelBuilder.Entity<Voz>().ToTable("Voz");
            modelBuilder.Entity<Stanica>().ToTable("Stanica");
            modelBuilder.Entity<Linija>().ToTable("Linija");
        }
    }
}
