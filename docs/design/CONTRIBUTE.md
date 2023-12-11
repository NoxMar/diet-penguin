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

## Narzędzia do użycia przez deweloperów

### Automatyczne sprawdzanie wiadomości commitu

Automatyzacja sprawdzania zgodności wiadomości commitu z ustalonym [standardem](#wiadomości-commitów) możliwa jest przy użyciu [git hook](https://git-scm.com/book/en/v2/Customizing-Git-Git-Hooks) skonfigurowanego przy użyciu narzędzi [commitlint](https://commitlint.js.org/) i [husky](https://typicode.github.io/husky/). Aby używać [tego](/.husky/commit-msg) hooka trzeba **z katalogu repozytorium**, wydać po jego sklonowaniu następujące polecenia (na maszynie **muszą być zainstalowane [node.js i npm](https://nodejs.org/en)**):

```sh
npm install
npx husky install
```

Po tych krokach próba dodania commitu, którego wiadomość nie jest zgodna z przyjętą specyfikacją powodować będzie błąd dodania commitu. Treść tego błędu będzie zawierać informacje o tym, które reguły zostały naruszone.

Przykład:

```ps
git commit -m "wiadomosc z bledem"
⧗   input: wiadomosc z bledem
✖   subject may not be empty [subject-empty]
✖   type may not be empty [type-empty]

✖   found 2 problems, 0 warnings
ⓘ   Get help: https://github.com/conventional-changelog/commitlint/#what-is-commitlint

husky - commit-msg hook exited with code 1 (error)
```

### Diagramy

> [!NOTE] 
> korki 2-4 mogą być zastępnione przez uruchomienie skryptu PowerShel w ścieżce [/scripts/Start-StructurizrLite.ps1](/scripts/Start-StructurizrLite.ps1). Urchomi on kontener i otorzwy WebUI narzędzia [Structurizr Lite](https://structurizr.com/help/lite) w przeglądarce. Jeśli chcesz użyć portu innego niż `8080` wywołaj skrypt z atrybutem `-HostPort <port>`. Sposób uruchomienia skryptu opisany jest w jego treści jednak, w skrócie, wydanie następujacego pocenia w PowerShell z **głównego katalogu tego repozytorium** powinno być wystarczające:
>
> `PowerShell -ExecutionPolicy Bypass -File scripts/Start-StructurizrLite.ps1 [-HostPort <port>]`
> 
> Parametr `-HostPort` jest opcjonalny.

W ramach tego projektu używany jest [Structurizr](https://structurizr.com/) ([dokładniejszy opis użycia w projekcie](system-design.md#wizualizacja-systemu)). Narzędziem zalecanym do edycji diagramów na maszynach deweloperskich jest [Structurizr Lite](https://structurizr.com/help/lite). Poniżej przedstawiam instrukcję jego użycia:
1. Upewnij się, że masz zainstalowany na maszynie [Docker](https://docs.docker.com/engine/install/)
2. Uruchom kontener bazujący na [obrazie Structurizr Lite](https://hub.docker.com/r/structurizr/lite) poleceniem `docker run -it --rm -p <port_na_ktorym_chcesz_miec_dostep>:8080 -v <sciezka_do_katalogu_structurizr>:/usr/local/structurizr structurizr/lite`
    - dla przykładu jeśli masz na swojej maszynie wolny port 8080 i polecenie wydajesz z katalogu głównego tego repozytorium, ta komenda przybierze formę: `docker run -it --rm -p 8080:8080 -v ./structurizr:/usr/local/structurizr structurizr/lite` (**w 99.99% przypadków można użyć tej komendy bez zmian, najwyżej zmieniając port**)
3. Zaczekaj na uruchomienie kontonera (zazwyczaj nie więcej niż 10 sekund). Będzie to zasygnalizowane wypisaniem przez kontener informacji o licencji.
4. Otwórz w przeglądarce adres [http://localhost:8080](http://localhost:8080) (zmień port jeśli używasz innego). **UWAGA:** kontener nie używa HTTPS, wprowadzając link musisz użyć prefiksu protokołu `http://`.
5. Możesz teraz dokonywać edycji pliku opisującego system i jego diagramy w pliku [/structurizr/workspace.dsl](/structurizr/workspace.dsl). Jeśli chcesz podejrzeć nowe diagramy, odśwież otwartą w korku 4 kartę w przeglądarce.
6. Gdy skończysz edycję i chcesz dodać jej wyniki do repozytorium **silnie zalecane jest wyeksporotowanie ich i zapisanie do [/docs/images/diagrams/](/docs/images/diagrams/)**. Jeśli tego nie zrobisz zostają wyrenderowane przez [workflow](#workflowy) jednak nie jest to preferowane. Jeśli chcesz zapoznać się z powodem przeczytaj [opis workflowu "Wyrenderuj diagramy Struturizr"](#workflowy).

## Github Actions

### Workflowy

- [Wyrenderuj diagramy Struturizr](/.github/workflows/render-structurizr-diagrams.yaml) - Renderuje diagramy, jeżeli ich źródło zostało zmienione **a pliki wyrenderowanych diagramów nie**. Preferowane jest korzystanie z narzędzia [Structurizr Lite](https://structurizr.com/help/lite) ze względu na lepszą czytelność. **UWAGA: Ręcznie wyrenderuj wszystkie zmienione diagramy, jeśli którykolwiek został wyrenderowany ręcznie.** Akcja nie jest w stanie rozróżnić, które diagramy uległy zmianie, a które nie.