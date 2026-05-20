using Microsoft.EntityFrameworkCore;
using ZelezniceSrbije.Models;

namespace ZelezniceSrbije.Data
{
    /// <summary>
    /// Kontekst baze podataka za aplikaciju.
    /// Sadrži DbSet kolekcije i konfiguraciju modela.
    /// </summary>
    public class VozAppContext : DbContext
    {
        /// <summary>
        /// Kreira novi kontekst baze podataka.
        /// </summary>
        /// <param name="opcije">Opcije za podešavanje konteksta baze.</param>
        public VozAppContext(DbContextOptions<VozAppContext> opcije) : base(opcije)
        {
        }

        /// <summary>
        /// Korisnici sistema.
        /// </summary>
        public DbSet<Korisnik> Korisnik { get; set; }

        /// <summary>
        /// Putnici sistema.
        /// </summary>
        public DbSet<Putnik> Putnik { get; set; }

        /// <summary>
        /// Administratori sistema.
        /// </summary>
        public DbSet<Administrator> Admin { get; set; }

        /// <summary>
        /// Kondukteri sistema.
        /// </summary>
        public DbSet<Kondukter> Kondukter { get; set; }

        /// <summary>
        /// Veze između stanica i linija.
        /// </summary>
        public DbSet<StanicaLinija> StanicaLinija { get; set; }//LINIJA STANICA

        /// <summary>
        /// Rasporedi vožnje.
        /// </summary>
        public DbSet<Raspored> Raspored { get; set; }

        /// <summary>
        /// Vozovi u sistemu.
        /// </summary>
        public DbSet<Voz> Voz { get; set; }

        /// <summary>
        /// Tipovi vozova.
        /// </summary>
        public DbSet<TipVoza> TipVoza { get; set; }

        /// <summary>
        /// Linije vožnje.
        /// </summary>
        public DbSet<Linija> Linija { get; set; }

        /// <summary>
        /// Stanice u sistemu.
        /// </summary>
        public DbSet<Stanica> Stanica { get; set; }

        /// <summary>
        /// Karte korisnika.
        /// </summary>
        public DbSet<Karta> Karta { get; set; }

        /// <summary>
        /// Podešava mapiranje modela na tabele i relacije u bazi.
        /// </summary>
        /// <param name="modelBuilder">Objekat za konfiguraciju modela.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Putnik>().ToTable("Putnik");
            modelBuilder.Entity<Kondukter>().ToTable("Kondukter");
            modelBuilder.Entity<Administrator>().ToTable("Administrator");
            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
            modelBuilder.Entity<Karta>(entity =>
            {
                entity.ToTable("Karta");

                entity.Property(x => x.Id).HasColumnName("Id");
                entity.Property(x => x.Cena).HasColumnName("Cena");
                entity.Property(x => x.Ocitana).HasColumnName("Ocitana");
                entity.Property(x => x.Datum_ocitavanja).HasColumnName("Datum_ocitavanja");

                entity.Property(x => x.Putnik_id).HasColumnName("Putnik_id");
                entity.Property(x => x.Raspored_id).HasColumnName("Raspored_id");

                entity.Property(x => x.Polaziste).HasColumnName("Polaziste");
                entity.Property(x => x.Odrediste).HasColumnName("Odrediste");
                entity.Property(x => x.Linija).HasColumnName("Linija");
                entity.Property(x => x.Tip_voza).HasColumnName("Tip_voza");
                entity.Property(x => x.Kondukter).HasColumnName("Kondukter");

                entity.Property(x => x.Trajanje_min).HasColumnName("Trajanje_min");
                entity.Property(x => x.Vreme_polaska).HasColumnName("Vreme_polaska");
                entity.Property(x => x.Vreme_dolaska).HasColumnName("Vreme_dolaska");

                entity.Property(x => x.Qr_token).HasColumnName("Qr_token");

                entity.HasOne<Korisnik>()
                .WithMany()
                .HasForeignKey(x => x.Putnik_id)
                .OnDelete(DeleteBehavior.NoAction);

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