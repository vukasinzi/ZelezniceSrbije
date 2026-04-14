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
        public DbSet<StanicaLinija> StanicaLinija { get; set; }//LINIJA STANICA
        public DbSet<Raspored> Raspored { get; set; }
        public DbSet<Voz> Voz { get; set; }
        public DbSet<TipVoza> TipVoza { get; set; }
        public DbSet<Linija> Linija { get; set; }
        public DbSet<Stanica> Stanica { get; set; }
        public DbSet<Karta> Karta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Putnik>().ToTable("Putnik");
            modelBuilder.Entity<Kondukter>().ToTable("Kondukter");
            modelBuilder.Entity<Administrator>().ToTable("Administrator");
            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
            modelBuilder.Entity<Karta>(entity =>
            {
                entity.ToTable("Karta");

                entity.Property(x => x.Kondukter_id).HasColumnName("Kondukter_id");
                entity.Property(x => x.Putnik_id).HasColumnName("Putnik_id");
                entity.Property(x => x.Raspored_id).HasColumnName("Raspored_id");
                entity.Property(x => x.Polaziste_id).HasColumnName("Polaziste_id");
                entity.Property(x => x.Odrediste_id).HasColumnName("Odrediste_id");
                entity.Property(x => x.QrToken).HasColumnName("Qr_token");

            });
            modelBuilder.Entity<StanicaLinija>(entity =>
            {
                entity.ToTable("StanicaLinija");

                entity.Property(x => x.Stanica_id).HasColumnName("Stanica_id");
                entity.Property(x => x.Linija_id).HasColumnName("Linija_id");
                entity.Property(x => x.Vreme_od_polaska).HasColumnName("Vreme_od_polaska");
                entity.Property(x => x.Redosled).HasColumnName("Redosled");

                entity.HasOne(x => x.Stanica)
                    .WithMany()
                    .HasForeignKey(x => x.Stanica_id)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Linija)
                    .WithMany()
                    .HasForeignKey(x => x.Linija_id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Raspored>(entity =>
            {
                entity.ToTable("Raspored");
                entity.Property(x => x.Linija_id).HasColumnName("Linija_id");
                entity.Property(x => x.Voz_id).HasColumnName("Voz_id");

                entity.HasOne(x => x.Linija)
        .WithMany()
        .HasForeignKey(x => x.Linija_id);

                entity.HasOne(x => x.Voz)
                    .WithMany()
                    .HasForeignKey(x => x.Voz_id);
            });

            modelBuilder.Entity<Voz>().ToTable("Voz");
            modelBuilder.Entity<Stanica>().ToTable("Stanica");
            modelBuilder.Entity<Linija>().ToTable("Linija");
            modelBuilder.Entity<TipVoza>().ToTable("TipVoza");
        }
    }
}
