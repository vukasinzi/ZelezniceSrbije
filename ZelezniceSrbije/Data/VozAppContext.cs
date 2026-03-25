using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Data
{
    public class VozAppContext : DbContext
    {
        public VozAppContext(DbContextOptions<VozAppContext> opcije):base(opcije)
        {
        }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Putnik> Putnici { get; set; }
        public DbSet<Administrator> Admini { get; set; }
        public DbSet<Kondukter> Kondukteri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Putnik>().ToTable("Putnik");
            modelBuilder.Entity<Kondukter>().ToTable("Kondukter");
            modelBuilder.Entity<Administrator>().ToTable("Administrator");
        }
    }
}
