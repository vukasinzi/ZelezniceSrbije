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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Putnik>().ToTable("Putnik");
            modelBuilder.Entity<Kondukter>().ToTable("Kondukter");
            modelBuilder.Entity<Administrator>().ToTable("Administrator");
        }
    }
}
