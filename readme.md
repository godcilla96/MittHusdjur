# Husdjursapp

## Beskrivning
Detta är ett Windows Forms-applikationsprojekt i C# som simulerar ett virtuellt husdjur. Användaren kan välja mellan olika husdjurstyper, mata och leka med sitt husdjur samt hålla koll på dess hälsotillstånd (hunger, energi och glädje). Programmet är utformat för att underhålla användaren samtidigt som det ger en grundläggande inblick i objektorienterad programmering och Windows Forms-användargränssnitt i .NET.

## Funktioner
- **Välj djurtyp**: Användaren kan välja mellan hund eller katt.
- **Anpassa namn**: Ge husdjuret ett namn som visas i gränssnittet.
- **Interagera med husdjuret**: Mata, lek eller låt husdjuret sova för att hålla dess status på en bra nivå.
- **Statusindikatorer**: Hunger, glädje och energi visas i realtid och ändras baserat på interaktioner och timer.
- **Game Over**: Om husdjurets hungerstatus når 0 avslutas spelet.

## Klassbeskrivning
Projektet är uppdelat i flera klasser och formulär som representerar olika aspekter av husdjuret och användargränssnittet.

- **Pet.cs**: En klass som representerar husdjuret och dess egenskaper i form av hunger, glädje och energi. Den innehåller metoder för att justera dessa värden baserat på användarens handlingar.
  
- **Form1.cs**: Huvudformuläret som hanterar användargränssnittet. Här visas knappar för att mata, leka och sova, samt statusetiketter för att följa husdjurets tillstånd. Den använder en timer för att uppdatera husdjurets status över tid.
  
- **AnimalSelectionForm.cs**: Ett formulär där användaren väljer husdjurstyp (hund eller katt).
  
- **NameForm.cs**: Ett formulär där användaren kan namnge sitt husdjur, som sedan visas i huvudformuläret.

## Teknologier
Projektet är byggt med följande teknologier:
- **C#**: Programmeringsspråket som används för att implementera logik och funktionalitet.
- **.NET Framework**: Plattformen som tillhandahåller verktyg och bibliotek för Windows Forms-applikationer.
- **Windows Forms**: Användargränssnittet är utvecklat med Windows Forms för att skapa en grafisk miljö där användaren kan interagera med sitt virtuella husdjur.

## Kom igång
1. Klona eller ladda ner projektfilerna från detta repo.
2. Öppna projektet i Visual Studio/VS Code.
3. Kör projektet (dotnet run).

## Hur man använder applikationen
1. Starta applikationen.
2. Välj en djurtyp och namnge ditt husdjur.
3. Använd knapparna för att mata, leka med och låta husdjuret sova för att hålla dess status hög.
4. Om någon av statusarna når 0 kan husdjuret må dåligt och spelet avslutas om hungern går till 0.

## Projektstruktur
```plaintext
Husdjursapp/
├── Pet.cs                 # Klassen som hanterar husdjurets tillstånd och beteende
├── Form1.cs               # Huvudformulär för användargränssnittet och interaktion
├── AnimalSelectionForm.cs # Formulär för att välja djurtyp
├── NameForm.cs            # Formulär för att namnge husdjuret
├── Program.cs             # Startpunkt för applikationen
└── (Övriga resurser och filer)