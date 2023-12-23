using MediatR;

namespace DietPenguin.Core;

/// <summary>
/// Klasa bazowa dla zdarzeń domenowych.
/// </summary>
/// <remarks>
/// Pamiętaj, że zdarzenia domenowe, z definicji, są faktami dokonanymi (już się zdarzyły).
/// Zdarzenia powinny być zgłaszane dopiero po pełnym i poprawnym wykonaniu czynności, której wykonanie sygnalizujemy.
/// </remarks>
public abstract record DomainEvent : INotification;