# Instrukcje dla AI - Chill Game

To jest gra Unity wykorzystująca Universal Render Pipeline (URP). Poniżej znajdują się kluczowe informacje o architekturze i wzorcach projektowych używanych w tym projekcie.

## Struktura Projektu

```
Assets/
├── Scripts/      # Skrypty C# podzielone na moduły gry
├── Scenes/       # Sceny Unity
├── Prefabs/      # Prefabrykaty komponentów
├── Materials/    # Materiały i shadery
└── Sprites/      # Grafiki 2D
```

## Architektura

### Zarządzanie Stanem Gry
- `GameManager.cs` jest głównym kontrolerem stanu gry
- Zarządza cyklem życia gry (start, pauza, game over)
- Kontroluje punktację i UI
- Współpracuje z komponentem `Player` do sterowania zachowaniem gracza

### Konwencje Kodowania
- Używamy MonoBehaviour dla komponentów Unity
- Stan gry jest kontrolowany przez TimeScale (1.0 = aktywna gra, 0 = pauza)
- UI jest zarządzany przez bezpośrednie referencje do komponentów

### System Wejścia
- Projekt używa nowego Input System (patrz `InputSystem_Actions.inputactions`)
- Akcje gracza są definiowane w systemie wejścia zamiast bezpośredniego używania Input.GetKey

### Debugowanie
- Ustaw Application.targetFrameRate = 60 dla spójnej wydajności
- Używaj Time.timeScale do pauzowania/wznawiania gry podczas debugowania

## Główne Komponenty

### Player
- Zarządza ruchem i stanem gracza
- Reaguje na kolizje z przeszkodami
- Komunikuje się z GameManager przy zdobywaniu punktów i kolizjach

### Pipes (Przeszkody)
- Generowane proceduralnie
- System punktacji bazuje na przejściu przez przeszkody
- Automatyczne czyszczenie przy restarcie gry

## Dobre Praktyki
1. Zawsze używaj referencji przez Inspektor Unity zamiast FindObjectOfType gdzie to możliwe
2. Pamiętaj o czyszczeniu obiektów gry przy restarcie (zobacz metodę Play w GameManager)
3. Trzymaj logikę UI w GameManager dla lepszej organizacji

## Integracje
- Universal Render Pipeline (URP) dla grafiki
- TextMesh Pro dla tekstu UI
- New Input System dla sterowania