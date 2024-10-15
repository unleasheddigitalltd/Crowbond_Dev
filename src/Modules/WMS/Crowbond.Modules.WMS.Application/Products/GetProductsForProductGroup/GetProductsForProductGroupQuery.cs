using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.GetProductsForProductGroup;

public sealed record GetProductsForProductGroupQuery(Guid ProductGroupId) : IQuery<IReadOnlyCollection<ProductResponse>>;
