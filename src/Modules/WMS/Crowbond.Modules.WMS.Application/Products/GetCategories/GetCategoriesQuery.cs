using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Products.GetCategories;

public sealed record GetCategoriesQuery() : IQuery<IReadOnlyCollection<CategoryResponse>>;
