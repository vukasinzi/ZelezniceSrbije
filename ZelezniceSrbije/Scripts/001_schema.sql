create table Karta_backup_20260503
(
    id               int identity,
    cena             decimal(10, 2)   not null,
    ocitana          bit              not null,
    datum_ocitavanja datetime,
    putnik_id        int              not null,
    kondukter_id     int,
    raspored_id      int              not null,
    polaziste_id     int              not null,
    odrediste_id     int              not null,
    Qr_token         uniqueidentifier not null
)
go

create table Korisnik
(
    id      int identity
        primary key,
    ime     nvarchar(20)  not null,
    prezime nvarchar(20)  not null,
    email   nvarchar(150) not null
        unique,
    lozinka nvarchar(255) not null
)
go

create table Administrator
(
    id               int      not null
        primary key
        constraint FK_Administrator_Korisnik
            references Korisnik
            on delete cascade,
    datum_zaposlenja datetime not null
)
go

create table Karta
(
    Id               int identity
        primary key,
    Cena             decimal(10, 2)                   not null,
    Ocitana          bit              default 0       not null,
    Datum_ocitavanja datetime,
    Putnik_id        int                              not null
        constraint FK_Karta_Korisnik
            references Korisnik,
    Raspored_id      int                              not null,
    Polaziste        nvarchar(100)                    not null,
    Odrediste        nvarchar(100)                    not null,
    Linija           nvarchar(100)                    not null,
    Tip_voza         nvarchar(50)                     not null,
    Kondukter        nvarchar(200),
    Trajanje_min     int                              not null,
    Vreme_polaska    datetime                         not null,
    Vreme_dolaska    datetime                         not null,
    Qr_token         uniqueidentifier default newid() not null
)
go

create table Kondukter
(
    id                int          not null
        primary key
        constraint FK_Kondukter_Korisnik
            references Korisnik
            on delete cascade,
    broj_legitimacije nvarchar(50) not null
)
go

create table Linija
(
    id             int identity
        primary key,
    naziv          nvarchar(100) not null,
    cena_po_minutu int           not null
)
go

create table Putnik
(
    id            int not null
        primary key
        constraint FK_Putnik_Korisnik
            references Korisnik
            on delete cascade,
    broj_telefona nvarchar(20)
)
go

create table Stanica
(
    id     int identity
        primary key,
    naziv  nvarchar(100) not null,
    region nvarchar(100) not null
)
go

create table StanicaLinija
(
    id               int identity
        primary key,
    vreme_od_polaska int not null,
    redosled         int not null,
    stanica_id       int not null
        constraint FK_StanicaLinija_Stanica
            references Stanica,
    linija_id        int not null
        constraint FK_StanicaLinija_Linija
            references Linija
            on delete cascade
)
go

create table TipVoza
(
    id    int identity
        primary key,
    naziv nvarchar(100) not null,
    opis  nvarchar(500)
)
go

create table Voz
(
    id            int identity
        primary key,
    naziv         nvarchar(100) not null,
    serijski_broj nvarchar(50)  not null
        unique,
    aktivan       bit default 1 not null,
    tip_voza_id   int           not null
        constraint FK_Voz_TipVoza
            references TipVoza
)
go

create table RasporedSablon
(
    id                 int identity
        constraint PK_RasporedSablon
            primary key,
    linija_id          int                             not null
        constraint FK_RasporedSablon_Linija
            references Linija,
    voz_id             int                             not null
        constraint FK_RasporedSablon_Voz
            references Voz,
    vreme_polaska_time time(0)                         not null,
    aktivan            bit
        constraint DF_RasporedSablon_aktivan default 1 not null
)
go

create table Raspored
(
    id            int identity
        constraint PK_Raspored
            primary key,
    vreme_polaska datetime not null,
    linija_id     int      not null
        constraint FK_Raspored_Linija
            references Linija,
    voz_id        int      not null
        constraint FK_Raspored_Voz
            references Voz,
    sablon_id     int
        constraint FK_Raspored_Sablon
            references RasporedSablon
)
go

create unique index UX_RasporedSablon_LinijaVozTime
    on RasporedSablon (linija_id, voz_id, vreme_polaska_time)
go

CREATE PROCEDURE dbo.sp_GenerisiRasporede
@Od DATETIME,
@Do DATETIME
AS
BEGIN
DECLARE @TrenutniDatum DATE = CAST(@Od AS DATE);
	
while @TrenutniDatum <= CAST(@Do AS DATE)
begin
	insert into dbo.Raspored (vreme_polaska, linija_id, voz_id, sablon_id)
	select cast(@TrenutniDatum AS DATETIME) + cast(vreme_polaska_time AS DATETIME),
		   linija_id,
		   voz_id,
		   id
		   from dbo.RasporedSablon where aktivan = 1
		   and not exists (
	       select 1 FROM dbo.Raspored r 
	       where r.sablon_id = dbo.RasporedSablon.id 
	       and cast(r.vreme_polaska AS DATE) = @TrenutniDatum
 		   );
SET @TrenutniDatum = DATEADD(DAY, 1, @TrenutniDatum);
end
end
go


