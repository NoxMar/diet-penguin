# Wymagania podczas pracy nad projektem

## Wiadomości commitów

Commity w tym projekcie korzystają ze specyfikacji [Convetional Commits w wersji 1.0.0](https://www.conventionalcommits.org/en/v1.0.0/). Commity mają w skrócie następującą strukturę

```
<typ>[opcjonalnie zakres]: <opis>

[opcjonalny dłuższy opis commitu]

[opcjonalne stopki]
```

### Pierwsza linia wiadomości

Pierwsza linia wiadomości **musi** spełniać wszystkie poniższe kryteria:

- `<opis>` zaczyna się **małą literą**
- `<opis>` **nie** jest zakończony kropką
- `<opis>` używa trybu rozkazującego czasu teraźniejszego (dla przykładu `ustaw` a nie `ustawi` czy `ustawiono`)
- **całość** pierwszej linii (wraz z typem i zakresem) **nie powinna przekraczać `70` znaków** 

### Typy

Typ jest **obowiązkowy** i powinien być wybrany z poniższej listy (ze znaczeniami zgodnymi ze [typami ze specyfikacji Angular](https://github.com/angular/angular/blob/22b96b9/CONTRIBUTING.md#type)):

- fix
- feat
- build
- ci
- docs
- style
- refactor
- perf
- test

### Zakresy

Jeśli commit dotyczy jednego z tych zakresów **należy go oznaczyć** natomiast **nie jest wymagane** jeśli zawartość commitu dotyczy **więcej niż jednego** z wymienionych zakresów lub **nie wpisuje się w żadne z nich**.

- backend
- frontned