using System.Collections.ObjectModel;
using DietPenguin.Core;

namespace DietPenguin.Domain.Nutrition;

public class FoodItem : BaseEntity
{
    public string Name { get; set; }
    private readonly List<Portion> _typicalPortions = new();
    public IReadOnlyList<Portion> TypicalPortions => _typicalPortions.AsReadOnly();
    private readonly Dictionary<Nutrient, ValueWithUnit> _nutritionalValuesPer100G = new ();
    public ReadOnlyDictionary<Nutrient, ValueWithUnit> NutritionalValuesPer100G { get; }

    private FoodItem(string name, 
        IEnumerable<Portion> typicalPortions, 
        IEnumerable<KeyValuePair<Nutrient, ValueWithUnit>> nutritionalValuesPer100G)
    {
        Name = name;
        _typicalPortions.AddRange(typicalPortions);
        foreach (var n in nutritionalValuesPer100G)
        {
            _nutritionalValuesPer100G[n.Key] = n.Value;
        }
        NutritionalValuesPer100G = new ReadOnlyDictionary<Nutrient, ValueWithUnit>(_nutritionalValuesPer100G);
    }

    public static Result<FoodItem> Create(string name,
        IEnumerable<Portion> typicalPortions,
        IEnumerable<KeyValuePair<Nutrient, ValueWithUnit>> nutritionalValuesPer100G)
    {
        var foodItem = new FoodItem(name, typicalPortions, nutritionalValuesPer100G);
        
        foodItem.QueueDomainEvent(new FoodItemCreatedEvent(foodItem.Id));
        return Result<FoodItem>.Success(foodItem);
    }
}