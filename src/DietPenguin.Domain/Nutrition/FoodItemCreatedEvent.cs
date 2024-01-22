using DietPenguin.Core;

namespace DietPenguin.Domain.Nutrition;

public record FoodItemCreatedEvent(Guid Id) : DomainEvent;