using Crowbond.Modules.WMS.Domain.Products;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.Strategies;

public interface IProductPickingClassifier
{
    /// <summary>
    /// Determines if a product should use individual (per-order) picking instead of consolidated picking
    /// </summary>
    /// <param name="product">The product to classify</param>
    /// <returns>True if product requires individual picking, false for consolidated picking</returns>
    bool RequiresIndividualPicking(Product product);
}