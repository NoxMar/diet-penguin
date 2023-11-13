workspace {

    model {
        # Personas
        client = person "Klient" "Użytkownik aplikacji, który monitoruje swoją dietę sam lub przy pomocy dietetyka"
        dietitian = person "Dietetyk" "Użytkownik aplikacji będący dietetykiem, który ustala cele i monitoruje postęp i dietę klientów"

        # System
        dietPenguin = softwareSystem "Diet Penguin" "Pozwala użytkownikom śledzić swoją dietę i poziom aktywności w kontekście celów ustalonych przez użytkownika lub jego dietetyka" {
            singlePageApp = container "Progresywna aplikacja webowa typu SPA" "Dostarcza interfejs graficzny dla pełnego zakresu funkcjonalności systemu" "Przeglądarka i Blazor" "Web Browser"
            apiApp = container "Aplikacja API" "Dostarcza funkcjonalność systemu poprzez REST API" "C# i ASP.NET Core"
            database = container "Baza danych" "Przechowuje dane związane z użytkownikami, historię ich diety i aktywności oraz cele. NIE przechowuje danych logowania" "MS SQL Server" "Database"
        }

        # External systems
        identityProvider = softwaresystem "Dostawca tożsamości" "System przechowujący dane logowania użytkowników i pozwalający im na rejestrację i logowanie się" "External System"

        # Relationships between personas and systems
        ## Modeled system
        client -> dietPenguin "Wprowadza dane o posiłkach i aktywności oraz monitoruje postęp do ustalonych celów"
        dietitian -> dietPenguin "Ustala cele, monitoruje do nich postęp i wprowadza przepisy dla swoich klientów"
        ## External systems
        client -> identityProvider "Rejestruje i loguje się" "HTTPS"
        dietitian -> identityProvider "Rejestruje i loguje się" "HTTPS"

        # Relationships between system and exeternal systems
        dietPenguin -> identityProvider "Potwierdza status zalogowania użytkownika"

        # Relationships between containers
        ## Container <-> container
        apiApp -> database "Odczytuje z i zapisuje do" "SQL/TCP"
        singlePageApp -> apiApp "Wykonuje żądania API" "JSON/HTTPS"
        ## Cotnainer <-> system
        apiApp -> identityProvider "Waliduje poświadczenie tożsamości użytkownika" "OIDC JSON/HTTPS"
        ## Persona <-> Container
        client -> singlePageApp "Wprowadza dane, przegląda je i sprawdza postęp względem celów"
        dietitian -> singlePageApp "Przegląda postęp swoich klientów i ustala ich cele"
    }

    views {
        systemContext dietPenguin "Diagram-kontekstu" {
            include *
            autolayout
        }

        container dietPenguin "Diagram-kontenerow" {
            include *
            autolayout
        }

        theme default


        styles {
            element "External System" {
                background #999999
                color #ffffff
            }
            element "Database" {
                shape Cylinder
            }
            element "Web Browser" {
                shape WebBrowser
            }
        }
    }

}