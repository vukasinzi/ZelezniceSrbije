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
        /// Označava da li se kontekst koristi u testovima.
        /// </summary>
        private readonly bool test;

        /// <summary>
        /// Kreira novi kontekst baze podataka.
        /// </summary>
        /// <param name="opcije">Opcije za podešavanje konteksta baze.</param>
        public VozAppContext(DbContextOptions<VozAppContext> opcije) : this(opcije, false)
        {
        }

        /// <summary>
        /// Kreira novi kontekst baze podataka.
        /// </summary>
        /// <param name="opcije">Opcije za podešavanje konteksta baze.</param>
        /// <param name="test">Označava da li se kontekst koristi u testovima.</param>
        public VozAppContext(DbContextOptions<VozAppContext> opcije, bool test) : base(opcije)
        {
            this.test = test;
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
        public DbSet<StanicaLinija> StanicaLinija { get; set; } //LINIJA STANICA

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
            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.ToTable("Korisnik");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Email).HasColumnName("email").HasMaxLength(150).IsRequired();
                entity.HasIndex(x => x.Email).IsUnique();
                entity.Property(x => x.Ime).HasColumnName("ime").HasMaxLength(20);
                entity.Property(x => x.Prezime).HasColumnName("prezime").HasMaxLength(20);
            });
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.ToTable("Administrator");
                //   entity.HasKey(x => x.Id);
                entity.Property(x => x.Datum_zaposlenja).HasColumnName("datum_zaposlenja").IsRequired();
            });
            modelBuilder.Entity<Kondukter>(entity =>
            {
                entity.ToTable("Kondukter");
                // entity.HasKey(x => x.Id);
                entity.Property(x => x.Broj_legitimacije).IsRequired().HasColumnName("broj_legitimacije")
                    .HasMaxLength(50);

            });
            modelBuilder.Entity<Putnik>(entity =>
            {
                entity.ToTable("Putnik");
                //entity.HasKey(x => x.Id);
                entity.Property(x => x.Broj_telefona).IsRequired().HasColumnName("broj_telefona").HasMaxLength(20);
            });
            modelBuilder.Entity<Karta>(entity =>
            {
                entity.ToTable("Karta");
                entity.HasKey(x => x.Id);
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
                entity.HasOne<Raspored>()
                    .WithMany()
                    .HasForeignKey(x => x.Raspored_id)
                    .OnDelete(DeleteBehavior.NoAction);

            });
            modelBuilder.Entity<StanicaLinija>(entity =>
            {
                entity.ToTable("StanicaLinija");
                entity.HasKey(x => x.Id);
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
                entity.HasKey(x => x.Id);
  
                entity.Property(x => x.Id)
                    .HasColumnName("id");

                entity.Property(x => x.Vreme_polaska)
                    .HasColumnName("vreme_polaska")
                    .IsRequired();

                entity.Property(x => x.Linija_id)
                    .HasColumnName("linija_id")
                    .IsRequired();

                entity.Property(x => x.Voz_id)
                    .HasColumnName("voz_id")
                    .IsRequired();

                entity.Property(x => x.Sablon_id)
                    .HasColumnName("sablon_id");

                entity.HasOne(x => x.Linija)
                    .WithMany()
                    .HasForeignKey(x => x.Linija_id)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(x => x.Voz)
                    .WithMany()
                    .HasForeignKey(x => x.Voz_id)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne<RasporedSablon>()
                    .WithMany()
                    .HasForeignKey(x => x.Sablon_id)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Voz>(entity =>
            {
                entity.ToTable("Voz");
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.TipVoza)
                    .WithMany()
                    .HasForeignKey(z => z.Tip_voza_id);
                entity.Property(x => x.Naziv).HasMaxLength(100);
                entity.Property(x => x.Serijski_broj).HasMaxLength(50);

            });
            modelBuilder.Entity<Stanica>(entity =>
            {
                entity.ToTable("Stanica");
                entity.HasKey(x => x.Id);
            });
            modelBuilder.Entity<Linija>(entity =>
            {
                entity.ToTable("Linija");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Naziv).HasMaxLength(30);

            });
            modelBuilder.Entity<TipVoza>(entity =>
            {
                entity.ToTable("TipVoza");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Naziv).HasMaxLength(100);
                entity.Property(x => x.Opis).HasMaxLength(500);
            });
            modelBuilder.Entity<RasporedSablon>(entity =>
            {
                entity.ToTable("RasporedSablon");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .HasColumnName("id");

                entity.Property(x => x.Linija_id)
                    .HasColumnName("linija_id")
                    .IsRequired();

                entity.Property(x => x.Voz_id)
                    .HasColumnName("voz_id")
                    .IsRequired();

                entity.Property(x => x.Vreme_polaska_time)
                    .HasColumnName("vreme_polaska_time")
                    .HasColumnType("time")
                    .IsRequired();

                entity.Property(x => x.Aktivan)
                    .HasColumnName("aktivan")
                    .IsRequired();

                entity.HasOne<Linija>()
                    .WithMany()
                    .HasForeignKey(x => x.Linija_id)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne<Voz>()
                    .WithMany()
                    .HasForeignKey(x => x.Voz_id)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            if (!test)
                DodajSeedPodatke(modelBuilder);
        }


        /// <summary>
        /// Dodaje početne podatke u model baze podataka.
        /// Seeduje stanice, linije, tipove vozova, vozove i veze između stanica i linija.
        /// </summary>
        /// <param name="modelBuilder">
        /// Objekat koji se koristi za konfiguraciju EF Core modela i dodavanje seed podataka.
        /// </param>
        private static void DodajSeedPodatke(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stanica>().HasData(
                new Stanica(1, "Beograd Centar", "Beograd"),
                new Stanica(2, "Novi Beograd", "Beograd"),
                new Stanica(3, "Zemun", "Beograd"),
                new Stanica(4, "Batajnica", "Beograd"),
                new Stanica(5, "Nova Pazova", "Srem"),
                new Stanica(6, "Stara Pazova", "Srem"),
                new Stanica(7, "Inđija", "Srem"),
                new Stanica(8, "Beška", "Srem"),
                new Stanica(9, "Sremski Karlovci", "Srem"),
                new Stanica(10, "Petrovaradin", "Bačka"),
                new Stanica(11, "Novi Sad", "Bačka"),
                new Stanica(12, "Zmajevo", "Bačka"),
                new Stanica(13, "Vrbas Nova", "Bačka"),
                new Stanica(14, "Lovćenac-Mali Iđoš", "Bačka"),
                new Stanica(15, "Bačka Topola", "Bačka"),
                new Stanica(16, "Žednik", "Bačka"),
                new Stanica(17, "Subotica", "Bačka"),
                new Stanica(18, "Pančevo", "Banat"),
                new Stanica(19, "Vršac", "Banat"),
                new Stanica(20, "Ruma", "Srem"),
                new Stanica(21, "Sremska Mitrovica", "Srem"),
                new Stanica(22, "Šid", "Srem"),
                new Stanica(23, "Lajkovac", "Mačva i Kolubara"),
                new Stanica(24, "Valjevo", "Mačva i Kolubara"),
                new Stanica(25, "Požega", "Zlatibor"),
                new Stanica(26, "Užice", "Zlatibor"),
                new Stanica(27, "Mladenovac", "Šumadija"),
                new Stanica(28, "Lapovo", "Šumadija"),
                new Stanica(29, "Jagodina", "Pomoravlje"),
                new Stanica(30, "Ćuprija", "Pomoravlje"),
                new Stanica(31, "Paraćin", "Pomoravlje"),
                new Stanica(32, "Aleksinac", "Južna Srbija"),
                new Stanica(33, "Niš", "Južna Srbija")
            );

            modelBuilder.Entity<Linija>().HasData(
                new Linija(1, "Beograd Centar – Novi Sad", 12),
                new Linija(2, "Novi Sad – Beograd Centar", 12),
                new Linija(3, "Subotica – Beograd Centar", 18),
                new Linija(4, "Beograd Centar – Subotica", 16),
                new Linija(5, "Subotica – Beograd Centar", 16),
                new Linija(6, "Zemun – Niš", 8),
                new Linija(7, "Niš – Zemun", 8),
                new Linija(8, "Zemun – Pančevo", 8),
                new Linija(9, "Pančevo – Zemun", 8),
                new Linija(10, "Zemun – Vršac", 8),
                new Linija(11, "Vršac – Zemun", 8),
                new Linija(12, "Zemun – Šid", 8),
                new Linija(13, "Šid – Zemun", 8),
                new Linija(14, "Novi Sad – Šid", 8),
                new Linija(15, "Šid – Novi Sad", 8),
                new Linija(16, "Zemun – Užice", 8),
                new Linija(17, "Užice – Zemun", 8),
                new Linija(18, "Zemun – Valjevo", 8),
                new Linija(19, "Valjevo – Zemun", 8),
                new Linija(20, "Beograd Centar - Vršac", 12)
            );

            modelBuilder.Entity<TipVoza>().HasData(
                new TipVoza(1, "Soko", "Najbrzi vozicc"),
                new TipVoza(2, "InterCity", "Brzi međugradski voz"),
                new TipVoza(3, "Regio Express", "Polubrzi voz između malo većih mesta"),
                new TipVoza(4, "Regio", "Regionalni voz. Staje na svaku banderu")
            );

            modelBuilder.Entity<Voz>().HasData(
                new Voz(1, "Stadler KISS 200", "KISS-200-01", true, 1),
                new Voz(2, "Stadler KISS 200-2", "KISS-200-02", true, 2),
                new Voz(3, "Stadler FLIRT 3", "FLIRT-3-01", true, 3),
                new Voz(4, "Siemens Desiro", "DESIRO-012", true, 4)
            );

            modelBuilder.Entity<StanicaLinija>().HasData(
                new StanicaLinija(0, 1, 1, 1) { Id = 1 },
                new StanicaLinija(5, 2, 2, 1) { Id = 2 },
                new StanicaLinija(10, 3, 3, 1) { Id = 3 },
                new StanicaLinija(18, 4, 4, 1) { Id = 4 },
                new StanicaLinija(25, 5, 5, 1) { Id = 5 },
                new StanicaLinija(30, 6, 6, 1) { Id = 6 },
                new StanicaLinija(38, 7, 7, 1) { Id = 7 },
                new StanicaLinija(45, 8, 8, 1) { Id = 8 },
                new StanicaLinija(55, 9, 9, 1) { Id = 9 },
                new StanicaLinija(62, 10, 10, 1) { Id = 10 },
                new StanicaLinija(68, 11, 11, 1) { Id = 11 },
                new StanicaLinija(0, 1, 11, 2) { Id = 12 },
                new StanicaLinija(6, 2, 10, 2) { Id = 13 },
                new StanicaLinija(13, 3, 9, 2) { Id = 14 },
                new StanicaLinija(23, 4, 8, 2) { Id = 15 },
                new StanicaLinija(30, 5, 7, 2) { Id = 16 },
                new StanicaLinija(38, 6, 6, 2) { Id = 17 },
                new StanicaLinija(43, 7, 5, 2) { Id = 18 },
                new StanicaLinija(50, 8, 4, 2) { Id = 19 },
                new StanicaLinija(58, 9, 3, 2) { Id = 20 },
                new StanicaLinija(63, 10, 2, 2) { Id = 21 },
                new StanicaLinija(68, 11, 1, 2) { Id = 22 },
                new StanicaLinija(0, 1, 17, 3) { Id = 23 },
                new StanicaLinija(10, 2, 16, 3) { Id = 24 },
                new StanicaLinija(25, 3, 15, 3) { Id = 25 },
                new StanicaLinija(35, 4, 14, 3) { Id = 26 },
                new StanicaLinija(45, 5, 13, 3) { Id = 27 },
                new StanicaLinija(55, 6, 12, 3) { Id = 28 },
                new StanicaLinija(75, 7, 11, 3) { Id = 29 },
                new StanicaLinija(81, 8, 10, 3) { Id = 30 },
                new StanicaLinija(88, 9, 9, 3) { Id = 31 },
                new StanicaLinija(98, 10, 8, 3) { Id = 32 },
                new StanicaLinija(105, 11, 7, 3) { Id = 33 },
                new StanicaLinija(113, 12, 6, 3) { Id = 34 },
                new StanicaLinija(118, 13, 5, 3) { Id = 35 },
                new StanicaLinija(125, 14, 4, 3) { Id = 36 },
                new StanicaLinija(133, 15, 3, 3) { Id = 37 },
                new StanicaLinija(138, 16, 2, 3) { Id = 38 },
                new StanicaLinija(143, 17, 1, 3) { Id = 39 },
                new StanicaLinija(0, 1, 1, 4) { Id = 40 },
                new StanicaLinija(5, 2, 2, 4) { Id = 41 },
                new StanicaLinija(10, 3, 3, 4) { Id = 42 },
                new StanicaLinija(18, 4, 4, 4) { Id = 43 },
                new StanicaLinija(25, 5, 5, 4) { Id = 44 },
                new StanicaLinija(30, 6, 6, 4) { Id = 45 },
                new StanicaLinija(38, 7, 7, 4) { Id = 46 },
                new StanicaLinija(45, 8, 8, 4) { Id = 47 },
                new StanicaLinija(55, 9, 9, 4) { Id = 48 },
                new StanicaLinija(62, 10, 10, 4) { Id = 49 },
                new StanicaLinija(68, 11, 11, 4) { Id = 50 },
                new StanicaLinija(88, 12, 12, 4) { Id = 51 },
                new StanicaLinija(98, 13, 13, 4) { Id = 52 },
                new StanicaLinija(108, 14, 14, 4) { Id = 53 },
                new StanicaLinija(118, 15, 15, 4) { Id = 54 },
                new StanicaLinija(133, 16, 16, 4) { Id = 55 },
                new StanicaLinija(143, 17, 17, 4) { Id = 56 },
                new StanicaLinija(0, 1, 17, 5) { Id = 57 },
                new StanicaLinija(20, 2, 15, 5) { Id = 58 },
                new StanicaLinija(35, 3, 13, 5) { Id = 59 },
                new StanicaLinija(60, 4, 11, 5) { Id = 60 },
                new StanicaLinija(110, 5, 1, 5) { Id = 61 },
                new StanicaLinija(0, 1, 3, 6) { Id = 62 },
                new StanicaLinija(5, 2, 2, 6) { Id = 63 },
                new StanicaLinija(10, 3, 1, 6) { Id = 64 },
                new StanicaLinija(50, 4, 27, 6) { Id = 65 },
                new StanicaLinija(80, 5, 28, 6) { Id = 66 },
                new StanicaLinija(95, 6, 29, 6) { Id = 67 },
                new StanicaLinija(105, 7, 30, 6) { Id = 68 },
                new StanicaLinija(115, 8, 31, 6) { Id = 69 },
                new StanicaLinija(135, 9, 32, 6) { Id = 70 },
                new StanicaLinija(160, 10, 33, 6) { Id = 71 },
                new StanicaLinija(0, 1, 33, 7) { Id = 72 },
                new StanicaLinija(25, 2, 32, 7) { Id = 73 },
                new StanicaLinija(45, 3, 31, 7) { Id = 74 },
                new StanicaLinija(55, 4, 30, 7) { Id = 75 },
                new StanicaLinija(65, 5, 29, 7) { Id = 76 },
                new StanicaLinija(80, 6, 28, 7) { Id = 77 },
                new StanicaLinija(110, 7, 27, 7) { Id = 78 },
                new StanicaLinija(150, 8, 1, 7) { Id = 79 },
                new StanicaLinija(155, 9, 2, 7) { Id = 80 },
                new StanicaLinija(160, 10, 3, 7) { Id = 81 },
                new StanicaLinija(0, 1, 3, 8) { Id = 82 },
                new StanicaLinija(5, 2, 2, 8) { Id = 83 },
                new StanicaLinija(10, 3, 1, 8) { Id = 84 },
                new StanicaLinija(30, 4, 18, 8) { Id = 85 },
                new StanicaLinija(0, 1, 18, 9) { Id = 86 },
                new StanicaLinija(20, 2, 1, 9) { Id = 87 },
                new StanicaLinija(25, 3, 2, 9) { Id = 88 },
                new StanicaLinija(30, 4, 3, 9) { Id = 89 },
                new StanicaLinija(0, 1, 3, 10) { Id = 90 },
                new StanicaLinija(5, 2, 2, 10) { Id = 91 },
                new StanicaLinija(10, 3, 1, 10) { Id = 92 },
                new StanicaLinija(30, 4, 18, 10) { Id = 93 },
                new StanicaLinija(75, 5, 19, 10) { Id = 94 },
                new StanicaLinija(0, 1, 19, 11) { Id = 95 },
                new StanicaLinija(45, 2, 18, 11) { Id = 96 },
                new StanicaLinija(65, 3, 1, 11) { Id = 97 },
                new StanicaLinija(70, 4, 2, 11) { Id = 98 },
                new StanicaLinija(75, 5, 3, 11) { Id = 99 },
                new StanicaLinija(0, 1, 3, 12) { Id = 100 },
                new StanicaLinija(8, 2, 4, 12) { Id = 101 },
                new StanicaLinija(15, 3, 5, 12) { Id = 102 },
                new StanicaLinija(20, 4, 6, 12) { Id = 103 },
                new StanicaLinija(45, 5, 20, 12) { Id = 104 },
                new StanicaLinija(60, 6, 21, 12) { Id = 105 },
                new StanicaLinija(80, 7, 22, 12) { Id = 106 },
                new StanicaLinija(0, 1, 22, 13) { Id = 107 },
                new StanicaLinija(20, 2, 21, 13) { Id = 108 },
                new StanicaLinija(35, 3, 20, 13) { Id = 109 },
                new StanicaLinija(60, 4, 6, 13) { Id = 110 },
                new StanicaLinija(65, 5, 5, 13) { Id = 111 },
                new StanicaLinija(72, 6, 4, 13) { Id = 112 },
                new StanicaLinija(80, 7, 3, 13) { Id = 113 },
                new StanicaLinija(0, 1, 11, 14) { Id = 114 },
                new StanicaLinija(6, 2, 10, 14) { Id = 115 },
                new StanicaLinija(13, 3, 9, 14) { Id = 116 },
                new StanicaLinija(23, 4, 8, 14) { Id = 117 },
                new StanicaLinija(30, 5, 7, 14) { Id = 118 },
                new StanicaLinija(38, 6, 6, 14) { Id = 119 },
                new StanicaLinija(63, 7, 20, 14) { Id = 120 },
                new StanicaLinija(78, 8, 21, 14) { Id = 121 },
                new StanicaLinija(98, 9, 22, 14) { Id = 122 },
                new StanicaLinija(0, 1, 22, 15) { Id = 123 },
                new StanicaLinija(20, 2, 21, 15) { Id = 124 },
                new StanicaLinija(35, 3, 20, 15) { Id = 125 },
                new StanicaLinija(60, 4, 6, 15) { Id = 126 },
                new StanicaLinija(68, 5, 7, 15) { Id = 127 },
                new StanicaLinija(75, 6, 8, 15) { Id = 128 },
                new StanicaLinija(85, 7, 9, 15) { Id = 129 },
                new StanicaLinija(92, 8, 10, 15) { Id = 130 },
                new StanicaLinija(98, 9, 11, 15) { Id = 131 },
                new StanicaLinija(0, 1, 3, 16) { Id = 132 },
                new StanicaLinija(5, 2, 2, 16) { Id = 133 },
                new StanicaLinija(10, 3, 1, 16) { Id = 134 },
                new StanicaLinija(60, 4, 23, 16) { Id = 135 },
                new StanicaLinija(80, 5, 24, 16) { Id = 136 },
                new StanicaLinija(130, 6, 25, 16) { Id = 137 },
                new StanicaLinija(150, 7, 26, 16) { Id = 138 },
                new StanicaLinija(0, 1, 26, 17) { Id = 139 },
                new StanicaLinija(20, 2, 25, 17) { Id = 140 },
                new StanicaLinija(70, 3, 24, 17) { Id = 141 },
                new StanicaLinija(90, 4, 23, 17) { Id = 142 },
                new StanicaLinija(140, 5, 1, 17) { Id = 143 },
                new StanicaLinija(145, 6, 2, 17) { Id = 144 },
                new StanicaLinija(150, 7, 3, 17) { Id = 145 },
                new StanicaLinija(0, 1, 3, 18) { Id = 146 },
                new StanicaLinija(5, 2, 2, 18) { Id = 147 },
                new StanicaLinija(10, 3, 1, 18) { Id = 148 },
                new StanicaLinija(60, 4, 23, 18) { Id = 149 },
                new StanicaLinija(80, 5, 24, 18) { Id = 150 },
                new StanicaLinija(0, 1, 24, 19) { Id = 151 },
                new StanicaLinija(20, 2, 23, 19) { Id = 152 },
                new StanicaLinija(70, 3, 1, 19) { Id = 153 },
                new StanicaLinija(75, 4, 2, 19) { Id = 154 },
                new StanicaLinija(80, 5, 3, 19) { Id = 155 },
                new StanicaLinija(0, 1, 1, 20) { Id = 156 },
                new StanicaLinija(20, 2, 18, 20) { Id = 157 },
                new StanicaLinija(65, 3, 19, 20) { Id = 158 }
            );
            modelBuilder.Entity<RasporedSablon>().HasData(
                new RasporedSablon
                    { Id = 1, Linija_id = 1, Voz_id = 1, Vreme_polaska_time = new TimeSpan(7, 0, 0), Aktivan = true },
                new RasporedSablon
                    { Id = 2, Linija_id = 1, Voz_id = 2, Vreme_polaska_time = new TimeSpan(13, 0, 0), Aktivan = true },
                new RasporedSablon
                    { Id = 3, Linija_id = 1, Voz_id = 1, Vreme_polaska_time = new TimeSpan(19, 0, 0), Aktivan = true },
                new RasporedSablon
                    { Id = 4, Linija_id = 2, Voz_id = 2, Vreme_polaska_time = new TimeSpan(9, 0, 0), Aktivan = true },
                new RasporedSablon
                    { Id = 5, Linija_id = 2, Voz_id = 1, Vreme_polaska_time = new TimeSpan(15, 0, 0), Aktivan = true },
                new RasporedSablon
                    { Id = 6, Linija_id = 2, Voz_id = 2, Vreme_polaska_time = new TimeSpan(21, 0, 0), Aktivan = true },
                new RasporedSablon
                    { Id = 7, Linija_id = 3, Voz_id = 1, Vreme_polaska_time = new TimeSpan(6, 0, 0), Aktivan = true },
                new RasporedSablon
                    { Id = 8, Linija_id = 3, Voz_id = 2, Vreme_polaska_time = new TimeSpan(14, 0, 0), Aktivan = true },
                new RasporedSablon
                    { Id = 9, Linija_id = 4, Voz_id = 2, Vreme_polaska_time = new TimeSpan(9, 30, 0), Aktivan = true },
                new RasporedSablon
                {
                    Id = 10, Linija_id = 4, Voz_id = 1, Vreme_polaska_time = new TimeSpan(17, 30, 0), Aktivan = true
                },
                new RasporedSablon
                    { Id = 11, Linija_id = 5, Voz_id = 1, Vreme_polaska_time = new TimeSpan(7, 15, 0), Aktivan = true },
                new RasporedSablon
                {
                    Id = 12, Linija_id = 5, Voz_id = 2, Vreme_polaska_time = new TimeSpan(16, 15, 0), Aktivan = true
                },
                new RasporedSablon
                    { Id = 13, Linija_id = 6, Voz_id = 1, Vreme_polaska_time = new TimeSpan(5, 0, 0), Aktivan = true },
                new RasporedSablon
                    { Id = 14, Linija_id = 6, Voz_id = 2, Vreme_polaska_time = new TimeSpan(15, 0, 0), Aktivan = true },
                new RasporedSablon
                    { Id = 15, Linija_id = 7, Voz_id = 2, Vreme_polaska_time = new TimeSpan(9, 30, 0), Aktivan = true },
                new RasporedSablon
                {
                    Id = 16, Linija_id = 7, Voz_id = 1, Vreme_polaska_time = new TimeSpan(19, 30, 0), Aktivan = true
                },
                new RasporedSablon
                    { Id = 17, Linija_id = 8, Voz_id = 1, Vreme_polaska_time = new TimeSpan(6, 30, 0), Aktivan = true },
                new RasporedSablon
                {
                    Id = 18, Linija_id = 8, Voz_id = 2, Vreme_polaska_time = new TimeSpan(12, 30, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 19, Linija_id = 8, Voz_id = 1, Vreme_polaska_time = new TimeSpan(18, 30, 0), Aktivan = true
                },
                new RasporedSablon
                    { Id = 20, Linija_id = 9, Voz_id = 2, Vreme_polaska_time = new TimeSpan(7, 30, 0), Aktivan = true },
                new RasporedSablon
                {
                    Id = 21, Linija_id = 9, Voz_id = 1, Vreme_polaska_time = new TimeSpan(13, 30, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 22, Linija_id = 9, Voz_id = 2, Vreme_polaska_time = new TimeSpan(19, 30, 0), Aktivan = true
                },
                new RasporedSablon
                    { Id = 23, Linija_id = 10, Voz_id = 1, Vreme_polaska_time = new TimeSpan(7, 0, 0), Aktivan = true },
                new RasporedSablon
                {
                    Id = 24, Linija_id = 10, Voz_id = 2, Vreme_polaska_time = new TimeSpan(14, 0, 0), Aktivan = true
                },
                new RasporedSablon
                    { Id = 25, Linija_id = 11, Voz_id = 2, Vreme_polaska_time = new TimeSpan(9, 0, 0), Aktivan = true },
                new RasporedSablon
                {
                    Id = 26, Linija_id = 11, Voz_id = 1, Vreme_polaska_time = new TimeSpan(16, 0, 0), Aktivan = true
                },
                new RasporedSablon
                    { Id = 27, Linija_id = 12, Voz_id = 1, Vreme_polaska_time = new TimeSpan(8, 0, 0), Aktivan = true },
                new RasporedSablon
                {
                    Id = 28, Linija_id = 12, Voz_id = 2, Vreme_polaska_time = new TimeSpan(16, 0, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 29, Linija_id = 13, Voz_id = 2, Vreme_polaska_time = new TimeSpan(10, 15, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 30, Linija_id = 13, Voz_id = 1, Vreme_polaska_time = new TimeSpan(18, 15, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 31, Linija_id = 14, Voz_id = 1, Vreme_polaska_time = new TimeSpan(7, 45, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 32, Linija_id = 14, Voz_id = 2, Vreme_polaska_time = new TimeSpan(13, 45, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 33, Linija_id = 15, Voz_id = 2, Vreme_polaska_time = new TimeSpan(10, 0, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 34, Linija_id = 15, Voz_id = 1, Vreme_polaska_time = new TimeSpan(16, 0, 0), Aktivan = true
                },
                new RasporedSablon
                    { Id = 35, Linija_id = 16, Voz_id = 1, Vreme_polaska_time = new TimeSpan(6, 0, 0), Aktivan = true },
                new RasporedSablon
                {
                    Id = 36, Linija_id = 16, Voz_id = 2, Vreme_polaska_time = new TimeSpan(14, 15, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 37, Linija_id = 17, Voz_id = 2, Vreme_polaska_time = new TimeSpan(9, 45, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 38, Linija_id = 17, Voz_id = 1, Vreme_polaska_time = new TimeSpan(18, 0, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 39, Linija_id = 18, Voz_id = 1, Vreme_polaska_time = new TimeSpan(7, 10, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 40, Linija_id = 18, Voz_id = 2, Vreme_polaska_time = new TimeSpan(15, 10, 0), Aktivan = true
                },
                new RasporedSablon
                    { Id = 41, Linija_id = 19, Voz_id = 2, Vreme_polaska_time = new TimeSpan(9, 0, 0), Aktivan = true },
                new RasporedSablon
                {
                    Id = 42, Linija_id = 19, Voz_id = 1, Vreme_polaska_time = new TimeSpan(17, 0, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 43, Linija_id = 20, Voz_id = 1, Vreme_polaska_time = new TimeSpan(8, 30, 0), Aktivan = true
                },
                new RasporedSablon
                {
                    Id = 44, Linija_id = 20, Voz_id = 2, Vreme_polaska_time = new TimeSpan(20, 30, 0), Aktivan = true
                }
            );
        }
    }
}
