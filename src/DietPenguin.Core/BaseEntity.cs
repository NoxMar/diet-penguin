using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DietPenguin.Core;


// ReSharper disable file UnusedAutoPropertyAccessor.Global

/// <summary>
/// Jest bazą dla wszystkich encji domenowych.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Unikalny identyfikator domeny.
    /// </summary>
    [Key] public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Czas utworzenia encji.
    /// </summary>
    public DateTime CreatedOn { get; private set; }
    
    /// <summary>
    /// Identyfikator (domyślnie e-mail) użytkownika, który stworzył encję.
    /// </summary>
    public string? CreatedBy { get; private set; }
    
    /// <summary>
    /// Czas ostatniej modyfikacji.
    /// </summary>
    public DateTime? LastModifiedOn { get; private set; }
    
    /// <summary>
    /// Identyfikator(domyślnie e-mail) użytkownika, który dokonał ostatniej modyfikacji.
    /// </summary>
    public string? LastModifiedBy { get; private set; }
    
    /// <summary>
    /// Oznaczanie czy encja nie jest usunięta w ramach miękkiego usuwania.
    /// </summary>
    public bool IsDeleted { get; private set; }
    
    [NotMapped]
    private readonly List<DomainEvent> _domainEvents  = new();

    /// <summary>
    /// Zdarzenia domenowe, które są w kolejce dla tej encji. Zdarzenia trafiają tu w wyniku
    /// wywołania <see cref="QueueDomainEvent"/>. Co ważne, zdarzenia zakolejkowane nie są poprawnymi zdarzeniami,
    /// ponieważ jeszcze się nie zdarzyły.
    /// </summary>
    public IEnumerable<DomainEvent> DomainEvents => _domainEvents.AsEnumerable();

    /// <summary>
    /// Powinno być wywołane po utworzeniu nowej encji (niekonienicze instancji encji nie używaj tego w konstruktorze,
    /// ponieważ będzie on wywoływany też np. przy odczytywaniu encji z bazy danych).
    /// </summary>
    /// <param name="createdOn">Moment utworzenia</param>
    /// <param name="createdBy">Identyfikator (e-mail) użytkownika, który utworzył.</param>
    protected void UpdateCreationProperties(DateTime createdOn, string? createdBy)
    {
        CreatedOn = createdOn;
        CreatedBy = createdBy;
    }

    /// <summary>
    /// Zmienia pola związane z ostatnią modyfikacją encji. Powinno być wywoływane w metodach modyfikujących stan encji.
    /// </summary>
    /// <param name="lastModified">Moment ostatniej modyfikacji</param>
    /// <param name="modifiedBy">Identyfikator (e-mail) użytkownika, który zmodyfikował.</param>
    protected void UpdateModifiedProperties(DateTime? lastModified, string? modifiedBy)
    {
        LastModifiedOn = lastModified;
        LastModifiedBy = modifiedBy;
    }
    
    /// <summary>
    /// Zmienia stan "usunięcia" encji.
    /// </summary>
    /// <remarks>
    /// Ponieważ implementujemy miękkie usuwanie możliwe jest też przywrócenie usuniętej
    /// encji używając <c>false</c> jako wartości <paramref name="isDeleted"/>
    /// </remarks>
    /// <param name="isDeleted">ustala czy encja ma być oznaczona czy odznaczona jako usunięta</param>
    public void UpdateIsDeleted(bool isDeleted = true)
    {
        IsDeleted = isDeleted;
    }

    /// <summary>
    /// Kolejkuje zdarzenie domenowe, jeśli takie same zdarzenie nie zostało już zakolejkowane.
    /// </summary>
    /// <remarks>
    /// Zakolejkowanie zdarzenia nie sygnalizuje go, więc można używać tej metody zanim zdarzenie już się dokonało.
    /// Zdarzenie związane z encją zgłaszane są automatycznie przez kontekst bazy danych po zapisaniu
    /// zamian dotyczących encji.
    /// </remarks>
    /// <param name="event">zdarzenie do zakolejkowania</param>
    public void QueueDomainEvent(DomainEvent @event)
    {
        if (!DomainEvents.Contains(@event))
        {
            _domainEvents.Add(@event);
        }
    }
}