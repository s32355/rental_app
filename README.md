# Rental App

Aplikacja konsolowa w C# obsługująca uczelnianą wypożyczalnię sprzętu.

## Instrukcja uruchomienia

1. Sklonuj repozytorium:
```
git clone https://github.com/s32355/rental_app
```
2. Przejdź do folderu projektu:
```
cd rental_app/rental_app
```
3. Uruchom aplikację:
```
dotnet run
```

> Wymagany .NET 10.0 lub nowszy.

---

## Opis projektu

System pozwala na rejestrowanie sprzętu, wypożyczanie go użytkownikom, zwroty, kontrolę dostępności oraz generowanie raportów. Dane są zapisywane i odczytywane z plików JSON, dzięki czemu są trwałe między uruchomieniami aplikacji.

### Obsługiwane operacje

1. Dodanie nowego użytkownika do systemu
2. Dodanie nowego sprzętu danego typu
3. Wyświetlenie listy całego sprzętu z aktualnym statusem
4. Wyświetlenie wyłącznie sprzętu dostępnego do wypożyczenia
5. Wypożyczenie sprzętu użytkownikowi
6. Zwrot sprzętu wraz z przeliczeniem ewentualnej kary za opóźnienie
7. Oznaczenie sprzętu jako niedostępnego (uszkodzenie lub serwis)
8. Wyświetlenie aktywnych wypożyczeń danego użytkownika
9. Wyświetlenie listy przeterminowanych wypożyczeń
10. Wygenerowanie krótkiego raportu podsumowującego stan wypożyczalni

### Reguły biznesowe

- Student może mieć jednocześnie maksymalnie **2** aktywne wypożyczenia
- Pracownik może mieć jednocześnie maksymalnie **5** aktywnych wypożyczeń
- Sprzęt niedostępny nie może zostać wypożyczony
- Opóźniony zwrot skutkuje naliczeniem kary w wysokości **2.5** za każdy dzień opóźnienia

Wszystkie limity i stawki są zdefiniowane jako stałe `const` w klasie `RentalService`, co umożliwia łatwą ich zmianę w jednym miejscu.

---

## Decyzje projektowe

### Podział na warstwy

Projekt jest podzielony na cztery wyraźne warstwy: modele (`entity`), repozytoria (`repository`), serwisy (`service`) i widoki (`view`). Każda warstwa ma jedną odpowiedzialność i nie miesza się z pozostałymi — widoki nie znają repozytoriów, serwisy nie wiedzą jak dane są wyświetlane.

### Kohezja

Każda klasa ma jeden wyraźny cel, np. `RentalService` odpowiada wyłącznie za logikę biznesową wypożyczeń. `DeviceView` odpowiada wyłącznie za interakcję z użytkownikiem w zakresie sprzętu. `RentalRepo` odpowiada wyłącznie za zapytania o dane wypożyczeń. Dzięki temu zmiana jednej rzeczy nie wymusza zmian w innych klasach.

### Coupling

Zależności są wstrzykiwane przez konstruktor (Dependency Injection), a nie tworzone wewnątrz klas. Dzięki temu `RentalService` nie tworzy repozytoriów samodzielnie — dostaje je z zewnątrz. Wszystkie instancje repozytoriów są tworzone raz w `Program.cs` i współdzielone między serwisami, co zapewnia spójność danych.

`RentalView` komunikuje się z `UserView` i `DeviceView` przez przekazanie `Action` zamiast bezpośredniej referencji — dzięki temu widoki nie są od siebie bezpośrednio zależne.

### Dziedziczenie wynikające z modelu domeny

Dziedziczenie zastosowano tylko tam, gdzie wynika z rzeczywistych relacji domenowych. `Camera`, `Laptop` i `Projector` są urządzeniami — dziedziczą po `Device`. `Student` i `Employee` są użytkownikami systemu — dziedziczą po `User`.

### Obsługa błędów

Operacje, które mogą się nie powieść, rzucają wyjątki z czytelnymi komunikatami (`KeyNotFoundException`, `InvalidOperationException`, `ArgumentException`). Wyjątki są łapane w warstwie widoku i wyświetlane użytkownikowi, nie crashując aplikacji.

### Zapis i odczyt danych

Dane są zapisywane do plików JSON w folderze `data/`. Repozytoria automatycznie ładują dane przy starcie i zapisują je po zakończeniu działania aplikacji w bloku `finally` w `Program.cs`, co gwarantuje zapis nawet przy nieoczekiwanym wyjątku.

---