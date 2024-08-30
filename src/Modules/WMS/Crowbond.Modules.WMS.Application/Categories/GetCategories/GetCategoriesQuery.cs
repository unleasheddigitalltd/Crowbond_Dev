using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Categories.GetCategories;

public sealed record GetCategoriesQuery() : IQuery<IReadOnlyCollection<CategoryResponse>>;
