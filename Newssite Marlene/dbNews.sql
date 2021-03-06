/****** Object:  Table [dbo].[tblBruger]    Script Date: 08-03-2019 09:46:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBruger](
	[idUser] [int] IDENTITY(1,1) NOT NULL,
	[brugernavn] [nvarchar](150) NOT NULL,
	[password] [nvarchar](500) NOT NULL,
	[email] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_tblBruger] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblKategorier]    Script Date: 08-03-2019 09:46:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblKategorier](
	[idCategory] [int] IDENTITY(1,1) NOT NULL,
	[category] [nvarchar](100) NOT NULL,
	[categoryImg] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_tblKategorier] PRIMARY KEY CLUSTERED 
(
	[idCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblKontact]    Script Date: 08-03-2019 09:46:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblKontact](
	[idKontact] [int] NOT NULL,
	[firm] [nvarchar](50) NOT NULL,
	[address] [nvarchar](50) NOT NULL,
	[zipcode] [int] NOT NULL,
	[city] [nvarchar](50) NOT NULL,
	[phone] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblKontact] PRIMARY KEY CLUSTERED 
(
	[idKontact] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNyheder]    Script Date: 08-03-2019 09:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNyheder](
	[idNews] [int] IDENTITY(1,1) NOT NULL,
	[overskrift] [nvarchar](50) NOT NULL,
	[teasertekst] [nvarchar](300) NOT NULL,
	[nyheden] [nvarchar](max) NOT NULL,
	[Dato] [date] NOT NULL,
	[nyhedsImg] [nvarchar](100) NOT NULL,
	[kategori] [int] NOT NULL,
 CONSTRAINT [PK_tblNyheder] PRIMARY KEY CLUSTERED 
(
	[idNews] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVidstedu]    Script Date: 08-03-2019 09:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVidstedu](
	[idVidstedu] [int] IDENTITY(1,1) NOT NULL,
	[overskrift] [nvarchar](100) NOT NULL,
	[vidsteDu] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_tblVidstedu] PRIMARY KEY CLUSTERED 
(
	[idVidstedu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblBruger] ON 

INSERT [dbo].[tblBruger] ([idUser], [brugernavn], [password], [email]) VALUES (1, N'marl', N'1234', N'test@test.dk')
SET IDENTITY_INSERT [dbo].[tblBruger] OFF
SET IDENTITY_INSERT [dbo].[tblKategorier] ON 

INSERT [dbo].[tblKategorier] ([idCategory], [category], [categoryImg]) VALUES (1, N'Indland', N'indland.png')
INSERT [dbo].[tblKategorier] ([idCategory], [category], [categoryImg]) VALUES (3, N'Krimi', N'krimi.png')
INSERT [dbo].[tblKategorier] ([idCategory], [category], [categoryImg]) VALUES (4, N'Sport', N'sport.png')
INSERT [dbo].[tblKategorier] ([idCategory], [category], [categoryImg]) VALUES (5, N'Udland', N'udland.png')
INSERT [dbo].[tblKategorier] ([idCategory], [category], [categoryImg]) VALUES (6, N'Underholdning', N'underholdning.png')
SET IDENTITY_INSERT [dbo].[tblKategorier] OFF
INSERT [dbo].[tblKontact] ([idKontact], [firm], [address], [zipcode], [city], [phone], [email]) VALUES (1, N'NwwsSite', N'Domkirketorvet 12b 1. th.', 8000, N'Aarhus C', N'+45 34 18 12 33', N'Kontakt@newssite.dk')
SET IDENTITY_INSERT [dbo].[tblNyheder] ON 

INSERT [dbo].[tblNyheder] ([idNews], [overskrift], [teasertekst], [nyheden], [Dato], [nyhedsImg], [kategori]) VALUES (3, N'Frederik og Mary på hemmelig rejse', N'Kronprins Frederik og kronprinsesse Mary er taget på uofficiel rejse. De skal angiveligt på privat slædetur i den storslåede grønlandske natur.', N' Grønlands Lufthavne har tirsdag afsløret billeder fra en lille lufthavn på Østgrønland, hvor Kronprinsparret pludselig dukkede op tirsdag.', CAST(N'2019-03-02' AS Date), N'royal.jpg', 5)
INSERT [dbo].[tblNyheder] ([idNews], [overskrift], [teasertekst], [nyheden], [Dato], [nyhedsImg], [kategori]) VALUES (4, N'Badminton-bosser vil stoppe medlemsflugt', N'DGI Badminton og Badminton Danmark samler kræfterne for at vende badmintons nedgang i antallet af medlemmer', N' Badmintonklubberne skal op på dupperne og have langt mere fokus på at skabe spændende klubmiljøer, der kan tiltrække flere medlemmer. - Det er meget alvorligt. Badminton oplever en deroute. Vi bliver nødt til at tage en ny kasket på og tænke nyt, siger Ib Møller.', CAST(N'2019-03-02' AS Date), N'badminton.jpg', 4)
INSERT [dbo].[tblNyheder] ([idNews], [overskrift], [teasertekst], [nyheden], [Dato], [nyhedsImg], [kategori]) VALUES (5, N'24-årig idømt forvaring for dødsvold', N'24-årige Dennis Vestergaard er idømt forvaring for at banke rocker ihjel med næverne, skriver Ekstra Bladet.', N' Den 24-årige Dennis Kronome Vestergaard er idømt forvaring på ubestemt tid for at banke Bandidos-rockeren Poul Joachim Hansen, kaldet Big Mac'', til døde med knytnæveslag på et værtshus i Helsinge i Nordsjælland. Et enigt nævningeting slog onsdag fast, at forvaring er nødvendig for at forhindre nye forbrydelser fra den 24-årige.', CAST(N'2019-03-03' AS Date), N'dommer.jpg', 3)
INSERT [dbo].[tblNyheder] ([idNews], [overskrift], [teasertekst], [nyheden], [Dato], [nyhedsImg], [kategori]) VALUES (6, N'Skraldemænd kede af det: Arbejder så meget vi kan', N'De strejkende skraldemænd i København har valgt at genoptage arbejdet. En midlertidig aftale har givet parterne ro til at forhandle. Skraldemændene vil gøre en ekstra indsats for at få fjernet alt det ophobede affald.', N' De strejkende skraldemænd i København har nu valgt at genoptage arbejdet, lyder det i en pressemeddelelse. - Vi har indgået en skriftlig aftale om, at renovationsfirmaet M. Larsen fortsætter i tre måneder med samme antal ansatte. I de tre måneder skal vi med kommunens advokat ved bordenden forsøge at lave en aftale om, hvordan driften skal være på Østerbro og Nørrebro efter 1. august, hvor City Renovation overtager driften. Inden da skal vi have nået en endelig aftale, siger faglig sekretær John Ø. Pedersen fra 3F Kastrup til Politiken.', CAST(N'2019-03-03' AS Date), N'skrald.jpg', 1)
INSERT [dbo].[tblNyheder] ([idNews], [overskrift], [teasertekst], [nyheden], [Dato], [nyhedsImg], [kategori]) VALUES (10, N'Skader tvinger U21-landstræner i tænkeboks', N'U21-landstræner Jess Thorup overvejer at lede efter helt nye spillere til angrebet efter de mange skadesafbud. Det giver', N' Det giver ikke kun hovedpine i FC København, at Andreas Cornelius, Youssef Toutouh og Danny Amankwaa har pådraget sig skader. - Cornelius'' fravær gør, at jeg skal til at tænke alternativt. Måske skal jeg ud og kigge efter helt andre typer - eller eventuelt kigge på en ny opstilling. Thorup udvælger sin bruttotrup til EM inden for samme periode, som Morten Olsen afslører navnene til de to næste A-landsholdskampe.', CAST(N'2019-03-04' AS Date), N'bold.jpg', 4)
INSERT [dbo].[tblNyheder] ([idNews], [overskrift], [teasertekst], [nyheden], [Dato], [nyhedsImg], [kategori]) VALUES (11, N'Op mod to millioner børn i akut nød i Nepal', N'Op mod halvdelen af befolkningen i Nepal er børn. Mange af dem står uden tag over hovedet og trues af sygdom.', N' Omkring 1,7 millioner børn er i akut nød efter jordskælvskatastrofen i Nepal, siger FN''s børnefond Unicef. Op mod halvdelen af befolkningen i Nepal er børn.', CAST(N'2019-03-04' AS Date), N'nepal.jpg', 5)
INSERT [dbo].[tblNyheder] ([idNews], [overskrift], [teasertekst], [nyheden], [Dato], [nyhedsImg], [kategori]) VALUES (12, N'Saudiarabere tvinges tættere på USA', N'Ny kronprins og udenrigsminister i Saudi-Arabien har USA-venlig baggrund, siger ekspert', N' Den saudiarabiske kong Salmans udpegning af sin nevø som kronprins er et forsøg på at gøre et ellers køligt forhold til USA varmere, mener ekspert. Saudi-Arabien er omringet af opløsningstruede lande, som det selv er militært involveret i. Og det tvinger nu landet til at lede efter andre alliancer, der blandt andet kan styrke det i rivaliseringen med Iran.', CAST(N'2019-03-04' AS Date), N'saudi.jpg', 5)
INSERT [dbo].[tblNyheder] ([idNews], [overskrift], [teasertekst], [nyheden], [Dato], [nyhedsImg], [kategori]) VALUES (13, N'Hollywoodpar krydser kærlige klinger', N'Efter 20-års venskab blev Charlize Theron og Sean Penn kærester, og selv om alt kører privat, løber temperamenterne løbsk professionelt', N' Der skulle gå hele 20 år, inden Sean Penn og Charlize Theron fandt sammen som kærester. - Der var øjeblikke, hvor jeg var meget urimelig over for ham, og øjeblikke hvor jeg følte, at han var utrolig urimelig over for mig. Men det får én til at indse, at uanset hvor kompliceret tingene bliver, så er førsteprioriteten forholdet, siger Theron, som trods de professionelle gnidninger indså, hvor meget Penn betyder for hende.', CAST(N'2019-03-04' AS Date), N'klinger.jpg', 6)
INSERT [dbo].[tblNyheder] ([idNews], [overskrift], [teasertekst], [nyheden], [Dato], [nyhedsImg], [kategori]) VALUES (14, N'Madonna overrasker rapper med vådt kys', N'Sangerinden Madonna kastede sig under en musikfestival ud i et hedt kys med en af hiphoppens største stjerner.', N' 56-årige Madonna er kendt for at slå kløerne i unge fyre, og søndag aften kastede hun sig så over rapperen Drake på28 år. Det skriver ABC News. Bagefter valsede hun af scenen med ordene: - Jeg er Madonna.', CAST(N'2019-03-05' AS Date), N'madonna.jpg', 6)
SET IDENTITY_INSERT [dbo].[tblNyheder] OFF
SET IDENTITY_INSERT [dbo].[tblVidstedu] ON 

INSERT [dbo].[tblVidstedu] ([idVidstedu], [overskrift], [vidsteDu]) VALUES (3, N'Flyulykker', N'Æsler dræber hvert år flere mennesker end flyulykker.')
INSERT [dbo].[tblVidstedu] ([idVidstedu], [overskrift], [vidsteDu]) VALUES (4, N'Umuligt', N'Det er fysisk umuligt at slikke sig selv på albuen.')
INSERT [dbo].[tblVidstedu] ([idVidstedu], [overskrift], [vidsteDu]) VALUES (5, N'Hjernen', N'Hjernen består af 80% vand.')
INSERT [dbo].[tblVidstedu] ([idVidstedu], [overskrift], [vidsteDu]) VALUES (6, N'Kalorier', N'Man forbrænder flere kalorier ved at sove, end ved at se TV')
INSERT [dbo].[tblVidstedu] ([idVidstedu], [overskrift], [vidsteDu]) VALUES (7, N'Coca Cola', N'Coca-Cola oprindeligt var grøn (ligesom julemanden).')
INSERT [dbo].[tblVidstedu] ([idVidstedu], [overskrift], [vidsteDu]) VALUES (8, N'Prosit', N'Et nys kan komme op på 400 km/t.')
INSERT [dbo].[tblVidstedu] ([idVidstedu], [overskrift], [vidsteDu]) VALUES (9, N'Dynamit', N'Peanuts er en ingrediens i fremstillingen af dynamit.')
INSERT [dbo].[tblVidstedu] ([idVidstedu], [overskrift], [vidsteDu]) VALUES (10, N'Tung(e)', N'En almindelig elefant vejer sædvanligvis mindre end tungen på en blåhval.')
INSERT [dbo].[tblVidstedu] ([idVidstedu], [overskrift], [vidsteDu]) VALUES (11, N'Guldfisk', N'En guldfisk har en korttidshukommelse på tre sekunder.')
INSERT [dbo].[tblVidstedu] ([idVidstedu], [overskrift], [vidsteDu]) VALUES (12, N'Tastatur', N'”Stewardesse” staves udelukkende med venstre hånd på et tastatur.')
SET IDENTITY_INSERT [dbo].[tblVidstedu] OFF
ALTER TABLE [dbo].[tblNyheder]  WITH CHECK ADD  CONSTRAINT [FK_tblNyheder_tblKategorier] FOREIGN KEY([kategori])
REFERENCES [dbo].[tblKategorier] ([idCategory])
GO
ALTER TABLE [dbo].[tblNyheder] CHECK CONSTRAINT [FK_tblNyheder_tblKategorier]
GO
