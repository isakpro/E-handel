\# SportGear – E-handelsprojekt



En fullstack e-handelsapplikation byggd med ASP.NET Core Web API och Blazor WebAssembly.



\## Projektbeskrivning

SportGear är en webbshop för sportkläder och utrustning. Användare kan bläddra bland produkter, söka och filtrera på pris, lägga till produkter i kundvagnen och lägga orders. Administratörer kan hantera produkter och orders via skyddade endpoints.



\## Teknikstack

\- \*\*Backend:\*\* ASP.NET Core Web API (.NET 10)

\- \*\*Frontend:\*\* Blazor WebAssembly

\- \*\*Autentisering:\*\* JWT (JSON Web Tokens)

\- \*\*Arkitektur:\*\* Lagerarkitektur (Controller → Service → Repository)



\## Kom igång



\### Krav

\- .NET 10 SDK

\- Visual Studio 2022



\### Starta projektet

1\. Klona repot:

```

&#x20;  git clone https://github.com/isakpro/E-handel

```

2\. Öppna `ECommerce.sln` i Visual Studio

3\. Högerklicka på Solution → Properties → Startup Project → Multiple startup projects

4\. Sätt både `ECommerce.Api` och `ECommerce.Client` till Start

5\. Tryck på play-knappen



\### Demo-inloggning

\- \*\*Email:\*\* admin@test.com

\- \*\*Lösenord:\*\* password123



\## Arkitektur

```
ECommerce.Api/
├── Controllers/    # HTTP-endpoints
├── Services/       # Affärslogik
├── Repositories/   # Datahämtning
└── Program.cs      # Konfiguration

ECommerce.Client/
├── Pages/          # Blazor-sidor
├── Components/     # Återanvändbara komponenter
└── Services/       # Frontend-tjänster

ECommerce.Shared/
└── DTOs/           # Delade datamodeller

ECommerce.Tests/
└── ...             # Enhetstester med xUnit och Moq
```


## Enhetstester
Projektet har 10+ enhetstester skrivna med xUnit och Moq.
Testerna finns i `ECommerce.Tests`-projektet och testar främst service-lagret.

Kör testerna med:
```
dotnet test
```



\## Funktionalitet

\- Produktkatalog med bilder, sökning och prisfilter

\- Produktdetalj-sida

\- Kundvagn

\- Login och registrering med JWT

\- Orderhistorik (kräver inloggning)

\- Admin-sidor för produkter och orders



\## Ansvarsfördelning

| Person | Ansvar |

|--------|--------|

| Hamze | Produktsidor, sökfunktion, prisfilter, lagerarkitektur Orders |

| Isak | Projektledare, grundstruktur, API-setup |

| Dalmar | Autentisering, JWT, användare |

| Ihab | Kundvagn, checkout |

| Thanos | Tester, Bakcoffice, dokumentation |

