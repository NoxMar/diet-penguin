Wybraną przez nas metodą przeprowadzenia analizy ryzyka jest **wykres spalania ryzyka**.

Jest to praktyka znana przy zwinnym projektowaniu oprogramowania, która pozwala zespołowi deweloperskiemu na śledzenie ekspozycji projektu na ryzyko w miarę powstawania kolejnych iteracji.

Zastosowanie tego podejścia wymaga od nas wcześniejszego zdefiniowania danych wejściowych takich jak:

- Lista zidentyfikowanych przez nas ryzyk,
- Oszacowane prawdopodobieństwo ich wystąpienia,
- Oszacowana strata (wyrażona będzie przez nas w dniach),

Jako wynik powyższych rozważań, otrzymamy tzw. **ekspozycję**, która jest iloczynem oszacowanego prawdopodobieństwa oraz straty.

### Lista potencjalnych ryzyk w projekcie:

1. **Problemy z integrowalnością zewnętrznych serwisów** - ponieważ aplikacja będzie musiała integrować się z aplikacją deskstopową używaną przez firmę do zarządzania wizytami klientów, mogą pojawić się problemy np. z API,
2. **Problemy z ochroną danych i prywatnością użytkowników** - konieczne będzie przestrzeganie regulacji dotyczących ochrony danych (np. RODO, GDPR) oraz zapewnienie bezpieczeństwa danych użytkowników. W związku z tym należy wdrożyć odpowiednie mechanizmy zabezpieczeń, przeprowadzić audyty i przeszkolić pracowników z zakresu ochrony danych osobowych,
3. **Problemy z interfejsem użytkownika** - jeśli interfejs użytkownika nie będzie intuicyjny lub trudny do obsługi, pilotażowa grupa użytkowników, która będzie testowała prototyp może mieć negatywne wrażenia z używania aplikacji; z kolei niedostosowanie się do zaleceń i aktualnego wizerunku firmy (style guide), może doprowadzić do sytuacji, w której to już sam klient nie zaakceptuje interfejsu aplikacji w danej postaci i konieczne będą zmiany,
4. **Problemy z wydajnością aplikacji** - wraz z rozwojem produktu oraz rozrostem firmy klienta, teoretycznie możliwe są spadki wydajności aplikacji, związane z potencjalnie zwiększająca się liczbą użytkowników,
5. **Problemy z działaniem aplikacji na wszystkich OS** - do terminu oddania projektu zespołowi może zabraknąć czasu na sprawdzenie poprawności działania systemu we wszystkich dostępnych przeglądarkach, na wszystkich najpopularniejszych systemach operacyjnych,
6. **Problemy z dostarczoną dokumentacją** - klient może stwierdzić, że opracowana przez zespół projektowy dokumentacja oraz intrukcja użytkownika są niewystarczające do przeszkolenia pracowników i klientów; może wymagać więcej dokumentów opisujących działanie systemu.

### Dane do wykresu spalania ryzyka - Iteracja 0

| Ryzyko                                                | Prawdopodobieństwo (w %) | Strata (w dniach) | Ekspozycja |
| ----------------------------------------------------- | ------------------------ | ----------------- | ---------- |
| Problemy z integrowalnością zewnętrznych serwisów     | 20                       | 15                | 3          |
| Problemy z ochroną danych i prywatnością użytkowników | 30                       | 20                | 6          |
| Problemy z interfejsem użytkownika                    | 50                       | 4                 | 2          |
| Problemy z wydajnością aplikacji                      | 10                       | 10                | 1          |
| Problemy z działaniem aplikacji na wszystkich OS      | 40                       | 5                 | 2          |
| Problemy z dostarczoną dokumentacją                   | 25                       | 20                | 5          |
| Ekspozycja                                            |                          |                   | 19         |
