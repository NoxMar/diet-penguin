workspace {

    model {
        # Personas
        client = person "Klient" "Użytkownik aplikacji, który monitoruje swoją dietę sam lub przy pomocy dietetyka"
        dietitian = person "Dietetyk" "Użytkownik aplikacji będący dietetykiem, który ustala cele i monitoruje postęp i dietę klientów"

        # System
        dietPenguin = softwareSystem "Diet Penguin" "Pozwala użytkownikom śledzić swoją dietę i poziom aktywności w kontekście celów ustalonych przez użytkownika lub jego dietetyka"

        # External systems
        identityProvider = softwaresystem "Dostawca tożsamości" "System przechowujący dane logowania użytkowników i pozwalający im na rejestrację i logowanie się" "External System"

        # Relationships between personas and systems
        ## Modeled system
        client -> dietPenguin "Wprowadza dane o posiłkach i aktywności oraz monitoruje postęp do ustalonych celów"
        dietitian -> dietPenguin "Ustala cele, monitoruje do nich postęp i wprowadza przepisy dla swoich klientów"
        ## External systems
        client -> identityProvider "Rejestruje i loguje się"
        dietitian -> identityProvider "Rejestruje i loguje się"

        # Relationships between system and exeternal systems
        dietPenguin -> identityProvider "Potwierdza status zalogowania użytkownika"
    }

    views {
        systemContext dietPenguin "Diagram-kontekstu" {
            include *
            autolayout
        }

        theme default


        styles {
            element "External System" {
                background #999999
                color #ffffff
            }
        }
    }

}