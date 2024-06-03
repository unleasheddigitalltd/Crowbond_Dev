using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Products.Application.Categories.Dtos;

namespace Crowbond.Modules.Products.Application.Categories;

public sealed record GetCategoriesQuery() : IQuery<IReadOnlyCollection<CategoryResponse>>;
