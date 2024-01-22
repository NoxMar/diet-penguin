using DietPenguin.Domain.User;

namespace DietPenguin.Domain.Nutrition;

public record Portion(
    string Name,
    MassValue Size
);