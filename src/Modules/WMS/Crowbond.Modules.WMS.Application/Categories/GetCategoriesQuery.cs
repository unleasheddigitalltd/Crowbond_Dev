using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Categories.Dtos;

namespace Crowbond.Modules.WMS.Application.Categories;

public sealed record GetCategoriesQuery() : IQuery<IReadOnlyCollection<CategoryResponse>>;
