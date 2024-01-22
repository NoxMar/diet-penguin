using DietPenguin.Core;

namespace DietPenguin.Domain.User;

public record UserCreatedEvent(Guid Id) : DomainEvent;