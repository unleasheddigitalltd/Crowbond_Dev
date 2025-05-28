using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.Strategies;

/// <summary>
/// Naive product classifier that identifies fruit/veg products by name patterns.
/// This can be easily replaced with more sophisticated strategies (category-based, location-based, etc.)
/// </summary>
public class NameBasedProductPickingClassifier : IProductPickingClassifier
{
    private static readonly string[] FruitVegKeywords = 
    {
        "tomato", "tomatoes",
        "apple", "apples", 
        "banana", "bananas",
        "orange", "oranges",
        "lettuce", "salad",
        "carrot", "carrots",
        "potato", "potatoes",
        "onion", "onions",
        "pepper", "peppers",
        "cucumber", "cucumbers",
        "broccoli", "cauliflower",
        "spinach", "kale",
        "strawberry", "strawberries",
        "grape", "grapes",
        "lemon", "lemons",
        "lime", "limes",
        "avocado", "avocados",
        "mushroom", "mushrooms"
    };

    public bool RequiresIndividualPicking(Product product)
    {
        if (product?.Name == null)
        {
            return false;
        }

        var productName = product.Name.ToLowerInvariant();
        
        return FruitVegKeywords.Any(keyword => productName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }
}