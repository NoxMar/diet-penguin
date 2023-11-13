## Wizualizacja systemu

Do wizualizacji projektowanego systemu używane będą diagramy [modelu C4](https://c4model.com/). Ważne cechy tego modelu to:
- obrazowanie systemu na hierarchicznych poziomach abstrakcji (systemu, kontenerów, komponentów i kodu)
- bazuje na oglądzie ["4+1"](https://en.wikipedia.org/wiki/4%2B1_architectural_view_model) na architekturę systemu
- oparty na prostszym od UML języku modelowania, który może być uzupełniany dokładniejszymi diagramami UML dla części systemu, które tego wymagają

### Diagram kontekstu

![Diagram kontekstu wygenerowany na podstawie modelu structurizr](/docs/images/diagrams/structurizr-1-Diagram-kontekstu.png)
![Klucz diagramu kontekstu wygenerowany na podstawie modelu structurizr](/docs/images/diagrams/structurizr-1-Diagram-kontekstu-key.png)


### Podstawowe terminy i pojęcia

#### Poziomy abstrakcji

Warto zaznaczyć, że model ten nie zaleca modelowania każdego elementy systemu do najniższego poziomu abstrakcji. Oficjalna dokumentacja zaleca tworzenie diagramu kontekstu i kontenerów dla każdego projektu o jakkolwiek wyższym stopniu złożoności a diagramów komponentów i kodu jeśli są one przydatne dla danych kontenerów/komponentów i zespół uważa, że są one przydatne. Na tę decyzję wpływa zazwyczaj złożoność kontenera/komponentu, jak kluczowy jest on dla systemu i spodziewane tempo jego zmian.

1. Diagram kontekstu (Context) - obrazuje analizowany system w kontekście jego interakcji z użytkownikami i innymi systemami. Jest przeznaczony zarówno dla architektów jak i deweloperów jak i "mniej technicznych" członków organizacji/uczestników projektu. Przykład:
![Przykład z oficjalnej strony](https://static.structurizr.com/workspace/76749/diagrams/SystemContext.png)
2. Diagram kontenerów (container) - jest swoistym "zbliżeniem" na system, który analizujemy. Pokazuje on jego wewnętrzne jednostki wdrożeniowe nazywane kontenerami i relacje między nimi. Warto zaznaczyć, że przypadku tego diagramu kontenery nie mają związku z kontenerami jako metodą wdrożenia (np. Docker czy inne narzędzia współpracujące z kontenerami OCI[^1]) a są to po prostu jednostki wdrożenia (zawartość każdego kontenera może być samodzielnie uruchomiona w opisanym na diagramie środowisku uruchomieniowym). Warto wspomnieć, że kontener nie musi być oprogramowaniem stworzonym w ramach analizowanego systemu. Może to, równie dobrze, być wdrożone w ramach systemu gotowe oprogramowanie jak np. system zarządzania bazą danych. Jest przeznaczony głównie dla "technicznych" członków organizacji takich jak deweloperzy, architekci oprogramowania, specjaliści od operacji IT. Przykład:
![Przykład z oficjalnej strony](https://static.structurizr.com/workspace/76749/diagrams/Containers.png)
3. Diagram komponentów (component) - kolejny poziom "zbliżenia" dekomponujący **pojedynczy kontener** na komponenty w klasycznym rozumieniu tego terminu w kontekście projektowania oprogramowania. Jest on przeznaczony głównie dla architektów oprogramowania i deweloperów. Przykład:
![Przykład z oficjalnej strony](https://static.structurizr.com/workspace/36141/diagrams/Components.png)
4. Diagram kodu (code) - używa diagramu UML, aby zobrazować implementację wewnętrzną **pojedynczego komponentu**. Typowo używany jest w tym celu diagram klas, jednak jeśli jest to bardziej odpowiednie dla danego komponentu, stosuje się inne diagramy jak np. ERD. Jest on przeznaczony głównie dla architektów oprogramowania i deweloperów. Przykład:
![Przykład z oficjalnej strony](https://c4model.com/img/class-diagram.png)

[^1]: [Open Container Initniative](https://opencontainers.org/) - orgazniacja zajmująca się rozwijaniem otwartych standardów konteneryzacji i środowisk uruchomieniowych kontenerów.

### Wybór narzędzi

Model C4 jest zintegrowany na różnych poziomach z różnymi popularnymi narzędziami do tworzenia diagramów i ogólnymi narzędziami CASE takimi jak PlantUML, Visual Paradigm czy chociażby Microsoft Visio. Dla tego projektu wybrane zostały jednak narzędzie [https://structurizr.org/](Structurizr). Do powodów tego wyboru należały:
- generowanie wielu diagramów systemu na różnych poziomach abstrakcji z **jednego modelu**. Pozwala to zapewnić, że różne diagramy systemu nie staną się z czasem rozbieżne między sobą. W podejściach zwinnych, w których struktura systemu może zmieniać się bardziej dynamicznie w trakcie jego powstawania. Kontrastując to z modelem kaskadowym, w którym zmiany w diagramach są rzadsze i wykonywane zazwyczaj w krótszym czasie (co ułatwia nieprzeoczenie jednego z diagramów, w których należałoby odnotować wprowadzaną zmianą) wydaje się to ważniejsze.
- fakt, że jest to narzędzie typu "diagrams as a code" co pozwala śledzić zmiany modelu razem z kodem w systemie kontroli wersji. Przez to narzędzie używany jest język dziedzinowy [Structurizr DSL](https://docs.structurizr.com/dsl). Technicznie można to też robić z diagramami generowanymi narzędziami używającymi formatów binarnych/nieudokumentowanych formatów wewnętrznych jednak dziennik zmian dostarczany przez system kontroli wersji w tym przypadku jest dużo mniej czytelny.
- dostarczanie przez ten projekt narzędzia Structurizr CLI, które pozwala eksportować diagramy z wiersza poleceń. Dzięki temu narzędziu powinna być możliwa integracja z wybranym narzędziem Github Actions w celu utrzymywania w pełnej synchronizacji diagramów zawartych w formie obrazów w dokumentacji utrzymywanej w formacie Markdown z modelem.